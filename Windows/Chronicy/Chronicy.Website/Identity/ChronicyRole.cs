using Microsoft.AspNetCore.Identity;

namespace Chronicy.Website.Identity
{
    /// <summary>
    /// This class represents a Chronicy role. It inherits from IdentityRole to provide the ability of future extension
    /// </summary>
    public class ChronicyRole : IdentityRole<int>
    {
        public ChronicyRole() : base() { }

        public ChronicyRole(string roleName) : base(roleName) { }
    }
}
