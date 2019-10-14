using Chronicy.Data;
using NUnit.Framework;

namespace Chronicy.Tests.Data
{
    [TestFixture]
    public class FieldTemplateTest
    {
        [Test]
        public void TemplatesMatch()
        {
            FieldTemplate template1 = new FieldTemplate(new CustomField());
            FieldTemplate template2 = new FieldTemplate(new CustomField());

            Assert.True(template1.Matches(template2), "The two templates must match");
        }
    }
}
