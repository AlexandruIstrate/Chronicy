using Chronicy.Standard.Config;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace Chronicy.Sql
{
    public class SqlConnectionFactory
    {
        public static SqlConnection Create(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = configuration[Settings.Database.DataSource],
                InitialCatalog = configuration[Settings.Database.InitialCatalog],
                UserID = configuration[Settings.Database.UserID],
                Password = configuration[Settings.Database.Password],
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            };

            return new SqlConnection(builder.ToString());
        }
    }
}
