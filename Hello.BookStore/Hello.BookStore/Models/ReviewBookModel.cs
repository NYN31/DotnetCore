using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Models
{
    public class ReviewBookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the Book Id number")]
        [Display(Name = "Enter Book Id")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please enter the Book Name")]
        [Display(Name = "Enter Book Name")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [Display(Name = "Enter your name")]
        public string ReviewerName { get; set; }
        public string ReviewTime { get; set; }
        [Required(ErrorMessage = "Please enter the description")]
        [Display(Name = "Review")]
        public string BookReviewDetails { get; set; }
    }
}
