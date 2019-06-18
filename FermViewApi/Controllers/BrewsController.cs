using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FermViewApi.Models;

namespace FermViewApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Brews")]
    public class BrewsController : Controller
    {
        private readonly TemperatureDataContext _context;

        public BrewsController(TemperatureDataContext context)
        {
            _context = context;
        }

        // GET: api/Brews
        [HttpGet]
        public IEnumerable<Brew> GetBrew()
        {
            return _context.Brew;
        }

        // GET: api/Brews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrew([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brew = await _context.Brew.SingleOrDefaultAsync(m => m.ID == id);

            if (brew == null)
            {
                return NotFound();
            }

            return Ok(brew);
        }

        // PUT: api/Brews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrew([FromRoute] int id, [FromBody] Brew brew)
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

            _context.Brew.Add(brew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrew", new { id = brew.ID }, brew);
        }

        // DELETE: api/Brews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrew([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brew = await _context.Brew.SingleOrDefaultAsync(m => m.ID == id);
            if (brew == null)
            {
                return NotFound();
            }

            _context.Brew.Remove(brew);
            await _context.SaveChangesAsync();

            return Ok(brew);
        }

        private bool BrewExists(int id)
        {
            return _context.Brew.Any(e => e.ID == id);
        }
    }
}