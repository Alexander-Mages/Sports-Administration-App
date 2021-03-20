using Microsoft.AspNetCore.Authorization;
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
    //commented because features need to be anonymous for debug
    //[Authorize(Roles = Roles.CoachRole)]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Register", "Account");
        }
        //ERROR FUNCTIONALITY
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //END ERROR
    }
}
