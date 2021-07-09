using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Data
{
    public class ReviewBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewTime { get; set; }
        public string BookReviewDetails { get; set; }

    }
}
