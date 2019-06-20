using Chronicy.Tracking;
using System;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace Chronicy.Excel.Tracking
{
    public class WorksheetTrackable : ITrackable
    {
        public override Type ValueType => typeof(ExcelWorksheet);

        public WorksheetTrackable(ExcelWorksheet trackedSheet)
        {
            TrackedValue = trackedSheet;
            Enabled = true;

            InitializeEvents();
        }

        public WorksheetTrackable()
        {
            Enabled = true;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Globals.ThisAddIn.Application.SheetChange += (sheet, range) =>
            {
                ExcelWorksheet worksheet = (ExcelWorksheet)sheet;

                if (worksheet == (TrackedValue as ExcelWorksheet))
                {
                    TriggerUpdate(worksheet);
                }
            };
        }
    }
}
