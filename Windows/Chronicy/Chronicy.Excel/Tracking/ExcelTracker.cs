using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Tracking
{
    // TODO: Change to something more dynamic
    public class ExcelTracker
    {
        public WorkbookTrackable WorkbookTracker { get; }
        public WorksheetTrackable WorksheetTracker { get; }
        public RangeTrackable CellRangeTracker { get; }

        public ExcelTracker()
        {
            WorkbookTracker = new WorkbookTrackable();
            WorksheetTracker = new WorksheetTrackable();
            CellRangeTracker = new RangeTrackable();
        }

        public WorkbookTrackable Track(Workbook workbook)
        {
            WorkbookTracker.TrackedWorkbook = workbook;
            WorkbookTracker.Enabled = true;
            return WorkbookTracker;
        }

        public WorksheetTrackable Track(Worksheet worksheet)
        {
            WorksheetTracker.TrackedSheet = worksheet;
            WorksheetTracker.Enabled = true;
            return WorksheetTracker;
        }

        public RangeTrackable Track(Range range)
        {
            CellRangeTracker.TrackedRange = range;
            CellRangeTracker.Enabled = true;
            return CellRangeTracker;
        }
    }
}
