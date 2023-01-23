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

    }
}
