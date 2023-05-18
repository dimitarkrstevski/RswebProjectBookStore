using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RswebProject.Data;
using RswebProject.Models;
using RswebProject.ViewModels;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.IO;


namespace RswebProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly RswebProjectContext _context;

        public BooksController(RswebProjectContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<Book> books = _context.Book.AsQueryable();
            IQueryable<string> genreQuery = _context.Book.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(movieGenre))
            {
                books = books.Where(x => x.Genre == movieGenre);
            }

            books = books.Include(m => m.Author)
                .Include(m => m.PHouses)
                .ThenInclude(m => m.PHouse);

            var bookGenreVM = new BookGenreViewModel
            {
                Genres = new SelectList(await genreQuery.ToListAsync()),
                Books = await books.ToListAsync()
            };

            return View(bookGenreVM);

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
               .Include(m => m.Author)
               .Include(m => m.PHouses).ThenInclude(m => m.PHouse)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating,AuthorId")] Book book)
            
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", book.AuthorId);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = _context.Book.Where(m => m.Id == id).Include(m => m.PHouses).First();

            if (book == null)
            {
                return NotFound();
            }

            var phouses = _context.PHouse.AsEnumerable();
            phouses = phouses.OrderBy(s => s.FullName);

            BookPHouseEditViewModel viewmodel = new BookPHouseEditViewModel
            {
                Book = book,
                PHouseList = new MultiSelectList(phouses, "Id", "FullName"),
                SelectedPHouse = book.PHouses.Select(sa => sa.PHouseId)
            };

            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", book.AuthorId);
            return View(viewmodel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookPHouseEditViewModel viewmodel)
        {
            if (id != viewmodel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Book);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> newPHouseList = viewmodel.SelectedPHouse;
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
                    if (!BookExists(viewmodel.Book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", viewmodel.Book.AuthorId);
            return View(viewmodel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }


            var book = await _context.Book
                .Include(m => m.Author)
                .Include(m => m.PHouses).ThenInclude(m => m.PHouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'RswebProjectContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
