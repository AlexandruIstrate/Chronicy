using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Information;
using Chronicy.Service.Information;
using System.ServiceModel;

namespace Chronicy.Service.Communication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TrackingService : IServerService
    {
        // Test only
        private EventLogContext context = new EventLogContext();

        public IClientCallback Callback { get; set; }

        public void Connect()
        {
            Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
        }

        public void SendSelectedNotebook(Notebook notebook)
        {
            InformationDispatcher.Default.Dispatch("Notebook: " + notebook.Name, context);
        }

        public void SendSelectedStack(Stack stack)
        {
            InformationDispatcher.Default.Dispatch("Stack: " + stack.Name, context);
        }

        public void SendDebugMessage(string message)
        {
            InformationDispatcher.Default.Dispatch(message, context);
        }
    }
}
