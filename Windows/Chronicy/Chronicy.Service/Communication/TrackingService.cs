using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Managers;
using Chronicy.Data.Storage;
using Chronicy.Information;
using Chronicy.Service.Information;
using Chronicy.Tracking;
using Chronicy.Utils;
using Chronicy.Web;
using Chronicy.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Chronicy.Service.Communication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, IncludeExceptionDetailInFaults = true)]
    public class TrackingService : IServerService, IInformationContext
    {
        private readonly IInformationContext context;

        private NotebookManager notebookManager;

        public IClientCallback Callback { get; set; }

        public TrackingService()
        {
            context = AgregateContext.Of(EventLogContext.Current, this);

            ExceptionUtils.LogExceptions(() => notebookManager = new NotebookManager(DataSourceFactory.Create(DataSourceType.Local)), context);
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

        public DataResult Authenticate(string username, string password)
        {
            try
            {
                Web.Models.Token response = ChronicyWebApi.Shared.Authenticate(username, password);

                if (response.HasError)
                {
                    return DataResult.Failure(response.ErrorMessage);
                }
            }
            catch (WebApiException e)
            {
                InformationDispatcher.Default.Dispatch(e, DebugLogContext.Current);
                return DataResult.Failure("Could not authenticate");
            }

            return DataResult.Success();
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

        public DataResult<IEnumerable<Notebook>> GetAll()
        {
            try
            {
                return new DataResult<IEnumerable<Notebook>>(notebookManager.dataSource.GetAll());
            }
            catch (DataSourceException e)
            {
                return new DataResult<IEnumerable<Notebook>>(e.Message);
            }
        }

        public DataResult<Notebook> Get(int id)
        {
            try
            {
                return new DataResult<Notebook>(notebookManager.dataSource.Get(id));
            }
            catch (DataSourceException e)
            {
                return new DataResult<Notebook>(e.Message);
            }
        }

        public DataResult Create(Notebook notebook)
        {
            try
            {
                notebookManager.dataSource.Create(notebook);
            }
            catch (DataSourceException e)
            {
                DataResult.Failure(e.Message);
            }

            return DataResult.Success();
        }

        public DataResult Update(Notebook notebook)
        {
            try
            {
                notebookManager.dataSource.Update(notebook);
            }
            catch (DataSourceException e)
            {
                return DataResult.Failure(e.Message);
            }

            return DataResult.Success();
        }

        public DataResult Delete(int id)
        {
            try
            {
                notebookManager.dataSource.Delete(id);
            }
            catch (DataSourceException e)
            {
                return DataResult.Failure(e.Message);
            }

            return DataResult.Success();
        }
    }
}
