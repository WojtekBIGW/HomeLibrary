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
using System.Security.Claims;

namespace HomeLibrary1.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<HomeLibrary1User> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<HomeLibrary1User> userManager)
        {
            _userManager = _userManager;
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var sessions = _context.Book.Where(s => s.UserId == _userManager.GetUserId(User)).Include(s=>s.User);
            return View(await _context.Book.ToListAsync());
        }
        [Authorize]
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [Authorize]
        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Category,Isbn")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        [Authorize]
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Category,Isbn")] Book book)
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
        [Authorize]
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [Authorize]
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
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
          return _context.Book.Any(e => e.Id == id);
        }

        public UserManager<HomeLibrary1User> Get_userManager()
        {
            return _userManager;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Assign(int Id, string UserId)
        {
            using (_context)
            {
                var book = _context.Book.Find(Id);
                if (book.IsBorrowed == true)
                {
                    return View("~/Views/Home/Blad.cshtml");
                }
                else
                {
                    book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); ;
                    book.IsBorrowed = true;

                    // Dodaj nowy rekord do tabeli "BorrowedBooks"
                    var borrowedBook = new BorrowedBooks
                    {
                        BookId = book.Id,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        BorrowDate = DateTime.Now
                    };
                    _context.BorrowedBooks.Add(borrowedBook);

                    _context.SaveChanges();
                    return View("~/Views/Home/Confirm.cshtml");
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Return(int Id, string UserId)
        {
            using (_context)
            {
                var book = _context.Book.Find(Id);
                if (book.IsBorrowed == false)
                {
                    return View("~/Views/Home/Blad1.cshtml");
                }
                else
                {
                    book.UserId = UserId;
                    book.IsBorrowed = false;

                    // Znajdź i usuń wypożyczoną książkę z tabeli "BorrowedBooks"
                    var borrowedBook = _context.BorrowedBooks.Where(x => x.BookId == book.Id).FirstOrDefault();
                    _context.BorrowedBooks.Remove(borrowedBook);

                    _context.SaveChanges();
                    return View("~/Views/Home/Confirm1.cshtml");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
