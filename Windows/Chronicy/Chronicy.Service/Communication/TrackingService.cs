using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Information;
using Chronicy.Service.Information;
using Chronicy.Tracking;
using System;
using System.ServiceModel;
using System.Text;

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

        public void SendTrackingData(TrackingData data)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Date: " + data.Date.ToString());
            builder.AppendLine("Name: " + data.Name);
            builder.AppendLine("Comment: " + data.Comment);

            InformationDispatcher.Default.Dispatch(builder.ToString(), context);
        }

        public void SendDebugMessage(string message)
        {
            InformationDispatcher.Default.Dispatch(message, context);
        }

    }
}
