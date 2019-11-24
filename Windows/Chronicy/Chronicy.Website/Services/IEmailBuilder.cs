namespace Chronicy.Website.Services
{
    /// <summary>
    /// Provides a set of operations for building email messages.
    /// </summary>
    public interface IEmailBuilder
    {
        /// <summary>
        /// Builds an email using an URL that can be used for a callback.
        /// </summary>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <returns>The HTML email</returns>
        string Build(string callbackUrl);
    }
}
