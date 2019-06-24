using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Excel.Communication;
using Chronicy.Excel.Utils;
using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
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
            Tracking.Register<Workbook>((trackingEvent) =>
            {
                Workbook workbook = (Workbook)trackingEvent.Value;

                TrackingDataBuilder builder = new TrackingDataBuilder();
                builder.Name = "Workbook Updated";
                builder.Comment = $"The data in the workbook { workbook.Name } changed";

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

            Tracking.Register<Worksheet>((trackingEvent) =>
            {
                Worksheet worksheet = (Worksheet)trackingEvent.Value;

                TrackingDataBuilder builder = new TrackingDataBuilder();
                builder.Name = "Worksheet Updated";
                builder.Comment = $"The data in the worksheet { worksheet.Name } changed";

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

            Tracking.Register<Range>((trackingEvent) =>
            {
                Range range = (Range)trackingEvent.Value;
                string rangeString = range.ToAddressString().Replace("$", "");  // TODO: Make it so that we don't need to do the replace thing

                TrackingDataBuilder builder = new TrackingDataBuilder();
                builder.Name = "Range Updated";
                builder.Comment = $"The data in the range { rangeString } changed";

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
    }
}
