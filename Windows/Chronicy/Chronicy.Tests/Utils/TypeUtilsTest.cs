using Chronicy.Utils;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Chronicy.Tests.Utils
{
    [TestFixture]
    public class TypeUtilsTest
    {
        [Test]
        public void CanCheckIsEnumerable()
        {
            IEnumerable e = new List<int>();
            Assert.True(TypeUtils.IsEnumerable(e.GetType()), "The passed in type must return true");
        }

        public void CanCheckIsCollection()
        {
            ICollection c = new List<int>();
            Assert.True(TypeUtils.IsEnumerable(c.GetType()), "The passed in type must return true");
        }
    }
}
