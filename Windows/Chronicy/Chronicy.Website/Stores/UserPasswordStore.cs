using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Chronicy.Sql;
using Chronicy.Website.Identity;
using Microsoft.AspNetCore.Identity;

namespace Chronicy.Website.Stores
{
    public partial class UserStore : IUserPasswordStore<ChronicyUser>
    {
        public async Task<string> GetPasswordHashAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                ChronicyUser databaseUser = GetUserFromDataSet(dataSet);
                return databaseUser.PasswordHash;
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return user.PasswordHash;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task<bool> HasPasswordAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(ChronicyUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
    }
}
