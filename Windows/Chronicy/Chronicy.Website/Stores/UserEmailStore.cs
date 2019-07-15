using Chronicy.Sql;
using Chronicy.Website.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Chronicy.Website.Stores
{
    public partial class UserStore : IUserEmailStore<ChronicyUser>
    {
        public async Task<ChronicyUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Email, normalizedEmail)
                });

                return GetUserFromDataSet(dataSet);
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                //return null;
                return new ChronicyUser();
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task<string> GetEmailAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetEmailAsync(ChronicyUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ChronicyUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(ChronicyUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }
    }
}
