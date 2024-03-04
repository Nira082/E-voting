using evoting.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using evoting.Models;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    public class YellowhouseController : Controller
    {
        private readonly DataContext dataContext;
        public YellowhouseController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Prefect()
        {
            return View();
            //return RedirectToAction("Viceprefect");
        }

        public IActionResult Viceprefect()
        {
            return View();
            //return RedirectToAction("Viceprefect");
        }
        public IActionResult Housecaption()
        {
            return View();
            //return RedirectToAction("Viceprefect");
        }
        public IActionResult Housevicecaption()
        {
            return View();
            //return RedirectToAction("Viceprefect");
        }


        [HttpPost]
        public IActionResult CreateVote(string candidate, string candidateType)
        {
            var house = new House();
            house.Username = User.Identity.Name;
            house.house = "Yellow";
            house.candidatetype = candidateType;
            house.candidate = candidate;

            // save to database
            dataContext.CreateVote(house);

            return Ok();
        }


    }
}
