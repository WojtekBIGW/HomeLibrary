using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeLibrary1.Data;
using HomeLibrary1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using HomeLibrary1.Areas.Identity.Data;

namespace HomeLibrary1.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly UserManager<HomeLibrary1User> _userManager;
        private readonly ApplicationDbContext _context;
        public BorrowedBooksController(ApplicationDbContext context)
        {
            _userManager = _userManager;
            _context = context;
        }
        [Authorize]
        // GET: BorrowedBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BorrowedBooks.Include(b => b.Book).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        /*// GET: BorrowedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBooks = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return View(borrowedBooks);
        }

        // GET: BorrowedBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BorrowedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId,BorrowDate,ReturnDate")] BorrowedBooks borrowedBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowedBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", borrowedBooks.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            return View(borrowedBooks);
        }

        // GET: BorrowedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", borrowedBooks.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            return View(borrowedBooks);
        }

        // POST: BorrowedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,UserId,BorrowDate,ReturnDate")] BorrowedBooks borrowedBooks)
        {
            if (id != borrowedBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBooksExists(borrowedBooks.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", borrowedBooks.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            return View(borrowedBooks);
        }

        // GET: BorrowedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBooks = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return View(borrowedBooks);
        }

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowedBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BorrowedBooks'  is null.");
            }
            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBooks != null)
            {
                _context.BorrowedBooks.Remove(borrowedBooks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBooksExists(int id)
        {
          return _context.BorrowedBooks.Any(e => e.Id == id);
        }*/
    }
}
