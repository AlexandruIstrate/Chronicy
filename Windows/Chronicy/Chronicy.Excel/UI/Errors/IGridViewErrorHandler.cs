using System.Windows.Forms;

namespace Chronicy.Excel.UI.Errors
{
    public interface IGridViewErrorHandler
    {
        DataGridViewDataErrorEventHandler ErrorHandler { get; }
    }
}
