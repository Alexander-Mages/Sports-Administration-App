﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.Services;
using SportsAdministrationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace SportsAdministrationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IEmailService emailService;
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ILogger<AccountController> logger,
                                 IEmailService emailService,
                                 ApplicationDbContext dbContext,
                                 IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailService = emailService;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }


        //PRIVATE METHODS
        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
        private string GenerateSecureCode()
        {
            var b = new byte[sizeof(UInt64)];
            var cryptoCodeProvider = new RNGCryptoServiceProvider();
            cryptoCodeProvider.GetBytes(b);
            var num = BitConverter.ToUInt64(b, 0);
            var code = num % 1000000;
            return code.ToString("D6");
        }




        //REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
            if (ModelState.IsValid)
            {
                if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"], //gets recaptcha response and checks it
                    configuration.GetSection("GoogleReCaptcha:secret").Value, logger))
                {
                    ModelState.AddModelError(string.Empty, "Sorry sir, no bots allowed");
                    return View(model);
                }

                //AthleteData d = new AthleteData() { TopPR = 100 };
                //dbContext.Add(d);
                //await dbContext.SaveChangesAsync();
                //PRs t = new PRs() { Name = "initial", Time = 30 };
                //t.AthleteDataId = d.Id;
                //dbContext.Add(t);
                //await dbContext.SaveChangesAsync();

                ////d.PRs = new List<PRs>();
                ////d.PRs.Add(t)

                //d.TopPR = 50;
                //dbContext.Update(d);

                //PRs pr1 = new PRs() { Name = "new personal record", Time = 29 };
                //dbContext.Add(pr1);
                //await dbContext.SaveChangesAsync();

                PersonalRecord r = new PersonalRecord() { Time = 30 };
                AthleteData d = new AthleteData() { Location = "Random Natatorium", Time = 29 };
                r.AthleteData = new List<AthleteData>();
                r.AthleteData.Add(d);

                var user = new User { UserName = model.Email, Email = model.Email, Name=model.Name, /*Team=model.Team,*/ TwoFactorEnabled=model.TwoFactorEnabled, PersonalRecord = r};
                if (model.Team != "team")
                {

                }
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (user.TwoFactorEnabled == true)
                    {
                        
                    }
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = user.Id, token = token }, Request.Scheme);
                    
                    emailService.SendAuthEmail(user.Email, confirmationLink);

                    //await signInManager.SignInAsync(user, isPersistent: false);
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("index", "home");
                    }

                    return View("/Views/Account/ConfirmEmail.cshtml", model);
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }




        //CONFIRM EMAIL
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("EmailConfirmed");
            }
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }


        //TWO FACTOR AUTHENTICATION

        [HttpGet]
        public IActionResult TwoFactorConfirm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TwoFactorConfirm(TwoFactorConfirmViewModel model)
        {
            User user = await userManager.FindByIdAsync(HttpContext.Session.GetString("Id"));
            if (user.Code == model.Code)
            {
                await signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        //LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
                if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"], //gets recaptcha response and checks it
    configuration.GetSection("GoogleReCaptcha:secret").Value, logger))
                {
                    ModelState.AddModelError(string.Empty, "Sorry sir, no bots allowed");
                    return View(model);
                }

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var checkResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, true);

                    if (checkResult.Succeeded && user.TwoFactorEnabled == true)
                    {
                        string TwoFactorCode = GenerateSecureCode();
                        emailService.SendTwoFactorCode(user.Email, TwoFactorCode);
                        user.Code = TwoFactorCode;
                        await userManager.UpdateAsync(user);
                        HttpContext.Session.SetString("Id", user.Id);
                        return RedirectToAction("TwoFactorConfirm", "account");
                    }
                    
                    if (checkResult.Succeeded && user.TwoFactorEnabled == false)
                    {
                        var signInResult = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                            model.RememberMe, true);

                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("index", "home");
                        }
                        if (signInResult.IsLockedOut)
                        {
                            return View("AccountLocked");
                        }
                    }

                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }
            return View(model);
        }


        //LOGOUT
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }




        //FORGOT PASSWORD
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReCaptchaKey"] = configuration.GetSection("GoogleReCaptcha:key").Value;
                if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"], //gets recaptcha response and checks it
configuration.GetSection("GoogleReCaptcha:secret").Value, logger))
                {
                    ModelState.AddModelError(string.Empty, "Sorry sir, no bots allowed");
                    return View(model);
                }

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);

                    //logger.Log(LogLevel.Warning, passwordResetLink);
                    emailService.SendPasswordResetLink(user.Email, passwordResetLink);
                    return View("ForgotPasswordConfirmation");
                }
                //return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        //CHANGE PASSWORD
        public IActionResult ChangePassword()
        {
            return View("ForgotPassword");
        }



        //RESET PASSWORD
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid Passsword Reset Token");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsLockedOutAsync(user))
                        {
                            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                        }
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }


    }
}
