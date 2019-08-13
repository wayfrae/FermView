using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FermView.Models;

namespace FermView.Controllers
{
    [Produces("application/json")]
    [Route("api/Brews")]
    public class BrewsController : Controller
    {
        private readonly BrewsContext _context;

        public BrewsController(BrewsContext context)
        {
            _context = context;
        }

        // GET: api/Brews
        [HttpGet]
        public IEnumerable<Brew> GetBrew()
        {
            return _context.Brews;
        }

        [HttpGet("GetUserBrews/{userName}")]
        public IEnumerable<Brew> GetUserBrews(string userName)
        {
            return _context.Brews.Where(x=>x.Username == userName);
        }

        // GET: api/Brews/{device guid}
        [HttpGet("{deviceId}")]
        public ActionResult<Brew> GetBrewForDevice(Guid? deviceId)
        {
            var brew = _context.Brews.FirstOrDefault(x => x.DeviceId == deviceId);
            if (brew == null) return NotFound("A brew assigned to this device was not found.");
            return Ok(brew);
        }

        // GET: api/Brews/5/CurrentTarget
        [HttpGet("{deviceId}/CurrentTarget")]
        public ActionResult<TempPeriod> GetTargetTemp([FromRoute] Guid? deviceId)
        {
            if (deviceId == null) return BadRequest("ID is null");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brew = _context.Brews.FirstOrDefault(x => x.DeviceId == deviceId);

            if (brew == null) return NotFound();
            if (brew.Profile == null) return NotFound("No profile is set to this brew.");
            if (brew.Profile == null) return NotFound("No temperatures are set to the profile set to this brew.");

            if (brew.StartDate == DateTime.MinValue)
            {
                brew.StartDate = DateTime.Now;
                _context.SaveChanges();
            }
            var timePassed = brew.StartDate - DateTime.Now;
            int totalDuration = 0; 
            foreach(var temp in brew.Profile.Details)
            {
                totalDuration += ((int)temp.Duration);
                if (totalDuration >= timePassed.Hours) 
                {
                    return Ok(temp);
                }
            }
            return Ok(brew.Profile.Details.Last());
        }

        // GET: api/Brews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrew([FromRoute] Guid? id)
        {
            if (id == null) return BadRequest("ID is null");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brew = await _context.Brews.SingleOrDefaultAsync(m => m.ID == id);

            if (brew == null)
            {
                return NotFound();
            }

            return Ok(brew);
        }

        // PUT: api/Brews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrew([FromRoute] Guid id, [FromBody] Brew brew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brew.ID)
            {
                return BadRequest();
            }

            _context.Entry(brew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Brews
        [HttpPost]
        public async Task<IActionResult> PostBrew([FromBody] Brew brew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Brews.Add(brew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrew", new { id = brew.ID }, brew);
        }

        // DELETE: api/Brews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrew([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brew = await _context.Brews.SingleOrDefaultAsync(m => m.ID == id);
            if (brew == null)
            {
                return NotFound();
            }

            _context.Brews.Remove(brew);
            await _context.SaveChangesAsync();

            return Ok(brew);
        }

        private bool BrewExists(Guid id)
        {
            return _context.Brews.Any(e => e.ID == id);
        }
    }
}