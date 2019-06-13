using Chronicy.Communication;
using System.ServiceModel;

namespace Chronicy.Excel.Communication
{
    public class ClientConnection
    {
        public delegate void ConnectionClosedEventHandler();
        public event ConnectionClosedEventHandler ConnectionClosed;

        public IServerService Connect(IClientCallback clientCallback)
        {
            InstanceContext context = new InstanceContext(clientCallback);
            context.Closing += ((sender, args) => 
            {
                ConnectionClosed?.Invoke();
            });

            DuplexChannelFactory<IServerService> channelFactory = new DuplexChannelFactory<IServerService>(context, new NetNamedPipeBinding(), new EndpointAddress(ConnectionConstants.EndpointFullAddress));

            IServerService service = channelFactory.CreateChannel();
            service.Connect();
            return service;
        }
    }
}
