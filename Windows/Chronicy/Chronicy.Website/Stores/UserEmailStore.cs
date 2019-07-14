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
                return null;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<string> GetEmailAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                string email = (string)dataRow[Columns.Email];
                return email;
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

        public Task<bool> GetEmailConfirmedAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedEmailAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                string email = (string)dataRow[Columns.NormalizedEmail];
                return email;
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

        public async Task SetEmailAsync(ChronicyUser user, string email, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id),
                    new SqlParameter(Parameters.Email, email)
                });
            }
            catch (Exception)
            {
                // Handle
                throw;
            }
        }

        public Task SetEmailConfirmedAsync(ChronicyUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedEmailAsync(ChronicyUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id),
                    new SqlParameter(Parameters.NormalizedEmail, normalizedEmail)
                });
            }
            catch (Exception)
            {
                // Handle
                throw;
            }
        }
    }
}
