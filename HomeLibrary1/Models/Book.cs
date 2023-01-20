using System.ComponentModel.DataAnnotations;
using HomeLibrary1.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HomeLibrary1.Models
{
    public class Book
    {
        [Display(Name ="ID książki")]
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        [Display(Name = "Numer ISBN")]
        public int Isbn { get; set; }
        [Display(Name = "Czy wypożyczona")]
        public bool IsBorrowed { get; set; }
        [Display(Name = "Użytkownik")]
        public string? UserId { get; set; }
        [Display(Name = "Użytkownik")]
        public HomeLibrary1User? User { get; set; }
    }
}
