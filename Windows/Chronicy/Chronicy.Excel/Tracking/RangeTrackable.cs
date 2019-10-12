using System;
using Chronicy.Excel.Utils;
using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Tracking
{
    /// <summary>
    /// Tracks changes made to a cell range.
    /// </summary>
    public class RangeTrackable : ITrackable
    {
        public override Type ValueType => typeof(Range);

        public RangeTrackable(Range trackedRange)
        {
            TrackedValue = trackedRange;
            Enabled = true;

            InitializeEvents();
        }

        public RangeTrackable()
        {
            Enabled = true;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            Globals.ThisAddIn.Application.SheetChange += (sheet, range) =>
            {
                if (TrackedValue == null)
                {
                    return;
                }

                Range intersection = (TrackedValue as Range).Intersection(range);

                if (intersection != null)
                {
                    TriggerUpdate(intersection);
                }
            };
        }
    }
}
