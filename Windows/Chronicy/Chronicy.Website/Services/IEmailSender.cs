using System.Threading.Tasks;

namespace Chronicy.Website.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}