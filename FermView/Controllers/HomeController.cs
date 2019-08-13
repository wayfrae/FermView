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
using Newtonsoft.Json;
using SQLitePCL;
using Newtonsoft.Json.Linq;

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

        public IActionResult Brew(Guid id)
        {
            ViewData["id"] = id;
            return View(_context);
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            ViewData["Message"] = "The page that shows the dashboard.";
            return View(_context);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult RegisterDevice(string setupCode, string name, string owner)
        {
            if(string.IsNullOrWhiteSpace(setupCode)) return BadRequest("You must fill out the setup code.");
            if(string.IsNullOrWhiteSpace(name)) return BadRequest("You must enter a name.");
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
            return Ok();
        }

        public ActionResult CreateProfile(Guid userId, string profileName, string description, string json)
        {
            if (userId == Guid.Empty) return BadRequest("The user Id was not sent.");
            if (string.IsNullOrWhiteSpace(profileName)) return BadRequest("You must enter a profile name.");
            if (string.IsNullOrWhiteSpace(json)) return BadRequest("You must set at least one temperature.");

            var tempPeriods = new List<TempPeriod>();
            JArray jObj = JArray.Parse(json);
            foreach (JObject o in jObj.Children<JObject>())
            {
                var temp = 0m;
                var duration = 0m;
                foreach (JProperty p in o.Properties())
                {
                    
                    if (p.Name.Equals("temp"))
                    {
                        decimal.TryParse(p.Value.ToString(), out temp);
                    }

                    if (p.Name.Equals("duration"))
                    {
                        decimal.TryParse(p.Value.ToString(), out duration);
                    }
                }
                tempPeriods.Add(new TempPeriod
                {
                    Temperature = temp,
                    Duration = duration
                });
            }

            _context.Profiles.Add(new Profile
            {
                Creator = userId,
                Name = profileName,
                Description = description,
                Hearts = 0,
                Details = tempPeriods
            });
            _context.SaveChanges();
            return Ok();
        }

        public ActionResult<List<Profile>> GetProfiles(Guid userId)
        {
            return Ok(_context.Profiles.Where(x => x.Creator == userId));
        }

        public ActionResult<List<Profile>> GetDevices(string userName)
        {
            return Ok(_context.Devices.Where(x => x.Owner == userName));
        }

        public ActionResult CreateBrew(string userName, string brewName, Guid profileId, Guid deviceId)
        {
            if (profileId == Guid.Empty) return BadRequest("A profile must be selected.");
            if (string.IsNullOrWhiteSpace(brewName)) return BadRequest("You must enter a brew name.");
            if (string.IsNullOrWhiteSpace(userName)) return BadRequest("The user name was not sent.");
            var existingBrew = _context.Brews.FirstOrDefault(x => x.DeviceId == deviceId);
            var startDate = DateTime.MinValue;
            if (existingBrew != null)
            {
                existingBrew.DeviceId = Guid.Empty;
                existingBrew.EndDate = DateTime.Now;
                startDate = DateTime.Now;
            }
            _context.Brews.Add(new Brew
            {
                Profile = _context.Profiles.Find(profileId),
                BrewName = brewName,
                Username = userName,
                DeviceId = deviceId,
                StartDate = startDate
            });
            _context.SaveChanges();
            return Ok();
        }

        public IEnumerable<Brew> GetUserBrews(string userName)
        {
            return _context.Brews.Where(x => x.Username == userName);
        }

        public ActionResult DeleteController(Guid id)
        {
            _context.Devices.Remove(_context.Devices.Find(id));
            _context.SaveChanges();
            return Ok();
        }
    }
}
