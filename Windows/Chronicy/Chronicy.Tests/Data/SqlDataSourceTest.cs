using Chronicy.Data;
using Chronicy.Data.Storage;
using NUnit.Framework;

namespace Chronicy.Tests.Data
{
    [TestFixture]
    public class SqlDataSourceTest
    {
        private IDataSource<Notebook> dataSource;

        [OneTimeSetUp]
        public void Setup()
        {
            dataSource = DataSourceFactory.Create(DataSourceType.Local);
        }

        [Test]
        public void CanReadAll()
        {
            Assert.DoesNotThrow(() =>
            {
                dataSource.GetAll();
            },
            "The read operation must be successful");
        }
    }
}
