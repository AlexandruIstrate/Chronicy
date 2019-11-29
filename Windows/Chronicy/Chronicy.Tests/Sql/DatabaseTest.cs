using Chronicy.Sql;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Chronicy.Tests.Sql
{
    [Ignore("Database testing infrastructure is not yet built")]
    [TestFixture]
    public class DatabaseTest
    {
        private ISqlDatabase database;

        [OneTimeSetUp]
        public void Setup()
        {
            // TODO: SqlConnection
            Assert.DoesNotThrow(() => database = new SqlServerDatabase(new SqlConnection()), "The connection must be successful");
        }

        [Test]
        public void CanRunScalarString()
        {
            // TODO: Query string
            Assert.DoesNotThrow(() => database.RunScalarString(string.Empty), "The scalar query must run successfuly");
        }

        [Test]
        public void CanRunNonQueryString()
        {
            // TODO: Query string
            Assert.DoesNotThrow(() => database.RunNonQueryString(string.Empty), "The non-query must run successfuly");
        }

        [Test]
        public void CanRunScalarProcedure()
        {
            // TODO: Procedure name
            Assert.DoesNotThrow(() => database.RunScalarProcedure(string.Empty), "The scalar procedure must run successfuly");
        }

        [Test]
        public void CanRunNonQueryProcedure()
        {
            // TODO: Procedure name
            Assert.DoesNotThrow(() => database.RunNonQueryProcedure(string.Empty), "The non-query procedure must run successfuly");
        }
    }
}
