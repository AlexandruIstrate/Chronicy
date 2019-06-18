using Chronicy.Excel.Utils;
using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Chronicy.Excel.Tracking
{
    public class RangeTrackable : ITrackable<Range>
    {
        public Range TrackedRange { get; set; }

        public RangeTrackable(Range trackedRange)
        {
            TrackedRange = trackedRange;
            Enabled = true;

            InitializeEvents();
        }

        public RangeTrackable()
        {
            Enabled = false;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Globals.ThisAddIn.Application.SheetChange += (sheet, range) =>
            {
                if (TrackedRange == null)
                {
                    return;
                }

                if (TrackedRange.Contains(range))
                {
                    TriggerUpdate(range);
                }
            };
        }
    }
}
