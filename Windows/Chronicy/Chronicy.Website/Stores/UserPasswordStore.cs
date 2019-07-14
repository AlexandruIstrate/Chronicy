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

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                string passwordHash = (string)dataRow[Columns.Password];
                return passwordHash;
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return null;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<bool> HasPasswordAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                string passwordHash = (string)dataRow[Columns.Password];
                return !string.IsNullOrEmpty(passwordHash);
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return false;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task SetPasswordHashAsync(ChronicyUser user, string passwordHash, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id),
                    new SqlParameter(Parameters.Password, passwordHash)
                });
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }
    }
}
