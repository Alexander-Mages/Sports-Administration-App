using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    [Authorize(Roles = Roles.AthleteRole)]

    public class AthleteController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IEmailService emailService;

        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AthleteController(UserManager<User> userManager,
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
        //rename that
        public IActionResult UserListWithData()
        {
            var user = userManager.Users.Include(u => u.PersonalRecord).ThenInclude(p => p.AthleteData).ToList();
            return View(user);
        }
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
            user.PersonalRecord.AthleteData.Add(new AthleteData() { Time = Convert.ToDecimal(time)});
            //add result check
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                //fix this
                return View(user);
            }
            return RedirectToAction("UserListWithData");
        }
    }
}
