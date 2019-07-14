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
    public class RoleStore : IRoleStore<ChronicyRole>
    {
        private readonly ISqlDatabase database;

        public RoleStore(ISqlDatabase database)
        {
            this.database = database;
        }

        public async Task<IdentityResult> CreateAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Role.Create, new List<SqlParameter>
                {
                    new SqlParameter("@name", role.Name)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Role.Delete, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id)
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

        public async Task<ChronicyRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Role.Read, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", roleId)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyRole role = new ChronicyRole
                {
                    Id = dataRow["idrole"].ToString(),
                    Name = (string)dataRow["name"]
                };

                return role;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<ChronicyRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Role.Read, new List<SqlParameter>
                {
                    new SqlParameter("@name", normalizedRoleName)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyRole role = new ChronicyRole
                {
                    Id = dataRow["idrole"].ToString(),
                    Name = (string)dataRow["name"]
                };

                return role;
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<string> GetNormalizedRoleNameAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Role.Read, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow["name"];
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<string> GetRoleIdAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Role.Read, new List<SqlParameter>
                {
                    new SqlParameter("@name", role.Name)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return dataRow["idrole"].ToString();
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<string> GetRoleNameAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.Role.Read, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow["name"];
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task SetNormalizedRoleNameAsync(ChronicyRole role, string normalizedName, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Role.Update, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id),
                    new SqlParameter("@name", normalizedName)
                });
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task SetRoleNameAsync(ChronicyRole role, string roleName, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Role.Update, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id),
                    new SqlParameter("@name", roleName)
                });
            }
            catch (Exception)
            {
                // TODO: Handle
                throw;
            }
        }

        public async Task<IdentityResult> UpdateAsync(ChronicyRole role, CancellationToken cancellationToken)
        {
            try
            {
                await database.RunNonQueryProcedureAsync(SqlProcedures.Role.Update, new List<SqlParameter>
                {
                    new SqlParameter("@idrole", role.Id),
                    new SqlParameter("@name", role.Name)
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
