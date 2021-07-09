using Hello.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}