using Chronicy.Sql;
using System;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public class TokenManager : ITokenManager
    {
        private readonly SqlServerDatabase database;

        public TokenManager(SqlServerDatabase database)
        {
            this.database = database;
        }

        public TokenStatus GetTokenStatus(string token)
        {
            throw new NotImplementedException();
        }

        public Task<TokenStatus> GetTokenStatusAsync(string token)
        {
            throw new NotImplementedException();
        }

        public DateTime GetExpirationDate(string token)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetExpirationDateAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
