using System.Text.Encodings.Web;

namespace Chronicy.Website.Services
{
    /// <summary>
    /// Builds an email for activating an account and confirming an email address.
    /// </summary>
    public class ConfirmationEmailBuilder : IEmailBuilder
    {
        /// <summary>
        /// Builds the account activation email.
        /// </summary>
        /// <param name="callbackUrl">The callback website URL used for activation.</param>
        /// <returns>The email message</returns>
        public string Build(string callbackUrl)
        {
            return $"Please confirm your account by <a href='{ HtmlEncoder.Default.Encode(callbackUrl) }'>clicking here</a>.";
        }
    }
}
