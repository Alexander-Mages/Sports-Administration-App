using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsAdministrationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    [Authorize(Roles = Roles.AthleteRole)]

    public class AthleteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
