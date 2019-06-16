using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Tracking
{
    public class RangeTrackable : ITrackable<Range>
    {
        public Range TrackedRange { get; set; }

        public RangeTrackable(Range trackedRange)
        {
            TrackedRange = trackedRange;

            Globals.ThisAddIn.Application.SheetChange += (sheet, _) =>
            {
                Worksheet worksheet = (Worksheet)sheet;
                worksheet.Change += (range) =>
                {
                    if (range == TrackedRange)
                    {
                        TriggerUpdate(range);
                    }
                };
            };
        }
    }
}
