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
    //THIS CONTROLLER IS ENTIRELY REDUNDANT, REMOVE WHEN SPECIALIZING THE CONTROLLERS
    public class HomeController : Controller
    {
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
        //END INDEX


        //DETAILS OF SPECIFIC USER BY ID
        public async Task<IActionResult> Details(string id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                User = await _userManager.FindByIdAsync(id),
                PageTitle = "User Details"
            };
            if (id != null)
            {
                return View(homeDetailsViewModel);
            }
            return View("error.cshtml", homeDetailsViewModel);
        }
        //END DETAILS



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
                var user = await _userManager.FindByIdAsync(model.Id);
                user.Name = model.Name;
                user.Email = model.Email;
                //user.Team = model.Team;
                await _userManager.UpdateAsync(user);
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
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
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
