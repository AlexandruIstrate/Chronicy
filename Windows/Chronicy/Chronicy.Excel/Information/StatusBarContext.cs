using Chronicy.Information;
using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Information
{
    public class StatusBarContext : IInformationContext
    {
        public void MessageDispatched(string message, InformationKind informationKind)
        {
            Application app = Globals.ThisAddIn.Application;
            app.StatusBar = "Test";
        }
    }
}
