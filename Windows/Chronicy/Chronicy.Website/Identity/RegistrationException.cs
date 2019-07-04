using System;

namespace Chronicy.Website.Areas.Identity.Pages.Account
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string username) 
            : base($"Could not register user { username }") { }

        public RegistrationException(string username, string message) 
            : base($"Could not register user { username }. { message }") { }

        public RegistrationException(string username, string message, Exception innerException) 
            : base($"Could not register user { username }. { message }", innerException) { }
    }
}
