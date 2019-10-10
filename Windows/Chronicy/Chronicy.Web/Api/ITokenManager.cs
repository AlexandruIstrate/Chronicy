using System;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    /// <summary>
    /// Provides information for the user tokens.
    /// </summary>
    public interface ITokenManager
    {
        TokenStatus GetTokenStatus(string token);
        Task<TokenStatus> GetTokenStatusAsync(string token);

        DateTime GetExpirationDate(string token);
        Task<DateTime> GetExpirationDateAsync(string token);
    }

    /// <summary>
    /// Represents the status of the token.
    /// </summary>
    public enum TokenStatus
    {
        Valid, Invalid, Expired
    }
}
