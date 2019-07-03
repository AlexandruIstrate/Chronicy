using System;
using System.Threading;
using System.Threading.Tasks;
using Chronicy.Website.Identity;
using Microsoft.AspNetCore.Identity;

namespace Chronicy.Website.Stores
{
    public partial class UserStore : IUserPasswordStore<ChronicyUser>
    {
        public Task<string> GetPasswordHashAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ChronicyUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
