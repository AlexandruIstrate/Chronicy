using Chronicy.Data;
using Chronicy.Web.Converters;
using NUnit.Framework;

namespace Chronicy.Tests.Data
{
    [TestFixture]
    public class ConversionTest
    {
        [Test]
        public void NotebookCanBeConverted()
        {
            IConverter<Notebook, Chronicy.Web.Models.Notebook> converter = new NotebookConverter();
            Notebook initial = new Notebook("A Notebook");

            Chronicy.Web.Models.Notebook webNotebook = converter.Convert(initial);
            Notebook convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void StackCanBeConverted()
        {
            IConverter<Stack, Chronicy.Web.Models.Stack> converter = new StackConverter();
            Stack initial = new Stack("A Stack");

            Chronicy.Web.Models.Stack webNotebook = converter.Convert(initial);
            Stack convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void CardCanBeConverted()
        {
            IConverter<Card, Chronicy.Web.Models.Card> converter = new CardConverter();
            Card initial = new Card("A Card");

            Chronicy.Web.Models.Card webNotebook = converter.Convert(initial);
            Card convertedBack = converter.ReverseConvert(webNotebook);

            Assert.AreEqual(initial, convertedBack);
        }

        [Test]
        public void CustomFieldCanBeConverted()
        {
            IConverter<CustomField, Chronicy.Web.Models.CustomField> converter = new CustomFieldConverter();
            CustomField initial = new CustomField("A Field", FieldType.String)
            {
                Value = "Hello, world!"
            };

            Chronicy.Web.Models.CustomField webNotebook = converter.Convert(initial);
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
