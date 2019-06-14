using Chronicy.Communication;
using Chronicy.Excel.Communication;
using System;
using System.ServiceModel;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExtension
    {
        // TODO: Maybe use Lazy<T>?
        private ClientConnection connection;

        public IServerService Service { get; set; }

        public override void Connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new ClientConnection();
                    connection.ConnectionClosed += (() => { Connected = false; });
                    connection.ConnectionClosed += (() => { Connected = false; });
                    connection.ConnectionClosed += (() => { Connected = false; });
                }

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
            throw new NotImplementedException();
        }
    }
}
