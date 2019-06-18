using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Chronicy.Excel.Tracking
{
    public class WorkbookTrackable : ITrackable<Workbook>
    {
        public Workbook TrackedWorkbook { get; set; }

        public WorkbookTrackable(Workbook trackedWorkbook)
        {
            TrackedWorkbook = trackedWorkbook;
            Enabled = true;

            InitializeEvents();
        }

        public WorkbookTrackable()
        { 
            Enabled = false;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Globals.ThisAddIn.Application.WorkbookNewSheet += (workbook, sheet) => { OnChange(workbook); };
            Globals.ThisAddIn.Application.WorkbookNewChart += (workbook, chart) => { OnChange(workbook); };
        }

        private void OnChange(Workbook workbook)
        {
            Debug.WriteLine("Workbook OnChange");

            if (workbook == TrackedWorkbook)
            {
                TriggerUpdate(workbook);
            }
        }
    }
}
