using System;

namespace Chronicy.Website.Pages.Account
{
    public class LoginException : Exception
    {
        public LoginException(string username) 
            : base($"Could not login user { username }") { }

        public LoginException(string username, string message) 
            : base($"Could not login user { username }. { message }") { }

        public LoginException(string username, string message, Exception innerException) 
            : base($"Could not login user { username }. { message }", innerException) { }
    }
}
