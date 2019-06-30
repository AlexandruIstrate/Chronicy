using System.Collections.Generic;

namespace Chronicy.Web.Auth
{
    public class UserInfo
    {
        public string Username { get; }
        public List<string> ActiveTokens { get; }
        public List<string> ActiveLogins { get; }

        public bool HasActiveTokens => (ActiveTokens.Count > 0);
        public bool HasActiveLogins => (ActiveLogins.Count > 0);
    }
}
