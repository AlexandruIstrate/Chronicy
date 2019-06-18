using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Excel.Communication;
using System.ServiceModel;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExcelExtension
    {
        private ClientConnection connection;

        public IServerService Service { get; private set; }

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
                throw new EndpointConnectionException("Could not connect to the endpoint", e);
            }
        }

        public override void Sync()
        {
            if (!Connected)
            {
                throw new EndpointConnectionException("The endpoint is not connected");
            }

            Service.SendSelectedNotebook(new Notebook("A Test Notebook"));
        }
    }
}
