using Chronicy.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace Chronicy.Web.Auth
{
    public class UserAuth
    {
        private SqlServerDatabase database;

        public UserAuth(SqlConnection connection)
        {
            database = new SqlServerDatabase(connection);
        }

        public string Authenticate(string username, SecureString password)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.AuthenticateForToken, new List<SqlParameter>
                {
                    new SqlParameter(nameof(username), username),
                    new SqlParameter(nameof(password), password.ToString())
                });
                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                // TODO: Use column names as constants
                return (string)dataRow["token"];
            }
            catch (Exception e)
            {
                throw new UserException("Could not authenticate", e);
            }
        }
    }
}
