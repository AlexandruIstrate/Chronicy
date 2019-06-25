using System.Windows.Forms;
using Microsoft.Office.Tools;

namespace Chronicy.Excel.UI.Pane
{
    public class TaskPaneFactory : ITaskPaneFactory
    {
        public CustomTaskPane Create(string title, UserControl control)
        {
            return Globals.ThisAddIn.CustomTaskPanes.Add(control, title);
        }
    }
}
