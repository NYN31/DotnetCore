using Hello.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetCategory();
    }
}