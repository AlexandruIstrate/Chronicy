using Chronicy.Excel.Information;
using Chronicy.Information;
using Microsoft.Office.Interop.Excel;
using System.Timers;

namespace Chronicy.Excel.User
{
    public class SelectCellRangeAction : IUserAction<Range>
    {
        private StatusBarContext informationContext;
        private Timer timer;

        private Range lastRange;

        public SelectCellRangeAction()
        {
            informationContext = new StatusBarContext
            {
                RestoresInitialState = true,
            };

            timer = new Timer
            {
                Interval = 5 * 1000,
                AutoReset = false
            };
            timer.Elapsed += OnTimerElapsed;
        }

        public override void Run()
        {
            InformationDispatcher.Default.Dispatch("Select any cell range...", informationContext);
            Globals.ThisAddIn.Application.SheetSelectionChange += OnSheetSelectionChange;
        }

        private void OnSheetSelectionChange(object sheet, Range range)
        {
            InformationDispatcher.Default.Dispatch($"Selection made. Waiting { timer.Interval / 1000 } seconds before saving...", informationContext);
            lastRange = range;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            InformationDispatcher.Default.Dispatch("Range saved", informationContext);
            TriggerEvent(lastRange);
            Globals.ThisAddIn.Application.SheetSelectionChange -= OnSheetSelectionChange;
        }
    }
}
