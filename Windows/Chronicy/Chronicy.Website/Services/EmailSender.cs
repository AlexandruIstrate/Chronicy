using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Chronicy.Website.Services
{
    /// <summary>
    /// The default implementation of <see cref="Chronicy.Website.Services.IEmailSender"/>.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public EmailSender(string host, NetworkCredential networkCredentials)
        {
            client = new SmtpClient(host)
            {
                UseDefaultCredentials = false,
                Credentials = networkCredentials
            };
        }

        public void SendEmail(string email, string subject, string htmlMessage)
        {
            client.Send(BuildMailMessage(email, subject, htmlMessage));
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await client.SendMailAsync(BuildMailMessage(email, subject, htmlMessage));
        }

        private MailMessage BuildMailMessage(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress("from"),
                Subject = subject,
                Body = htmlMessage
            };
            message.To.Add(new MailAddress(email));

            return message;
        }

        private readonly SmtpClient client;
    }
}
