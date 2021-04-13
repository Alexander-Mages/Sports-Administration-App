using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.Services;
using SportsAdministrationApp.ViewModels;
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
        public async Task<IActionResult> AddTime(string id, string time, string location)
        {
            var user = await userManager.FindByIdAsync(id);
            decimal Time = Convert.ToDecimal(time);
            PersonalRecord r = user.PersonalRecord;
            //AthleteData d = new AthleteData() { Location = location, Time = Time };
            //r.AthleteData.Add(d);
           // user.PersonalRecord.AthleteData.Add(d);

            decimal prevPr = user.PersonalRecord.PR;
            

            if (Time > prevPr)
            {
                //add new time to personalrecord and athletedata
                user.PersonalRecord.PR = Time;
            }

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }




        public IActionResult Index()
        {
            var user = userManager.Users.Include(u => u.PersonalRecord).ThenInclude(p => p.AthleteData).ToList();
            return View(user);
        }

        public IActionResult ManageAthletes()
        {
            var model = userManager.Users.ToList();
            return View(model);
        }
        //END INDEX



        //DETAILS OF SPECIFIC USER BY ID
        public async Task<IActionResult> UserDetails(string id)
        {
            UserDetailsViewModel DetailsViewModel = new UserDetailsViewModel()
            {
                User = await userManager.FindByIdAsync(id),
                PageTitle = "User Details"
            };
            if (id != null)
            {
                return View(DetailsViewModel);
            }
            return View("error.cshtml", DetailsViewModel);
        }
        //END DETAILS



        //EDIT SPECIFIC USER DETAILS
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            EditUserViewModel editViewModel = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                //Team = user.Team,
            };
            return View(editViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                user.Name = model.Name;
                user.Email = model.Email;
                //user.Team = model.Team;
                await userManager.UpdateAsync(user);
                return RedirectToAction("ManageAthletes");
            }
            return View();
        }
        //END EDIT

    }
}
