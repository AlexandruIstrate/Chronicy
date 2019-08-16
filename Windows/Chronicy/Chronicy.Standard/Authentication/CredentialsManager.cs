﻿#if NET472

using Chronicy.Communication;
using System;

namespace Chronicy.Authentication
{
    public class CredentialsManager : ICredentialsManager
    {
        public IServerService Service { get; set; }

        public AuthenticationState State { get; private set; }

        public CredentialsManager(IServerService service)
        {
            Service = service;
            State = AuthenticationState.NotSignedIn;
        }

        public void Signin(string username, string password)
        {
            try
            {
                State = AuthenticationState.OperationPending;

                DataResult result = Service.Authenticate(username, password);

                bool success = !result.HasError;
                State = success ? AuthenticationState.SignedIn : AuthenticationState.Errored;
            }
            catch (Exception e)
            {
                State = AuthenticationState.Errored;
                throw new AuthenticationException("Could not authenticate", e);
            }
        }

        public void Signout()
        {
            State = AuthenticationState.SignedOut;
        }
    }
}

#endif