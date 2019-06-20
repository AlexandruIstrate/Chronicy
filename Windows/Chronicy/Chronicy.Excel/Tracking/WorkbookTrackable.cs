using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;

namespace Chronicy.Excel.Tracking
{
    public class WorkbookTrackable : ITrackable
    {
        public override Type ValueType => typeof(Workbook);

        public WorkbookTrackable(Workbook trackedWorkbook)
        {
            TrackedValue = trackedWorkbook;
            Enabled = true;

            InitializeEvents();
        }

        public WorkbookTrackable()
        {
            Enabled = true;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            // TODO: Also tell the OnChange method, and finally the tracking system, what kind of change we have (e.g. NewSheet, NewChart, WorkbookSave)
            Application app = Globals.ThisAddIn.Application;
            app.WorkbookNewSheet += (workbook, sheet) => { OnChange(workbook); };
            app.WorkbookNewChart += (workbook, chart) => { OnChange(workbook); };
        }

        private void OnChange(Workbook workbook)
        {
            Debug.WriteLine("Workbook OnChange");

            if (workbook == (TrackedValue as Workbook))
            {
                TriggerUpdate(workbook);
            }
        }
    }
}
