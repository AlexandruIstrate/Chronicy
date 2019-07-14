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
                    new SqlParameter("@username", user.UserName),
                    new SqlParameter("@email", user.Email),
                    new SqlParameter("@phone", user.PhoneNumber),
                    new SqlParameter("@password", user.PasswordHash)
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
                    new SqlParameter("@iduser", user.Id)
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
                    new SqlParameter("@iduser", userId)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyUser user = new ChronicyUser
                {
                    Id = dataRow["iduser"].ToString(),
                    UserName = (string)dataRow["username"],
                    Email = (string)dataRow["email"],
                    PhoneNumber = (string)dataRow["phone"]
                };

                return user;
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
                    new SqlParameter("@username", normalizedUserName)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyUser user = new ChronicyUser
                {
                    Id = dataRow["userid"].ToString(),
                    UserName = (string)dataRow["username"],
                    Email = (string)dataRow["email"],
                    PhoneNumber = (string)dataRow["phone"]
                };

                return user;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.Read, new List<SqlParameter>
                {
                    new SqlParameter("@username", user.UserName)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow["userid"];
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public Task<string> GetUserNameAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedUserNameAsync(ChronicyUser user, string normalizedName, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter("@iduser", user.Id),
                    new SqlParameter("@username", user.UserName),
                    new SqlParameter("@email", user.Email),
                    new SqlParameter("@phone", user.PhoneNumber),
                    new SqlParameter("@password", user.PasswordHash)
                });
            }
            catch (Exception)
            {
                // Handle
                throw;
            }
        }

        public async Task SetUserNameAsync(ChronicyUser user, string userName, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter("@iduser", user.Id),
                    new SqlParameter("@username", user.UserName),
                    new SqlParameter("@email", user.Email),
                    new SqlParameter("@phone", user.PhoneNumber),
                    new SqlParameter("@password", user.PasswordHash)
                });
            }
            catch (Exception)
            {
                // Handle
                throw;
            }
        }

        public async Task<IdentityResult> UpdateAsync(ChronicyUser user, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.User.Update, new List<SqlParameter>
                {
                    new SqlParameter("@iduser", user.Id),
                    new SqlParameter("@username", user.UserName),
                    new SqlParameter("@email", user.Email),
                    new SqlParameter("@phone", user.PhoneNumber),
                    new SqlParameter("@password", user.PasswordHash)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }
    }
}
