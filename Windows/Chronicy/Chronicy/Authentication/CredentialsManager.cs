using Chronicy.Communication;
using System;

namespace Chronicy.Authentication
{
    public class CredentialsManager : ICredentialsManager
    {
        public IServerService Service { get; set; }

        public CredentialsManager(IServerService service)
        {
            Service = service;
        }

        public void Signin(string username, string password)
        {
            Service.Authenticate(username, password);
        }

        public void Signout()
        {
            throw new NotImplementedException();
        }
    }
}
