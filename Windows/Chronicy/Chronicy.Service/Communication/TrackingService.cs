using Chronicy.Communication;
using System.ServiceModel;

namespace Chronicy.Service.Communication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TrackingService : IServerService
    {
        public IClientCallback Callback { get; set; }

        public void Connect()
        {
            Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
        }
    }
}
