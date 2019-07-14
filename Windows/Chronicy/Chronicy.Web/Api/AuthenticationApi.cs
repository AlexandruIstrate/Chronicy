using Chronicy.Sql;
using Chronicy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public class AuthenticationApi : IAuthentication
    {
        private readonly SqlServerDatabase database;

        public AuthenticationApi(SqlServerDatabase database)
        {
            this.database = database;
        }

        public Token Authenticate(string username, string password)
        {
            database.RunScalarProcedure(SqlProcedures.AuthenticateForToken, new List<SqlParameter>
            {
                
            });

            throw new NotImplementedException();
        }

        public async Task<Token> AuthenticateAsync(string username, string password)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.AuthenticateForToken, new List<SqlParameter>
                {

                });

                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];
            }
            catch (Exception e)
            {
                throw new Exception("Could not authenticate", e);
            }

            throw new NotImplementedException();
        }
    }
}
