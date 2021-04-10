using Microsoft.AspNetCore.Identity;
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
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using System.Text;

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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITotpValidator totpValidator;
        private readonly ITotpGenerator totpGenerator;
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ILogger<AccountController> logger,
                                 IEmailService emailService,
                                 ApplicationDbContext dbContext,
                                 IConfiguration configuration,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailService = emailService;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.totpGenerator = new TotpGenerator();
            this.totpValidator = new TotpValidator(this.totpGenerator);
            this.roleManager = roleManager;
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

        static string RandomString(int len)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            using (RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];
                while (len-- > 0)
                {
                    Generator.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    result.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return result.ToString();
        }
        //END PRIVATE METHODS


        //REGISTER
        [HttpGet]
        [AllowAnonymous]
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
                PersonalRecord r = new PersonalRecord() { PR = 30 };
                AthleteData d = new AthleteData() { Location = "Random Natatorium", Time = 29 };
                r.AthleteData = new List<AthleteData>();
                r.AthleteData.Add(d);
                var user = new User { UserName = model.Email, Email = model.Email, Name=model.Name, TwoFactorEnabled=model.TwoFactorEnabled, PersonalRecord=r, TotpEnabled=model.TotpEnabled};

                Team team = dbContext.Teams.SingleOrDefault(t => t.TeamCode.ToLower() == model.TeamCode.ToLower());
                Team coach = null;
                if (model.CoachCode != null)
                {
                    coach = dbContext.Teams.SingleOrDefault(t => t.CoachCode.ToLower() == model.CoachCode.ToLower());
                }
                var result = await userManager.CreateAsync(user, model.Password);
                if (team != null && model.CoachEnabled == false)
                {
                    user.Team = team;
                    var roleResult = await userManager.AddToRoleAsync(user, Roles.AthleteRole);
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Team Code Invalid");
                        return RedirectToAction("Register", "Account");
                    }
                }
                else
                    if (team != null && model.CoachEnabled == true && coach != null && user.TwoFactorEnabled)
                {
                    user.Team = team;
                    user.Coach = true;
                    //kjldsj;lekjr
                    var roleResult = await userManager.AddToRoleAsync(user, Roles.CoachRole);
                    var AthleteRoleResult = await userManager.AddToRoleAsync(user, Roles.AthleteRole);
                    if (!roleResult.Succeeded || !AthleteRoleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Coach/Team Code Invalid");
                        return RedirectToAction("Register", "Account");
                    }
                }
                var UpdateResult = await userManager.UpdateAsync(user);

                //var result = await userManager.CreateAsync(user, model.Password);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                        new { userId = user.Id, token = token }, Request.Scheme);
                emailService.SendAuthEmail(user.Email, confirmationLink);
                HttpContext.Session.SetString("Id", user.Id);
                if (result.Succeeded)
                {
                    if (user.TwoFactorEnabled == true && user.TotpEnabled == true)
                    {
                        HttpContext.Session.SetString("Id", user.Id);
                        return RedirectToAction("SetUpTotp");
                    }
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("index", "Athlete");
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
        //END REGISTER

        //RESEND EMAIL CONFIRMATION LINK
        public async Task<IActionResult> ResendConfirmation()
        {
            var user = await userManager.FindByIdAsync(HttpContext.Session.GetString("Id"));
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                    new { userId = user.Id, token = token }, Request.Scheme);
            emailService.SendAuthEmail(user.Email, confirmationLink);
            return View("ConfirmEmail");
        }

        //LOGIN
        [HttpGet]
        //add errors
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
                    if (checkResult.Succeeded && user.TwoFactorEnabled == true && user.TotpEnabled == false)
                    {
                        string TwoFactorCode = GenerateSecureCode();
                        emailService.SendTwoFactorCode(user.Email, TwoFactorCode);
                        user.Code = TwoFactorCode;
                        await userManager.UpdateAsync(user);
                        HttpContext.Session.SetString("Id", user.Id);
                        return RedirectToAction("TwoFactorConfirm", "account");
                    }
                    if (checkResult.Succeeded && user.TwoFactorEnabled == true && user.TotpEnabled == true && user.TotpConfigured == true)
                    {
                        HttpContext.Session.SetString("Id", user.Id);
                        return RedirectToAction("TotpConfirm");
                    }
                    if (checkResult.Succeeded && user.TwoFactorEnabled == true && user.TotpEnabled == true && user.TotpConfigured == false)
                    {
                        HttpContext.Session.SetString("Id", user.Id);
                        return View("SetUpTotp");
                    }
                    if (checkResult.Succeeded && user.TwoFactorEnabled == false && user.EmailConfirmed == true)
                    {
                        var signInResult = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                            model.RememberMe, true);
                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("index", "Athlete");
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
        //END LOGIN


        [Authorize(Roles = Roles.AthleteRole)]
        //LOGOUT
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Athlete");
        }
        //



        //CONFIRM EMAIL
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "Athlete");
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
        //END CONFIRM EMAIL



        //TWO FACTOR AUTHENTICATION
        [HttpGet]
        //[Authorize(Roles = Roles.AthleteRole)]
        public IActionResult TwoFactorConfirm()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = Roles.AthleteRole)]
        public async Task<IActionResult> TwoFactorConfirm(TwoFactorConfirmViewModel model)
        {
            User user = await userManager.FindByIdAsync(HttpContext.Session.GetString("Id"));
            if (user.Code == model.Code && user.EmailConfirmed == true)
            {
                await signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Athlete");
            }
            if (user.Code == model.Code && user.EmailConfirmed == false)
            {
                return RedirectToAction("ConfirmEmail", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        //END TWO FACTOR AUTHENTICATION
 


        //TOTP CONFIGURATION/SETUP
        public async Task<IActionResult> SetUpTotp(TotpData model)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            if (user.TotpConfigured == false)
            {
                HttpContext.Session.SetString("Id", user.Id);
                string randomKey = RandomString(25);
                var totpSetupGenerator = new TotpSetupGenerator();
                var totpSetup = totpSetupGenerator.Generate("SportsAdministrationApp", user.Name, randomKey, 300, 300);
                string qrCodeImageUrl = totpSetup.QrCodeImage;
                string manualEntrySetupCode = totpSetup.ManualSetupKey;
                //model.TotpSetupCode = manualEntrySetupCode;
                //model.QrCodeUrl = qrCodeImageUrl;
                user.QrCodeUrl = qrCodeImageUrl;
                user.TotpSetupCode = manualEntrySetupCode;
                user.randomKey = randomKey;
                user.TotpConfigured = true;
            }
                //to pass data into View
                TotpData dta = new TotpData
                {
                    TotpSetupCode = user.TotpSetupCode,
                    QrCodeUrl = user.QrCodeUrl
                };
            model.TotpSetupCode = user.TotpSetupCode;
            model.QrCodeUrl = user.QrCodeUrl;
                await userManager.UpdateAsync(user);
                return View(dta);
        }
        //END TOTP CONFIGURATION/SETUP


        
        //TOTP CODE VALIDATION
        [HttpGet]
        public IActionResult TotpConfirm()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> TotpConfirm(TotpConfirmViewModel model)
        {
            User user = await userManager.FindByIdAsync(HttpContext.Session.GetString("Id"));
            int code = model.Code;
            string randomKey = user.randomKey;
            bool valid = this.totpValidator.Validate(randomKey, code);
            if (valid == true && user.EmailConfirmed == true)
            {
                await signInManager.SignInAsync(user, true);
                return RedirectToAction("index", "Athlete");
            }
            if (valid == true && user.EmailConfirmed == false)
            {
                ModelState.AddModelError(string.Empty, "Please confirm your email and try again.");
            }
            if (valid == false)
            {
                ModelState.AddModelError(string.Empty, "2FA code invalid");

                return View();
            }
            return View();
        }
        //END TOTP CODE VALIDATION


      
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
        //CHANGE PASSWORD (redundant but useful)
        public IActionResult ChangePassword()
        {
            return View("ForgotPassword");
        }
        //END FORGOT PASSWORD



        //RESET PASSWORD (backend for all forgot password functionality)
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
        
        public IActionResult AccountSettings()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
