using HomeLibrary1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HomeLibrary1.Models
{
    public class BorrowedBooks
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [Display(Name = "Id książki")]
        public Book Book { get; set; }
        [Display(Name = "Użytkownik")]
        public string? UserId { get; set; }
        [Display(Name = "Użytkownik")]
        public HomeLibrary1User? User { get; set; }
        [Display(Name ="Data wypożyczenia")]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
