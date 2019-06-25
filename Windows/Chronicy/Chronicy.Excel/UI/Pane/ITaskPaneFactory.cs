using Microsoft.Office.Tools;
using System.Windows.Forms;

namespace Chronicy.Excel.UI.Pane
{
    public interface ITaskPaneFactory
    {
        CustomTaskPane Create(string title, UserControl control);
    }
}
