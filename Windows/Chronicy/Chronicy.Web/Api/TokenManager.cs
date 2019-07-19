using Chronicy.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public class TokenManager : ITokenManager
    {
        private readonly SqlServerDatabase database;

        public TokenManager(SqlServerDatabase database)
        {
            this.database = database;
        }

        public TokenStatus GetTokenStatus(string token)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.CheckToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Token, token)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                bool valid = (bool)dataRow[Columns.IsValid];
                DateTime expirationDate = (DateTime)dataRow[Columns.ExpirationDate];

                if (valid)
                {
                    return TokenStatus.Valid;
                }

                if (expirationDate < DateTime.Now)
                {
                    return TokenStatus.Expired;
                }

                return TokenStatus.Invalid;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TokenStatus> GetTokenStatusAsync(string token)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.CheckToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Token, token)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                bool valid = (bool)dataRow[Columns.IsValid];
                DateTime expirationDate = (DateTime)dataRow[Columns.ExpirationDate];

                if (valid)
                {
                    return TokenStatus.Valid;
                }

                if (expirationDate < DateTime.Now)
                {
                    return TokenStatus.Expired;
                }

                return TokenStatus.Invalid;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DateTime GetExpirationDate(string token)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.User.CheckToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Token, token)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                DateTime expirationDate = (DateTime)dataRow[Columns.ExpirationDate];
                return expirationDate;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DateTime> GetExpirationDateAsync(string token)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.User.CheckToken, new List<SqlParameter>
                {
                    new SqlParameter(Parameters.Token, token)
                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                DateTime expirationDate = (DateTime)dataRow[Columns.ExpirationDate];
                return expirationDate;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private class Parameters
        {
            public const string Token = "@token";
        }

        private class Columns
        {
            public const string IsValid = "isAuth";
            public const string ExpirationDate = "expDateTime";
        }
    }
}
