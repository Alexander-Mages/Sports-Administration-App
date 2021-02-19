using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> master
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
    public class HomeController : Controller
    {
<<<<<<< HEAD
        //Add this when nlog support is fixed
        //private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly ApplicationDbContext dbContext;

        //visual studio reccomended this, not sure why I need to do it
        //public ApplicationDbContext ApplicationDbContext { get; }

        public HomeController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ApplicationDbContext dbContext,
                                 ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.dbContext = dbContext;
=======
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
>>>>>>> master
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            var model = dbContext.Users;
=======
            var model = _userManager.Users.ToList();
>>>>>>> master
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

<<<<<<< HEAD
        public IActionResult Details(string id)
=======
        public async Task<IActionResult> Details(string id)
>>>>>>> master
        {
            
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
<<<<<<< HEAD
                User = dbContext.User.Find(id),
=======
                User = await _userManager.FindByIdAsync(id),
>>>>>>> master
                PageTitle = "User Details"
            };
            return View(homeDetailsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
