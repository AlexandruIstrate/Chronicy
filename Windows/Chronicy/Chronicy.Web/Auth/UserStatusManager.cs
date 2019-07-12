using Chronicy.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Chronicy.Web.Auth
{
    public class UserStatusManager
    {
        private SqlServerDatabase database;

        public UserStatusManager()
        {
            // TODO: SqlConnection
            database = new SqlServerDatabase(null);
        }

        public IEnumerable<UserInfo> GetInfo()
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.GetUserInfoAll, null);
                DataTable dataTable = dataSet.Tables[0];

                List<UserInfo> result = new List<UserInfo>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // Get data from DataRow
                }

                return result;
            }
            catch (Exception e)
            {
                throw new UserException("Could not retrieve user info", e);
            }
        }

        public async Task<IEnumerable<UserInfo>> GetInfoAsync()
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.GetUserInfoAll, null);
                DataTable dataTable = dataSet.Tables[0];

                List<UserInfo> result = new List<UserInfo>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // Get data from DataRow
                }

                return result;
            }
            catch (Exception e)
            {
                throw new UserException("Could not retrieve user info for user", e);
            }
        }

        public UserInfo GetInfo(string username)
        {
            try
            {
                DataSet dataSet = database.RunScalarProcedure(SqlProcedures.GetUserInfoAll, null);
                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                UserInfo result = new UserInfo();
                // TODO: Fill UserInfo

                return result;
            }
            catch (Exception e)
            {
                throw new UserException("Could not retrieve user info for user", e);
            }
        }

        public async Task<UserInfo> GetInfoAsync(string username)
        {
            try
            {
                DataSet dataSet = await database.RunScalarProcedureAsync(SqlProcedures.GetUserInfoAll, null);
                DataTable dataTable = dataSet.Tables[0];
                DataRow dataRow = dataTable.Rows[0];

                UserInfo result = new UserInfo();
                // TODO: Fill UserInfo

                return result;
            }
            catch (Exception e)
            {
                throw new UserException("Could not retrieve user info", e);
            }
        }
    }
}
