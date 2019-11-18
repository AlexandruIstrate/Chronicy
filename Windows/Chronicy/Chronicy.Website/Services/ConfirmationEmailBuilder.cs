using System.Text.Encodings.Web;

namespace Chronicy.Website.Services
{
    public class ConfirmationEmailBuilder : IEmailBuilder
    {
        public string Build(string callbackUrl)
        {
            return $"Please confirm your account by <a href='{ HtmlEncoder.Default.Encode(callbackUrl) }'>clicking here</a>.";
        }
    }
}
