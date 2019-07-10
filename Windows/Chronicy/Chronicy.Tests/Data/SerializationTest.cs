using Chronicy.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Chronicy.Tests.Data
{
    [TestFixture]
    public class SerializationTest
    {
        [Test]
        public void CanSerializeNotebook()
        {
            Notebook notebook = new Notebook
            {
                Name = "Test",
                Stacks = new List<Stack> { new Stack { Name = "Stack1" } }
            };
            Notebook serialized = null;

            Assert.DoesNotThrow(() => serialized = Serialize(notebook), "Notebook should be serializable");
            Assert.AreEqual(notebook, serialized, "The resulting notebook should be the same as the input");
        }

        [Test]
        public void CanSerializeStack()
        {
            Stack stack = new Stack
            {
                Name = "Test",
                Fields = new List<CustomField> {
                    new CustomField { Name = "Field1", Type = FieldType.String, Value = "Value1" },
                    new CustomField { Name = "Field2", Type = FieldType.Number, Value = 100 }
                }
            };
            Stack serialized = null;

            Assert.DoesNotThrow(() => serialized = Serialize(stack), "Stack should be serializable");
            Assert.AreEqual(stack, serialized, "The resulting stack should be the same as the input");
        }

        [Test]
        public void CanSerializeCard()
        {
            Card card = new Card
            {
                Name = "Test",
                Comment = "This is a test card",
                Fields = new List<CustomField> {
                    new CustomField { Name = "Field1", Type = FieldType.String, Value = "Value1" }
                    //new CustomField { Name = "Field2", Type = FieldType.Number, Value = 100 }
                }
            };
            Card serialized = null;

            Assert.DoesNotThrow(() => serialized = Serialize(card), "Card should be serializable");
            Assert.AreEqual(card, serialized, "The resulting card should be the same as the input");
        }

        [Test]
        public void CanSerializeCustomField()
        {
            CustomField field = new CustomField { Name = "Field1", Type = FieldType.String, Value = "Value1" };
            CustomField serialized = null;

            Assert.DoesNotThrow(() => serialized = Serialize(field), "Field should be serializable");
            Assert.AreEqual(field, serialized, "The resulting field should be the same as the input");
        }

        [Test]
        public void CanSerializeTag()
        {
            Tag tag = new Tag { Name = "Test" };
            Tag serialized = null;

            Assert.DoesNotThrow(() => serialized = Serialize(tag), "Tag should be serializable");
            Assert.AreEqual(tag, serialized, "The resulting tag should be the same as the input");
        }

        private T Serialize<T>(T obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            byte[] data;

            using (MemoryStream writeStream = new MemoryStream())
            {
                serializer.WriteObject(writeStream, obj);
                data = writeStream.ToArray();
            }

            T result;

            using (MemoryStream readStream = new MemoryStream(data))
            {
                result = (T)serializer.ReadObject(readStream);
            }

            return result;
        }
    }
}
