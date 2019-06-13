using Chronicy.Communication;
using System;
using System.ServiceModel;

namespace Chronicy.Service.Communication
{
    public class ServerConnection
    {
        public void Start()
        {
            ServiceHost host = new ServiceHost(typeof(TrackingService), new Uri(ConnectionConstants.EndpointUri));
            host.AddServiceEndpoint(typeof(IServerService), new NetNamedPipeBinding(), ConnectionConstants.EndpointName);
            host.Open();
        }
    }
}
