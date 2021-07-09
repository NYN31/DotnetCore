using Hello.BookStore.Models;
using System.Threading.Tasks;

namespace Hello.BookStore.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}