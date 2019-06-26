using System;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class EditTaskPane : UserControl
    {
        public event EventHandler Confirmed;
        public event EventHandler Canceled;

        public EditTaskPane()
        {
            InitializeComponent();
        }

        public virtual void OnOk()
        {
        }

        public virtual void OnCancel()
        {
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            OnOk();
            Confirmed?.Invoke(this, EventArgs.Empty);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            OnCancel();
            Canceled?.Invoke(this, EventArgs.Empty);
        }
    }
}
