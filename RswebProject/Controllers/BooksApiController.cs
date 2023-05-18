using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RswebProject.Data;
using RswebProject.Models;
using RswebProject.ViewModels;
using Microsoft.Build.Framework;
using System.Numerics;

namespace RswebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly RswebProjectContext _context;

        public BooksApiController(RswebProjectContext context)
        {
            _context = context;
        }

        // GET: api/BooksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook(string? bookGenre, string? searchString)
        {
            IQueryable<Book> books = _context.Book.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre.Contains(bookGenre));
            }
            books = books.Include(m => m.Author);
            return await books.ToListAsync();
        }

        // GET: api/BooksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Book == null)
          {
              return NotFound();
          }
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // GET: api/BooksApi
        [HttpGet("{id}/GetPHouses")]
        public async Task<IActionResult> GetPHousesInBook([FromRoute] int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var phousebooks = _context.BookPHouses.Where(m => m.BookId == id).ToList();
            List<PHouse> phouses = new List<PHouse>();
            foreach (var phouse in phousebooks)
            {
                PHouse newphouse = _context.PHouse.Where(m => m.Id == phouse.PHouseId).FirstOrDefault();
                newphouse.Books = null;
                phouses.Add(newphouse);
            }
            return Ok(phouses);
        }

        // PUT: api/BooksApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] BookPHouseEditViewModel model)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (id != model.Book.Id) { return BadRequest(); }

            _context.Entry(model.Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                IEnumerable<int> newPHouseList = model.SelectedPHouse;
                IEnumerable<int> prevPHouseList = _context.BookPHouses.Where(s => s.BookId == id).Select(s => s.PHouseId);
                IQueryable<BookPHouse> toBeRemoved = _context.BookPHouses.Where(s => s.BookId == id);
                if (newPHouseList != null)
                {
                    toBeRemoved = toBeRemoved.Where(s => !newPHouseList.Contains(s.PHouseId));
                    foreach (int phouseId in newPHouseList)
                    {
                        if (!prevPHouseList.Any(s => s == phouseId))
                        {
                            _context.BookPHouses.Add(new BookPHouse { PHouseId = phouseId, BookId = id });
                        }
                    }
                }
                _context.BookPHouses.RemoveRange(toBeRemoved);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/BooksApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Book == null)
          {
              return Problem("Entity set 'RswebProjectContext.Book'  is null.");
          }
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/BooksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Book == null)
            {
                return NotFound();
            }
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
