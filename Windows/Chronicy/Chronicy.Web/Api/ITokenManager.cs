using System;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public interface ITokenManager
    {
        TokenStatus GetTokenStatus(string token);
        Task<TokenStatus> GetTokenStatusAsync(string token);

        DateTime GetExpirationDate(string token);
        Task<DateTime> GetExpirationDateAsync(string token);
    }

    public enum TokenStatus
    {
        Valid, Invalid, Expired
    }
}
