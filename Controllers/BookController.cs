using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookClub.Contexsts;
using BookClub.Models;

namespace BookClub.Controllers
{
    public class BookController : Controller
    {
        private readonly BookClubContext _context;

        public BookController(BookClubContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks() => Ok( await _context.Books.ToListAsync());

        [HttpGet("user-books/{Id}")]
        public async Task<IActionResult> GetUserBooks(int id)
        {
            var user = await _context.Users.Include(u => u.ReadBooks).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ReadBooks);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookForUser(int userId, int bookId)
        {
            var user = await _context.Users.Include(u => u.ReadBooks).FirstOrDefaultAsync(u =>  userId == u.Id);
            var book = await _context.Books.FindAsync(bookId);
            if (user == null || book == null)
            {
                return NotFound();
            }

            user.ReadBooks.Add(book);
            await _context.SaveChangesAsync();
            return Ok();

        }


        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBookFromUser(int userId, int bookId)
        {
            var user = await _context.Users.Include(u => u.ReadBooks).FirstOrDefaultAsync(u => userId == u.Id);
            var book = user.ReadBooks.FirstOrDefault(u => user.Id == bookId);
            if (book == null || user == null)
            {
                return NotFound();
            }
            user.ReadBooks.Remove(book);

            await _context.SaveChangesAsync();
            return Ok();
        }

        /*
        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Books != null ? 
                          View(await _context.Books.ToListAsync()) :
                          Problem("Entity set 'BookClubContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookClubContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
