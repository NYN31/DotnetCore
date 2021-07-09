using Hello.BookStore.Data;
using Hello.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _context = null;
        public CategoryRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetCategory()
        {
            return await _context.Category.Select(x => new CategoryModel()
            {
                ID = x.ID,
                Name = x.Name,
            }).ToListAsync();
        }


/*
	

*/
    }
}
