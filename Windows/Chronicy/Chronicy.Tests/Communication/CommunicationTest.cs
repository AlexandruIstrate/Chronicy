using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Tracking;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Chronicy.Tests.Communication
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
            throw new NotImplementedException();
        }

        public void SendDebugMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SendErrorMessage(string message)
        {
            throw new NotImplementedException();
        }
    }

    internal class Server : IServerService
    {
        public DataResult Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public DataResult Create(Notebook notebook)
        {
            throw new NotImplementedException();
        }

        public DataResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResult<Notebook> Get(int id)
        {
            throw new NotImplementedException();
        }

        public DataResult<IEnumerable<Notebook>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SendDebugMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SendSelectedDataSource(DataSourceType dataSource)
        {
            throw new NotImplementedException();
        }

        public void SendSelectedNotebook(Notebook notebook)
        {
            throw new NotImplementedException();
        }

        public void SendSelectedStack(Stack stack)
        {
            throw new NotImplementedException();
        }

        public void SendTrackingData(TrackingData data)
        {
            throw new NotImplementedException();
        }

        public void SendUrl(string url)
        {
            throw new NotImplementedException();
        }

        public DataResult Update(Notebook notebook)
        {
            throw new NotImplementedException();
        }
    }
}
