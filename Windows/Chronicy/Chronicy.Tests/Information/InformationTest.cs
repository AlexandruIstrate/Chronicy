using System;
using Chronicy.Information;
using NUnit.Framework;

namespace Chronicy.Tests.Information
{
    [TestFixture]
    public class InformationTest
    {
        [Test]
        public void CanDispatchMessages()
        {
            TestContext context = new TestContext();

            InformationDispatcher.Default.Dispatch("Info", context, InformationKind.Info);
            Assert.AreEqual("Info", context.LastMessage);
            Assert.AreEqual(InformationKind.Info, context.LastInformationKind);

            InformationDispatcher.Default.Dispatch("Warning", context, InformationKind.Warning);
            Assert.AreEqual("Warning", context.LastMessage);
            Assert.AreEqual(InformationKind.Warning, context.LastInformationKind);

            InformationDispatcher.Default.Dispatch("Error", context, InformationKind.Error);
            Assert.AreEqual("Error", context.LastMessage);
            Assert.AreEqual(InformationKind.Error, context.LastInformationKind);
        }
    }

    internal class TestContext : IInformationContext
    {
        public string LastMessage { get; set; }
        public InformationKind LastInformationKind { get; set; }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            LastMessage = message;
            LastInformationKind = informationKind;
        }

        public void ExceptionDispatched(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
