using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.Services;
using SportsAdministrationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{   
    [Authorize(Roles = Roles.AdminRole)]
    public class AdministrationController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ApplicationDbContext dbContext,
                                 IConfiguration configuration,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.roleManager = roleManager;
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



        [AllowAnonymous]
        public async Task<IActionResult> DebugCreateRole(string RoleName)
        {
            //DO NOT USE THIS CODE, DEMONSTRATION ONLY
            IdentityRole role = new IdentityRole()
            {
                Name = RoleName
            };
            var result = await roleManager.CreateAsync(role);
            return RedirectToAction("Index", "Administration");
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> AddAdminToFirstUser()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult InviteCoach()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> InviteCoach()
        //{
        //    string code = RandomString(10);
            

        //}




































        //INDEX/LIST OF ALL USERS
        public IActionResult Index()
        {
            var model = userManager.Users.ToList();
            return View(model);
        }
        //END INDEX


        //DETAILS OF SPECIFIC USER BY ID
        public async Task<IActionResult> Details(string id)
        {
            DetailsViewModel DetailsViewModel = new DetailsViewModel()
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
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            EditViewModel editViewModel = new EditViewModel
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
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                user.Name = model.Name;
                user.Email = model.Email;
                //user.Team = model.Team;
                await userManager.UpdateAsync(user);
                return RedirectToAction("index");
            }
            return View();
        }
        //END EDIT

        //Delete User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Index");
            }
        }
        //END DELETE



        //ERROR FUNCTIONALITY
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //END ERROR
    }
}