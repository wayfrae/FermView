using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FermView.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FermView.Controllers
{
    public class HomeController : Controller
    {
        private readonly BrewsContext _context;

        public HomeController(BrewsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            ViewData["Message"] = "The page that shows the dashboard.";
            using (BrewsContext context = new BrewsContext(new DbContextOptions<BrewsContext>()))
            {

            }

            return View(_context);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult RegisterDevice(string setupCode, string name, string owner)
        {
            if(string.IsNullOrWhiteSpace(setupCode)) return new JsonResult(false);
            Device device = _context.Devices.FirstOrDefault(x => x.SetupCode == setupCode.ToUpper());
            if (device == null) return new JsonResult(false);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Controller";
            }
            device.Owner = owner;
            device.Name = name;
            device.SetupCode = "";
            _context.SaveChanges();
            return new JsonResult(true);
        }
    }
}
