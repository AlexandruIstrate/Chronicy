using Chronicy.Sql;
using Chronicy.Web.Models;
using System;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public class AuthenticationApi : IAuthentication
    {
        private readonly SqlServerDatabase database;

        public AuthenticationApi(SqlServerDatabase database)
        {
            this.database = database;
        }

        public Token Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Token> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
