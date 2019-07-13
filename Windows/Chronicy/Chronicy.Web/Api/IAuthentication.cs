using Chronicy.Web.Models;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public interface IAuthentication
    {
        /// <summary>
        /// Authenticates and returns an API token
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The token, if successful</returns>
        Token Authenticate(string username, string password);

        /// <summary>
        /// Authenticates asynchronously and returns an API token
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> containing token, if successful</returns>
        Task<Token> AuthenticateAsync(string username, string password);
    }
}
