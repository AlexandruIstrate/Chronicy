using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Excel.Communication;
using Chronicy.Excel.Information;
using Chronicy.Excel.Tracking.Events;
using Chronicy.Information;
using Microsoft.Office.Interop.Excel;
using System.ServiceModel;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExcelExtension
    {
        private ClientConnection connection;

        public IServerService Service { get; private set; }

        public AppExtension()
        {
            connection = new ClientConnection();
            connection.ConnectionClosed += () => { Connected = false; };

            InitializeTracking();
        }

        public override void Connect()
        {
            try
            {
                Service = connection.Connect(new TrackedClient());
                Connected = true;
            }
            catch (EndpointNotFoundException e)
            {
                throw new EndpointConnectionException("Could not connect to the endpoint", e);
            }
        }

        public override void Sync()
        {
            if (!Connected)
            {
                throw new EndpointConnectionException("The endpoint is not connected");
            }

            Service.SendSelectedNotebook(new Notebook("A Test Notebook"));
        }

        private void InitializeTracking()
        {
            Tracking.Register<Workbook>(TestCallback);
            Tracking.Register<Worksheet>(TestCallback);
            Tracking.Register<Range>(TestCallback);
        }

        private void TestCallback(TrackingEvent trackingEvent)
        {
            InformationDispatcher.Default.Dispatch(trackingEvent.ValueType.FullName, new MessageBoxContext());
        }
    }
}
