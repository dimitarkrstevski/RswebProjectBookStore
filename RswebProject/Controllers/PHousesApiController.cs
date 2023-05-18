using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RswebProject.Data;
using RswebProject.Models;

namespace RswebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PHousesApiController : ControllerBase
    {
        private readonly RswebProjectContext _context;

        public PHousesApiController(RswebProjectContext context)
        {
            _context = context;
        }

        // GET: api/PHousesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PHouse>>> GetPHouse()
        {
          if (_context.PHouse == null)
          {
              return NotFound();
          }
            return await _context.PHouse.ToListAsync();
        }

        // GET: api/PHousesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PHouse>> GetPHouse(int id)
        {
          if (_context.PHouse == null)
          {
              return NotFound();
          }
            var pHouse = await _context.PHouse.FindAsync(id);

            if (pHouse == null)
            {
                return NotFound();
            }

            return pHouse;
        }

        // PUT: api/PHousesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPHouse(int id, PHouse pHouse)
        {
            if (id != pHouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(pHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PHouseExists(id))
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

        // POST: api/PHousesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PHouse>> PostPHouse(PHouse pHouse)
        {
          if (_context.PHouse == null)
          {
              return Problem("Entity set 'RswebProjectContext.PHouse'  is null.");
          }
            _context.PHouse.Add(pHouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPHouse", new { id = pHouse.Id }, pHouse);
        }

        // DELETE: api/PHousesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePHouse(int id)
        {
            if (_context.PHouse == null)
            {
                return NotFound();
            }
            var pHouse = await _context.PHouse.FindAsync(id);
            if (pHouse == null)
            {
                return NotFound();
            }

            _context.PHouse.Remove(pHouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PHouseExists(int id)
        {
            return (_context.PHouse?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
