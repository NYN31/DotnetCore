using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Data
{
    public class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public int TotalPages { get; set; }
        public string CoverImageUrl { get; set; }
        public string BookPdfUrl { get; set; }

        public Language Language { get; set; }
        public Category Category { get; set; }
    }
}
