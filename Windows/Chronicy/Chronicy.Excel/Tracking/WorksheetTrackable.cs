using Chronicy.Tracking;
using System.Diagnostics;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace Chronicy.Excel.Tracking
{
    public class WorksheetTrackable : ITrackable<ExcelWorksheet>
    {
        public ExcelWorksheet TrackedSheet { get; set; }

        public WorksheetTrackable(ExcelWorksheet trackedSheet)
        {
            TrackedSheet = trackedSheet;
            Enabled = true;

            InitializeEvents();
        }

        public WorksheetTrackable()
        {
            Enabled = false;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Globals.ThisAddIn.Application.SheetChange += (sheet, range) =>
            {
                ExcelWorksheet worksheet = (ExcelWorksheet)sheet;

                if (worksheet == TrackedSheet)
                {
                    TriggerUpdate(worksheet);
                }
            };
        }
    }
}
