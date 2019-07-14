using Microsoft.AspNetCore.Identity;

namespace Chronicy.Website.Identity
{
    /// <summary>
    /// This class represents a Chronicy user. It inherits from IdentityUser to provide the ability of future extension
    /// </summary>
    public class ChronicyUser : IdentityUser<int>
    {
        public ChronicyUser() : base() { }
        
        public ChronicyUser(string userName) : base(userName) { }
    }
}
