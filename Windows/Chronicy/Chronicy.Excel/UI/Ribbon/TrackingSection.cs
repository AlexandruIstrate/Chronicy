using Chronicy.Excel.Tracking;
using Chronicy.Excel.User;
using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using System;

namespace Chronicy.Excel.UI.Ribbon
{
    public class TrackingSection : Section
    {
        public ExcelTracker ExcelTracker { get; set; }

        public TrackingSystem ApplicationTracking { get; set; }

        public TrackingSection(ExcelTracker excelTracker, TrackingSystem applicationTracking)
        {
            ExcelTracker = excelTracker;
            ApplicationTracking = applicationTracking;

            InitializeCalls();
        }

        public void TrackWorkbook()
        {
            ITrackable trackable = ExcelTracker.Get<Workbook>();
            trackable.ValueUpdated += (value) => ApplicationTracking.Post<Workbook>(TrackingEvent.Create((Workbook)value));
            trackable.TrackedValue = Globals.ThisAddIn.Application.ActiveWorkbook;
            trackable.Enabled = true;
        }

        public void TrackSheet()
        {
            // 1. Ask the user to pick a sheet
            SelectSheetAction action = new SelectSheetAction();
            action.ActionCompleted += (sheet) =>
            {
                /* 2. Submit the sheet to the TrackingSystem */
                ITrackable trackable = ExcelTracker.Get<Worksheet>();
                trackable.ValueUpdated += (value) => ApplicationTracking.Post<Worksheet>(TrackingEvent.Create((Worksheet)value));
                trackable.TrackedValue = sheet;
                trackable.Enabled = true;
            };
            action.Run();
        }

        public void TrackCellRange()
        {
            // 1. Ask the user to pick a range
            SelectCellRangeAction action = new SelectCellRangeAction();
            action.ActionCompleted += (range) =>
            {
                /* 2. Submit the range to the TrackingSystem */
                ITrackable trackable = ExcelTracker.Get<Range>();
                trackable.ValueUpdated += (value) => ApplicationTracking.Post<Range>(TrackingEvent.Create((Range)value));
                trackable.TrackedValue = range;
                trackable.Enabled = true;
            };
            action.Run();
        }

        private void InitializeCalls()
        {
            Actions[ActionIdentifiers.TrackWorkbook] = () => TrackWorkbook();
            Actions[ActionIdentifiers.TrackSheet] = () => TrackSheet();
            Actions[ActionIdentifiers.TrackCellRange] = () => TrackCellRange();
        }

        public static class ActionIdentifiers
        {
            public const string TrackWorkbook = "TrackWorkbook";
            public const string TrackSheet = "TrackSheet";
            public const string TrackCellRange = "TrackCellRange";
        }
    }
}
