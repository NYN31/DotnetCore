using Hello.BookStore.Models;
using System.Threading.Tasks;

namespace Hello.BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
    }
}