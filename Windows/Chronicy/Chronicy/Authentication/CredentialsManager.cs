using Chronicy.Web;
using Chronicy.Web.Models;
using System;

namespace Chronicy.Authentication
{
    public class CredentialsManager : ICredentialsManager
    {
        public ChronicyWebApi WebApi { get; set; }

        public Token ApiToken => WebApi.AccessToken;

        public CredentialsManager(ChronicyWebApi api)
        {
            WebApi = api;
        }

        public void Signin(string username, string password)
        {
            WebApi.Authenticate(username, password);
        }

        public void Signout()
        {
            throw new NotImplementedException();
        }
    }
}
