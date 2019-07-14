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
                    new SqlParameter(Parameters.Name, role.Name)
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
                    new SqlParameter(Parameters.RoleID, role.Id)
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
                    new SqlParameter(Parameters.RoleID, roleId)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyRole role = new ChronicyRole
                {
                    Id = Convert.ToInt32(dataRow[Columns.RoleID]),
                    Name = (string)dataRow[Columns.Name]
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
                    new SqlParameter(Parameters.Name, normalizedRoleName)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                ChronicyRole role = new ChronicyRole
                {
                    Id = Convert.ToInt32(dataRow[Columns.RoleID]),
                    Name = (string)dataRow[Columns.Name]
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
                    new SqlParameter(Parameters.RoleID, role.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.Name];
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
                    new SqlParameter(Parameters.Name, role.Name)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return dataRow[Columns.RoleID].ToString();
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
                    new SqlParameter(Parameters.RoleID, role.Id)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                return (string)dataRow[Columns.Name];
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
                    new SqlParameter(Parameters.RoleID, role.Id),
                    new SqlParameter(Parameters.Name, normalizedName)
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
                    new SqlParameter(Parameters.RoleID, role.Id),
                    new SqlParameter(Parameters.Name, roleName)
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
                    new SqlParameter(Parameters.RoleID, role.Id),
                    new SqlParameter(Parameters.Name, role.Name)
                });

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed();
            }
        }
    }

    public static class Parameters
    {
        public const string RoleID  = "@iduser";
        public const string Name    = "@username";
    }

    public static class Columns
    {
        public const string RoleID  = "@iduser";
        public const string Name    = "@username";
    }
}
