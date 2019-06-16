using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Tracking
{
    public class WorkbookTrackable : ITrackable<Workbook>
    {
        public Workbook TrackedWorkbook { get; set; }

        public WorkbookTrackable(Workbook trackedWorkbook)
        {
            TrackedWorkbook = trackedWorkbook;
        }

        public WorkbookTrackable()
        { 
            Globals.ThisAddIn.Application.WorkbookNewSheet += (workbook, sheet) => { OnChange(workbook); };
            Globals.ThisAddIn.Application.WorkbookNewChart += (workbook, chart) => { OnChange(workbook); };
        }

        private void OnChange(Workbook workbook)
        {
            if (workbook == TrackedWorkbook)
            {
                TriggerUpdate(workbook);
            }
        }
    }
}
