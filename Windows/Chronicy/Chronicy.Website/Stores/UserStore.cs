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
    /// <summary>
    /// Manages the service users.
    /// </summary>
    public partial class UserStore : IUserStore<ChronicyUser>
    {
        private readonly ISqlDatabase database;

        public UserStore(ISqlDatabase database)
        {
            this.database = database;
        }

        public async Task<IdentityResult> CreateAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Create, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserName, user.UserName),
                    new SqlParameter(Parameters.NormalizedUserName, user.NormalizedUserName),
                    new SqlParameter(Parameters.Email, user.Email),
                    new SqlParameter(Parameters.NormalizedEmail, user.NormalizedEmail),
                    new SqlParameter(Parameters.Phone, user.PhoneNumber),
                    new SqlParameter(Parameters.Password, user.PasswordHash)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Delete, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public void Dispose()
        {
            // No-Op
        }

        public async Task<ChronicyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, userId)
                });

                return GetUserFromDataSet(dataSet);
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return new ChronicyUser { Id = Convert.ToInt32(userId) };
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<ChronicyUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.NormalizedUserName, normalizedUserName)
                });

                return GetUserFromDataSet(dataSet);
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return new ChronicyUser { NormalizedUserName = normalizedUserName };
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> GetUserIdAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                ChronicyUser databaseUser = GetUserFromDataSet(dataSet);
                return databaseUser.Id.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return user.Id.ToString();
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<string> GetUserNameAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id)
                });

                ChronicyUser databaseUser = GetUserFromDataSet(dataSet);
                return databaseUser.UserName;
            }
            catch (IndexOutOfRangeException)
            {
                // The user does not exist
                return user.UserName;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task SetNormalizedUserNameAsync(ChronicyUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ChronicyUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.UserID, user.Id),
                    new SqlParameter(Parameters.UserName, user.UserName),
                    new SqlParameter(Parameters.NormalizedUserName, user.NormalizedUserName),
                    new SqlParameter(Parameters.Email, user.Email),
                    new SqlParameter(Parameters.NormalizedEmail, user.NormalizedEmail),
                    new SqlParameter(Parameters.Phone, user.PhoneNumber),
                    new SqlParameter(Parameters.Password, user.PasswordHash)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        private ChronicyUser GetUserFromDataSet(DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables[0];
            DataRow dataRow = dataTable.Rows[0];

            return new ChronicyUser
            {
                Id                  = Convert.ToInt32(dataRow[Columns.UserID]),
                UserName            = (string)dataRow[Columns.UserName],
                NormalizedUserName  = (string)dataRow[Columns.NormalizedUserName],
                Email               = (string)dataRow[Columns.Email],
                NormalizedEmail     = (string)dataRow[Columns.NormalizedEmail],
                PhoneNumber         = dataRow[Columns.Phone].ToString(),    // TODO: Temporary
                PasswordHash        = (string)dataRow[Columns.Password]
            };
        }

        public static class Parameters
        {
            public const string UserID              = "@iduser";
            public const string UserName            = "@username";
            public const string NormalizedUserName  = "@n_username";
            public const string Email               = "@email";
            public const string NormalizedEmail     = "@n_email";
            public const string Phone               = "@phone";
            public const string Password            = "@password";
        }

        public static class Columns
        {
            public const string UserID              = "iduser";
            public const string UserName            = "username";
            public const string NormalizedUserName  = "n_username";
            public const string Email               = "email";
            public const string NormalizedEmail     = "n_email";
            public const string Phone               = "phone";
            public const string Password            = "passwordHash";
        }
    }
}
