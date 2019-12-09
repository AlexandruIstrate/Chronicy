using Chronicy.Authentication;
using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Managers;
using Chronicy.Data.Storage;
using Chronicy.Excel.Communication;
using Chronicy.Excel.Data;
using Chronicy.Excel.History;
using Chronicy.Excel.Information;
using Chronicy.Excel.Properties;
using Chronicy.Excel.Tracking;
using Chronicy.Excel.Utils;
using Chronicy.Information;
using Chronicy.Tracking;
using Chronicy.Utils;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExcelExtension
    {
        private readonly ClientConnection connection;

        public IServerService Service { get; private set; }

        private NotebookHistoryProvider historyProvider;

        private readonly TrackingSystem tracking;
        private readonly NotebookManager notebooks;
        private readonly HistoryManager history;
        private readonly AuthenticationManager credentialsManager;

        public override TrackingSystem Tracking => tracking;
        public override NotebookManager Notebooks => notebooks;
        public override HistoryManager History => history;
        public override IAuthenticationManager CredentialsManager => credentialsManager;

        public AppExtension()
        {
            connection = new ClientConnection();
            connection.ConnectionClosed += (sender, args) => { Connected = false; };

            historyProvider = new NotebookHistoryProvider(null);

            tracking = new TrackingSystem();
            notebooks = new NotebookManager(null);
            history = new HistoryManager();
            credentialsManager = new AuthenticationManager(null);

            InitializeNotebooks();
            InitializeTracking();
            InitializeHistory();
        }

        public override void Connect()
        {
            try
            {
                TrackedClient client = new TrackedClient();
                client.NotebooksRecieved += (notebooks) => OnNotebooksUpdated(notebooks);

                Service = connection.Connect(client);
                Connected = true;

                notebooks.DataSource = new ServiceDataSource(Service);
                credentialsManager.Service = Service;

                Service.SendUrl(Settings.Default.WebServiceAddress);
                AutoConnect();
            }
            catch (EndpointNotFoundException e)
            {
                throw new EndpointConnectionException("The remote endpoint could not be found or reached", e);
            }
            catch (Exception e)
            {
                throw new EndpointConnectionException("An unknown error occurred while connecting", e);
            }
        }

        public override void Sync()
        {
            if (!Connected)
            {
                throw new EndpointConnectionException("The endpoint is not connected");
            }

            if (Notebooks.SelectedNotebook == null ||
                Notebooks.SelectedStack == null)
            {
                Notebooks.ClearSelection();
                return;
            }

            SelectNotebook(Notebooks.SelectedNotebook);
            SelectStack(Notebooks.SelectedStack);
        }

        public override void OnStart()
        {
            Settings.Default.PropertyChanged += (sender, args) =>
            {
                Service.SendUrl(Settings.Default.WebServiceAddress);
            };
        }

        public override void SelectDataSource(DataSourceType dataSource)
        {
            Service.SendSelectedDataSource(dataSource);
        }

        public override void SelectNotebook(Notebook notebook)
        {
            Service.SendSelectedNotebook(notebook);
        }

        public override void SelectStack(Stack stack)
        {
            Service.SendSelectedStack(stack);
        }

        private void InitializeNotebooks()
        {
            notebooks.NotebooksChanged += (sender, args) => OnNotebooksUpdated();
        }

        private void AutoConnect()
        {
            if (!Settings.Default.AutoConnect)
            {
                return;
            }

            Task.Run(() => Authenticate())
                .ContinueWith(task => PresentAuthenticationState(CredentialsManager.State));
        }

        private void Authenticate()
        {
            try
            {
                string username = ProtectedDataStorage.Unprotect(Settings.Default.EncryptedUsername);
                string password = ProtectedDataStorage.Unprotect(Settings.Default.EncryptedPassword);

                CredentialsManager.Signin(username, password);
            }
            catch (AuthenticationException e)
            {
                InformationDispatcher.Default.Dispatch(e, DebugLogContext.Current);
            }
        }

        private void PresentAuthenticationState(AuthenticationState state)
        {
            if (state != AuthenticationState.Errored)
            {
                return;
            }

            InformationDispatcher.Default.Dispatch("Could not authenticate to web service", new MessageBoxContext(), InformationKind.Error);
        }

        private void InitializeTracking()
        {
            tracking.Register<Workbook>((trackingEvent) =>
            {
                Workbook workbook = (Workbook)trackingEvent.Value;

                TrackingDataBuilder builder = new TrackingDataBuilder
                {
                    Name = "Workbook Updated",
                    Comment = $"The data in the workbook { workbook.Name } changed"
                };

                List<CustomField> fields = new List<CustomField>
                {
                    new CustomField("WorkbookName", FieldType.String, workbook.Name),
                    new CustomField("ModifiedItems", FieldType.String, "")  // TODO: Fill in
                };

                builder.Fields = fields;

                List<Tag> tags = new List<Tag>
                {
                    new Tag("Office"),
                    new Tag("Excel"),
                    new Tag("Workbook")
                };

                builder.Tags = tags;

                Service.SendTrackingData(builder.Create());
            });

            tracking.Register<Worksheet>((trackingEvent) =>
            {
                Worksheet worksheet = (Worksheet)trackingEvent.Value;

                TrackingDataBuilder builder = new TrackingDataBuilder
                {
                    Name = "Worksheet Updated",
                    Comment = $"The data in the worksheet { worksheet.Name } changed"
                };

                List<CustomField> fields = new List<CustomField>
                {
                    new CustomField("SheetName", FieldType.String, worksheet.Name),
                    new CustomField("ModifiedItems", FieldType.String, "")  // TODO: Fill in
                };

                builder.Fields = fields;

                List<Tag> tags = new List<Tag>
                {
                    new Tag("Office"),
                    new Tag("Excel"),
                    new Tag("Sheet")
                };

                builder.Tags = tags;

                Service.SendTrackingData(builder.Create());
            });

            tracking.Register<Range>((trackingEvent) =>
            {
                Range range = (Range)trackingEvent.Value;
                string rangeString = range.ToDisplayAddressString();

                TrackingDataBuilder builder = new TrackingDataBuilder
                {
                    Name = "Range Updated",
                    Comment = $"The data in the range { rangeString } changed"
                };

                List<CustomField> fields = new List<CustomField>
                {
                    new CustomField("RangeName", FieldType.String, rangeString),
                    new CustomField("ModifiedItems", FieldType.String, "")  // TODO: Fill in
                };

                builder.Fields = fields;

                List<Tag> tags = new List<Tag>
                {
                    new Tag("Office"),
                    new Tag("Excel"),
                    new Tag("Range")
                };

                builder.Tags = tags;
                                                           
                Service.SendTrackingData(builder.Create());
            });
        }

        private void InitializeHistory()
        {
            Notebooks.DataSourceChanged += (sender, args) => historyProvider.DataSource = Notebooks.DataSource;
            History.Providers.Add(historyProvider);
        }
    }
}
