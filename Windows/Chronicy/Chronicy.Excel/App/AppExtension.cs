using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Excel.Communication;
using System.ServiceModel;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExtension
    {
        private ClientConnection connection;

        public IServerService Service { get; set; }

        public AppExtension()
        {
            connection = new ClientConnection();
            connection.ConnectionClosed += () => { Connected = false; };
        }

        public override void Connect()
        {
            try
            {
                Service = connection.Connect(new TrackedClient());
                Connected = true;
            }
            catch (EndpointNotFoundException e)
            {
                throw new EndpointConnectionException("Could not connect to the endpoint: " + e.Message);
            }
        }

        public override void Sync()
        {
            Service.SendSelectedNotebook(new Notebook("A Test Notebook"));
        }
    }
}
