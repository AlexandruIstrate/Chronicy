using Chronicy.Excel.Information;
using Chronicy.Information;
using System.Timers;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace Chronicy.Excel.User
{
    public class SelectSheetAction : IUserAction<ExcelWorksheet>
    {
        private StatusBarContext informationContext;
        private Timer timer;

        private ExcelWorksheet lastWorksheet;

        public SelectSheetAction()
        {
            informationContext = new StatusBarContext()
            {
                RestoresInitialState = true
            };

            timer = new Timer()
            {
                Interval = 5 * 1000,
                AutoReset = false
            };
            timer.Elapsed += OnTimerElapsed;
        }

        public override void Run()
        {
            InformationDispatcher.Default.Dispatch("Select any sheet...", informationContext);
            Globals.ThisAddIn.Application.SheetActivate += OnSheetActivated;
        }

        private void OnSheetActivated(object sheet)
        {
            InformationDispatcher.Default.Dispatch($"Selection made. Waiting { timer.Interval / 1000 } seconds before saving...", informationContext);
            lastWorksheet = (ExcelWorksheet) sheet;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            InformationDispatcher.Default.Dispatch("Saved", informationContext);
            TriggerEvent(lastWorksheet);
            Globals.ThisAddIn.Application.SheetActivate -= OnSheetActivated;
        }
    }
}
