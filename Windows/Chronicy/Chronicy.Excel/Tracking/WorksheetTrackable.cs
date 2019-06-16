using Chronicy.Tracking;
using Microsoft.Office.Tools.Excel;

namespace Chronicy.Excel.Tracking
{
    public class WorksheetTrackable : ITrackable<Worksheet>
    {
        public Worksheet TrackedSheet { get; set; }

        public WorksheetTrackable(Worksheet trackedSheet)
        {
            TrackedSheet = trackedSheet;

            Globals.ThisAddIn.Application.SheetChange += (sheet, range) =>
            {
                Worksheet worksheet = (Worksheet)sheet;

                if (worksheet == TrackedSheet)
                {
                    TriggerUpdate(worksheet);
                }
            };
        }
    }
}
