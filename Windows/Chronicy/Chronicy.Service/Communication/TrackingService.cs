using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Managers;
using Chronicy.Data.Storage;
using Chronicy.Information;
using Chronicy.Service.Dispatch;
using Chronicy.Service.Information;
using Chronicy.Tracking;
using Chronicy.Utils;
using Chronicy.Web;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Chronicy.Service.Communication
{
    //[ErrorHandlingServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, IncludeExceptionDetailInFaults = true)]
    public class TrackingService : IServerService, IInformationContext
    {
        private IInformationContext context;
        private NotebookManager notebookManager;

        public IClientCallback Callback { get; set; }

        public TrackingService()
        {
            context = AgregateContext.Of(EventLogContext.Current, this);

            ExceptionUtils.LogExceptions(() =>
            {
                notebookManager = new NotebookManager(DataSourceFactory.Create(DataSourceType.Local));
            }, context);
        }

        public void Connect()
        {
            ExceptionUtils.LogExceptions(() =>
            {
                Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                Callback.SendAvailableNotebooks(notebookManager.GetNotebooks());
            }, context);
        }

        public void SendUrl(string url)
        {
            ExceptionUtils.LogExceptions(() => ChronicyWebApi.Shared.Url = url, context);
        }

        public void Authenticate(string username, string password)
        {
            ExceptionUtils.LogExceptions(() => ChronicyWebApi.Shared.Authenticate(username, password), context);
        }

        public void SendSelectedDataSource(DataSourceType dataSource)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.DataSource = DataSourceFactory.Create(dataSource), context);
        }

        public void SendSelectedNotebook(Notebook notebook)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.SelectNotebook(notebook), context);
        }

        public void SendSelectedStack(Stack stack)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.SelectStack(stack), context);
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

            ExceptionUtils.LogExceptions(() => notebookManager.AddCard(card), context);
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

        public IEnumerable<Notebook> GetAll()
        {
            return notebookManager.dataSource.GetAll();
        }

        public Notebook Get(int id)
        {
            return notebookManager.dataSource.Get(id);
        }

        public void Create(Notebook notebook)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.dataSource.Create(notebook), context);
        }

        public void Update(Notebook notebook)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.dataSource.Update(notebook), context);
        }

        public void Delete(int id)
        {
            ExceptionUtils.LogExceptions(() => notebookManager.dataSource.Delete(id), context);
        }
    }
}
