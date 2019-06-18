using Chronicy.Information;
using Microsoft.Office.Interop.Excel;
using System.Timers;

namespace Chronicy.Excel.Information
{
    public class StatusBarContext : IInformationContext
    {
        private Application application;
        private string initialState;

        public string SavedState { get; private set; }
        public bool RestoresInitialState { get; set; }

        public StatusBarContext()
        {
            application = Globals.ThisAddIn.Application;
            initialState = application.StatusBar.ToString();
        }

        ~StatusBarContext()
        {
            if (RestoresInitialState)
            {
                application.StatusBar = initialState;
            }
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            application.StatusBar = message;
        }

        public void Save()
        {
            SavedState = application.StatusBar;
        }

        public void Restore()
        {
            application.StatusBar = SavedState;
        }
    }
}
