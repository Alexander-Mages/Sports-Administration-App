using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsAdministrationApp.Models;
using SportsAdministrationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    //currently a controller for everything but account, remember to specialize it later if it gets too large
    public class HomeController : Controller
    {
        //uncomment when logger is needed, commented to supress warning
        //private readonly ILogger<HomeController> _logger;

        private readonly UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //INDEX/LIST OF ALL USERS
        public IActionResult Index()
        {
            var model = _userManager.Users.ToList();
            return View(model);
        }

        //PRIVACY POLICY
        public IActionResult Privacy()
        {
            return View();
        }

        //DETAILS OF SPECIFIC USER BY ID
        public async Task<IActionResult> Details(string id)
        {
            
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                User = await _userManager.FindByIdAsync(id),
                PageTitle = "User Details"
            };
            return View(homeDetailsViewModel);
        }

        //EDIT SPECIFIC USER DETAILS
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            EditViewModel editViewModel = new EditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Team = user.Team,
            };
            return View(editViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.Name = model.Name;
                user.Email = model.Email;
                user.Team = model.Team;

                User newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Team = model.Team,
                };

                await _userManager.UpdateAsync(newUser);
                return RedirectToAction("index");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
