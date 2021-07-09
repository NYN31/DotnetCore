using Hello.BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }


        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ReviewBook> ReviewBook { get; set; }
    }
}
