using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsAdministrationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }



        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = user.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);


                    //await signInManager.SignInAsync(user, isPersistent: false);
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("index", "home");
                    }

                    return View("ConfirmEmailError.cshtml", model);
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                    model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);

                    //logger.Log(LogLevel.Warning, passwordResetLink);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }
    }
}
