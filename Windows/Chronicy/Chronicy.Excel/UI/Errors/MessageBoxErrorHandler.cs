using System.Windows.Forms;

namespace Chronicy.Excel.UI.Errors
{
    public class MessageBoxErrorHandler : IGridViewErrorHandler
    {
        public DataGridViewDataErrorEventHandler ErrorHandler => (sender, args) =>
        {
            MessageBox.Show(args.Exception.Message, "Error Updating Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
        };
    }
}
