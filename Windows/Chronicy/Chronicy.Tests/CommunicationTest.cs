using Chronicy.Communication;
using Chronicy.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace Chronicy.Tests
{
    [TestFixture]
    public class CommunicationTest
    {
        [Test]
        public void ClientAndServerCanCommunicate()
        {
            ThreadStart serverWork = CreateServer;
            Thread serverThread = new Thread(serverWork);

            ThreadStart clientWork = CreateClient;
            Thread clientThread = new Thread(serverWork);

            serverThread.Start();
            clientThread.Start();
        }

        private void CreateServer()
        {

        }

        private void CreateClient()
        {

        }
    }

    internal class Client : IClientCallback
    {
        public void SendAvailableNotebooks(List<Notebook> message)
        {
            throw new System.NotImplementedException();
        }

        public void SendDebugMessage(string message)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class Server : IServerService
    {
        public void Connect()
        {
            throw new System.NotImplementedException();
        }

        public void SendDebugMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendSelectedNotebook(Notebook notebook)
        {
            throw new System.NotImplementedException();
        }

        public void SendSelectedStack(Stack stack)
        {
            throw new System.NotImplementedException();
        }
    }
}
