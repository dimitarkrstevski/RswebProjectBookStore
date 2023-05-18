using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RswebProject.Data;
using RswebProject.Models;

namespace RswebProject.Controllers
{
    public class PHousesController : Controller
    {
        private readonly RswebProjectContext _context;

        public PHousesController(RswebProjectContext context)
        {
            _context = context;
        }

        // GET: PHouses
        public async Task<IActionResult> Index()
        {
              return _context.PHouse != null ? 
                          View(await _context.PHouse.ToListAsync()) :
                          Problem("Entity set 'RswebProjectContext.PHouse'  is null.");
        }

        // GET: PHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PHouse == null)
            {
                return NotFound();
            }

            var pHouse = await _context.PHouse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pHouse == null)
            {
                return NotFound();
            }

            return View(pHouse);
        }

        // GET: PHouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] PHouse pHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pHouse);
        }

        // GET: PHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PHouse == null)
            {
                return NotFound();
            }

            var pHouse = await _context.PHouse.FindAsync(id);
            if (pHouse == null)
            {
                return NotFound();
            }
            return View(pHouse);
        }

        // POST: PHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] PHouse pHouse)
        {
            if (id != pHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PHouseExists(pHouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pHouse);
        }

        // GET: PHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PHouse == null)
            {
                return NotFound();
            }

            var pHouse = await _context.PHouse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pHouse == null)
            {
                return NotFound();
            }

            return View(pHouse);
        }

        // POST: PHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PHouse == null)
            {
                return Problem("Entity set 'RswebProjectContext.PHouse'  is null.");
            }
            var pHouse = await _context.PHouse.FindAsync(id);
            if (pHouse != null)
            {
                _context.PHouse.Remove(pHouse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PHouseExists(int id)
        {
          return (_context.PHouse?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
