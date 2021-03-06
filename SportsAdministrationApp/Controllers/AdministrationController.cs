using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Controllers
{
    public class AdministrationController : Controller
    {
        //index for viewing athlete data (same index as home controller, needs migration to admin controller)
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GenerateTeam()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> GenerateTeam(string code)
        //{
         //   return View();
        //}
    }
}
