using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FermView.Models;

namespace FermView.Controllers
{
    [Produces("application/json")]
    [Route("api/Temps")]
    public class TempsController : Controller
    {
        private readonly BrewsContext _context;

        public TempsController(BrewsContext context)
        {
            _context = context;


            if (_context.Temperatures.Count() == 0)
            {
                // for early testing                
                _context.Temperatures.Add(new TemperatureData { Temperature = 69.69m });
                _context.SaveChanges();
            }
        }

        // GET: api/Temps
        [HttpGet]
        public IEnumerable<TemperatureData> GetTemperatureData()
        {
            return _context.Temperatures;
        }

        // GET: api/Temps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemperatureData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temperatureData = await _context.Temperatures.SingleOrDefaultAsync(m => m.ID == id);

            if (temperatureData == null)
            {
                return NotFound();
            }

            return Ok(temperatureData);
        }

        // PUT: api/Temps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemperatureData([FromRoute] int id, [FromBody] TemperatureData temperatureData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperatureData.ID)
            {
                return BadRequest();
            }

            _context.Entry(temperatureData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureDataExists(id))
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

        // POST: api/Temps
        [HttpPost]
        public async Task<IActionResult> PostTemperatureData([FromBody] TemperatureData temperatureData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Temperatures.Add(temperatureData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemperatureData", new { id = temperatureData.ID }, temperatureData);
        }

        // DELETE: api/Temps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemperatureData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temperatureData = await _context.Temperatures.SingleOrDefaultAsync(m => m.ID == id);
            if (temperatureData == null)
            {
                return NotFound();
            }

            _context.Temperatures.Remove(temperatureData);
            await _context.SaveChangesAsync();

            return Ok(temperatureData);
        }

        private bool TemperatureDataExists(int id)
        {
            return _context.Temperatures.Any(e => e.ID == id);
        }
    }
}