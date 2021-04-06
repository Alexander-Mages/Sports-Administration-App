using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    [Authorize(Roles = Roles.CoachRole)]
    public class CoachController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IEmailService emailService;

        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public CoachController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ApplicationDbContext dbContext,
                                 IEmailService emailService,

                                 IConfiguration configuration,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.emailService = emailService;

        }
        //PRIVATE METHODS
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
        [HttpGet]
        public async Task<IActionResult> AddTime(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddTime(string id, string time)
        {
            var user = await userManager.FindByIdAsync(id);
            user.PersonalRecord.AthleteData.Add(new AthleteData() { Time = Convert.ToDecimal(time) });
            //add result check
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                //fix this
                return View(user);
            }
            return RedirectToAction("UserListWithData");
        }

        public IActionResult Index()
        {
            return View();
        }
        ////INVITE NEW Athlete
        //[HttpGet]
        //public IActionResult InviteAthlete()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult InviteAthlete(InviteAthleteViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string code = RandomString(10);
        //        emailService.SendAthleteInvite(model.AthleteEmail, code);
        //        return View("AthleteInviteSuccess");
        //    }
        //    return View(model);
        //}
        ////END INVITE NEW Athlete
    }
}
