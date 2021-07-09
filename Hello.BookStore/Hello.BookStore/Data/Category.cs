using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Data
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Books> Books { get; set; }

    }
}
