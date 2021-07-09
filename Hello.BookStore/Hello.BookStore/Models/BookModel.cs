using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter the title of your book")]
        public string Author { get; set; }
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string Category { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name = "Choose the pdf of your book")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

    }
}
