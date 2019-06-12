using Chronicy.Communication;
using System.Diagnostics;
using System.ServiceModel;

namespace Chronicy.Excel.Communication
{
    public class ClientBootstrapper
    {
        public IServerService Start(IClientCallback clientCallback)
        {
            InstanceContext context = new InstanceContext(clientCallback);
            DuplexChannelFactory<IServerService> channelFactory = new DuplexChannelFactory<IServerService>(context, new NetNamedPipeBinding(), new EndpointAddress(ConnectionConstants.EndpointFullAddress));

            IServerService service = channelFactory.CreateChannel();
            service.Connect();
            return service;
        }
    }
}
