using System;

namespace Chronicy.Web.Auth
{
    public class TokenManager
    {
        public TokenStatus GetTokenStatus(string token)
        {
            throw new NotImplementedException();
        }

        public DateTime GetExpirationDate(string token)
        {
            throw new NotImplementedException();
        }
    }

    public enum TokenStatus
    {
        Valid, Invalid, Expired
    }
}
