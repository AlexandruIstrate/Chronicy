using System.Threading.Tasks;

namespace Chronicy.Website.Services
{
    /// <summary>
    /// Provides operations for sending emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends a HTML email message to the specified address with the specified subject.
        /// </summary>
        /// <param name="email">The email address to send to</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="htmlMessage">The HTML message body</param>
        void SendEmail(string email, string subject, string htmlMessage);

        /// <summary>
        /// Sends a HTML email message to the specified address with the specified subject, asynchronously.
        /// </summary>
        /// <param name="email">The email address to send to</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="htmlMessage">The HTML message body</param>
        /// <returns>A task representing the send operation</returns>
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}