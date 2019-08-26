using Chronicy.Sql;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Chronicy.Tests.Sql
{
    [TestFixture]
    public class DatabaseTest
    {
        private ISqlDatabase database;

        [Test]
        public void CanConnectToDatabase()
        {
            // TODO: SqlConnection
            Assert.DoesNotThrow(() => database = new SqlServerDatabase(new SqlConnection()), "The connection must be successful");
        }

        [Test]
        public void CanRunScalarString()
        {

        }

        [Test]
        public void CanRunNonQueryString()
        {

        }

        [Test]
        public void CanRunScalarProcedure()
        {

        }

        [Test]
        public void CanRunNonQueryProcedure()
        {

        }
    }
}
