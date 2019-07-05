using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Information;
using Chronicy.Service.Data;
using Chronicy.Service.Dispatch;
using Chronicy.Service.Information;
using Chronicy.Tracking;
using Chronicy.Utils;
using System;
using System.ServiceModel;
using System.Text;

namespace Chronicy.Service.Communication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TrackingService : IServerService, IInformationContext
    {
        private IInformationContext context;
        private NotebookManager notebookManager;

        private DispatcherTimer dispatcher;

        public IClientCallback Callback { get; set; }

        public TrackingService()
        {
            context = AgregateContext.Of(new EventLogContext(), this);

            ExceptionUtils.HandleExceptions(() =>
            {
                notebookManager = new NotebookManager();
                dispatcher = new DispatcherTimer();
            }, context);
        }

        public void Connect()
        {
            Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

            ExceptionUtils.HandleExceptions(() =>
            {
                dispatcher.Submit(() => { Callback.SendAvailableNotebooks(notebookManager.Notebooks); });
                dispatcher.Start();
            }, context);
        }

        public void SendSelectedNotebook(Notebook notebook)
        {
            InformationDispatcher.Default.Dispatch("Notebook: " + notebook.Name, context);

            ExceptionUtils.HandleExceptions(() => notebookManager.SelectNotebook(notebook), context);
        }

        public void SendSelectedStack(Stack stack)
        {
            InformationDispatcher.Default.Dispatch("Stack: " + stack.Name, context);

            ExceptionUtils.HandleExceptions(() => notebookManager.SelectStack(stack), context);
        }

        public void SendTrackingData(TrackingData data)
        {
            // DEBUG START
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Date: " + data.Date.ToString());
            builder.AppendLine("Name: " + data.Name);
            builder.AppendLine("Comment: " + data.Comment);

            InformationDispatcher.Default.Dispatch(builder.ToString(), context);
            // DEBUG END

            Card card = new Card(data.Name, data.Comment)
            {
                Date = data.Date,
                Fields = data.Fields,
                Tags = data.Tags
            };

            ExceptionUtils.HandleExceptions(() => notebookManager.AddCard(card), context);
        }

        public void SendDebugMessage(string message)
        {
            InformationDispatcher.Default.Dispatch(message, context);
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            switch (informationKind)
            {
                case InformationKind.Info:
                    //Callback?.SendDebugMessage(message);
                    break;

                case InformationKind.Warning:
                    break;

                case InformationKind.Error:
                    Callback?.SendErrorMessage(message);
                    break;
            }
        }

        public void ExceptionDispatched(Exception exception)
        {
            Callback?.SendErrorMessage(exception.Message);
        }
    }
}
