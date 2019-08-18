using System;
using System.ServiceModel;

namespace Chronicy.Communication
{
    public class ClientConnection
    {
        public event EventHandler ConnectionClosed;
        public event EventHandler ConnectionFaulted;

        // TODO: Detect when WCF sends a closed signal
        public IServerService Connect(IClientCallback clientCallback)
        {
            InstanceContext context = new InstanceContext(clientCallback);

            DuplexChannelFactory<IServerService> channelFactory = new DuplexChannelFactory<IServerService>(context, new NetNamedPipeBinding(), new EndpointAddress(ConnectionConstants.EndpointFullAddress));
            channelFactory.Closed += (sender, args) => ConnectionClosed?.Invoke(this, EventArgs.Empty);
            channelFactory.Faulted += (sender, args) => ConnectionFaulted?.Invoke(this, EventArgs.Empty);

            IServerService service = channelFactory.CreateChannel();
            service.Connect();
            return service;
        }
    }
}
