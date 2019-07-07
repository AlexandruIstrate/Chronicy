﻿using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Managers;
using Chronicy.Data.Storage;
using Chronicy.Information;
using Chronicy.Service.Dispatch;
using Chronicy.Service.Information;
using Chronicy.Tracking;
using Chronicy.Utils;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Chronicy.Service.Communication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TrackingService : IServerService, IInformationContext
    {
        private IInformationContext context;

        private IDataSource<Notebook> dataSource;
        private NotebookManager notebookManager;

        private DispatcherTimer dispatcher;

        public IClientCallback Callback { get; set; }

        public TrackingService()
        {
            context = AgregateContext.Of(new EventLogContext(), this);

            // TODO: Use settings for this IDataSource
            dataSource = new LocalDataSource();

            ExceptionUtils.HandleExceptions(() =>
            {
                notebookManager = new NotebookManager(dataSource);
                dispatcher = new DispatcherTimer(60 * 1000);
            }, context);
        }

        public void Connect()
        {
            Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            Callback.SendAvailableNotebooks(notebookManager.GetNotebooks());

            ExceptionUtils.HandleExceptions(() =>
            {
                dispatcher.Submit(() => { Callback.SendAvailableNotebooks(notebookManager.GetNotebooks()); });
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

        public IEnumerable<Notebook> GetAll()
        {
            return dataSource.GetAll();
        }

        public Notebook Get(string uuid)
        {
            return dataSource.Get(uuid);
        }

        public void Create(Notebook notebook)
        {
            dataSource.Create(notebook);
        }

        public void Update(Notebook notebook)
        {
            dataSource.Update(notebook);
        }

        public void Delete(string uuid)
        {
            dataSource.Delete(uuid);
        }
    }
}
