using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HomeLibrary1.Models;
using HomeLibrary1.Areas.Identity.Data;

namespace HomeLibrary1.Data
{
    public class ApplicationDbContext : IdentityDbContext<HomeLibrary1User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HomeLibrary1.Models.Book> Book { get; set; }
        public DbSet<HomeLibrary1.Models.BorrowedBooks> BorrowedBooks { get; set; }
    }
}