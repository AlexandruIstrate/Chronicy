using Chronicy.Data;
using Chronicy.Web.Converters;
using NUnit.Framework;

namespace Chronicy.Tests
{
    [TestFixture]
    public class ConversionTest
    {
        [Test]
        public void NotebookCanBeConverted()
        {
            IConverter<Notebook, Web.Models.Notebook> converter = new NotebookConverter();
            Notebook initial = new Notebook("A Notebook");

            Web.Models.Notebook webNotebook = converter.Convert(initial);
            Notebook convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void StackCanBeConverted()
        {
            IConverter<Stack, Web.Models.Stack> converter = new StackConverter();
            Stack initial = new Stack("A Stack");

            Web.Models.Stack webNotebook = converter.Convert(initial);
            Stack convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void CardCanBeConverted()
        {
            IConverter<Card, Web.Models.Card> converter = new CardConverter();
            Card initial = new Card("A Card");

            Web.Models.Card webNotebook = converter.Convert(initial);
            Card convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void CustomFieldCanBeConverted()
        {
            IConverter<CustomField, Web.Models.CustomField> converter = new CustomFieldConverter();
            CustomField initial = new CustomField("A Field", FieldType.String)
            {
                Value = "Hello, world!"
            };

            Web.Models.CustomField webNotebook = converter.Convert(initial);
            CustomField convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        //[Test]
        //public void CustomFieldCanBeConvertedWithFloatingPoint()
        //{
        //    IConverter<CustomField, Web.Models.CustomField> converter = new CustomFieldConverter();
        //    CustomField initial = new CustomField("A Field", FieldType.String)
        //    {
        //        Value = 3.14
        //    };

        //    Web.Models.CustomField webNotebook = converter.Convert(initial);
        //    CustomField convertedBack = converter.ReverseConvert(webNotebook);

        //    Assert.AreEqual(initial, convertedBack);
        //}
    }
}
