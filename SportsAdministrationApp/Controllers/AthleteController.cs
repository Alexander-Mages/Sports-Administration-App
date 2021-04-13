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
        public IActionResult Index()
        {
            var user = userManager.Users.Include(u => u.PersonalRecord).ThenInclude(p => p.AthleteData).ToList();
            return View(user);
        }

    }
}
