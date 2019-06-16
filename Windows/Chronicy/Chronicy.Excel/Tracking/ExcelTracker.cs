using Interop = Microsoft.Office.Interop.Excel;
using Tools = Microsoft.Office.Tools.Excel;

namespace Chronicy.Excel.Tracking
{
    public class ExcelTracker
    {
        public WorkbookTrackable WorkbookTracker { get; private set; }
        public WorksheetTrackable WorksheetTracker { get; private set; }
        public RangeTrackable CellRangeTracker { get; private set; }

        public WorkbookTrackable Track(Interop.Workbook workbook)
        {
            if (WorkbookTracker == null)
            {
                WorkbookTracker = new WorkbookTrackable(workbook);
            }
            else
            {
                WorkbookTracker.TrackedWorkbook = workbook;
            }

            WorkbookTracker.Enabled = true;
            return WorkbookTracker;
        }

        public WorksheetTrackable Track(Tools.Worksheet worksheet)
        {
            if (WorksheetTracker == null)
            {
                WorksheetTracker = new WorksheetTrackable(worksheet);
            }
            else
            {
                WorksheetTracker.TrackedSheet = worksheet;
            }

            WorksheetTracker.Enabled = true;
            return WorksheetTracker;
        }

        public RangeTrackable Track(Interop.Range range)
        {
            if (CellRangeTracker == null)
            {
                CellRangeTracker = new RangeTrackable(range);
            }
            else
            {
                CellRangeTracker.TrackedRange = range;
            }

            CellRangeTracker.Enabled = true;
            return CellRangeTracker;
        }
    }
}
