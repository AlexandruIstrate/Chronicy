using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Chronicy.Website.Services
{
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

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("from");
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = htmlMessage;

            await client.SendMailAsync(message);
        }

        private readonly SmtpClient client;
    }
}
