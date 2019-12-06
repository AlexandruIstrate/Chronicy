using Chronicy.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Chronicy.Tests.Utils
{
    [TestFixture]
    public class DataUtilsTest
    {
        [Test]
        public void CanGetValue()
        {
            DataSet dataSet = new DataSet();
            Assert.AreEqual(DataUtils.ValueOrDefault(dataSet), dataSet, "The call must return the passed in value");
        }
        
        [Test]
        public void CanGetDefaultValue()
        {
            Assert.AreEqual(DataUtils.ValueOrDefault((DataSet)null), default(DataSet), "The call must return the default value");
        }

        [Test]
        public void CanCreateDataColumns()
        {
            List<DataColumn> columns = DataUtils.CreateDataColumns<string>().ToList();
            List<PropertyInfo> properties = typeof(string).GetProperties().ToList();

            bool equal = columns.Select(c => c.ColumnName).SequenceEqual(properties.Select(p => p.Name));
            Assert.True(equal, "The DataSet must contain all of the type's properties");
        }
    }
}
