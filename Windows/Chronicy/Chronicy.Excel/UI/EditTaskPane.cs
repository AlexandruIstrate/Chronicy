using Microsoft.Office.Tools;
using System;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class EditTaskPane : UserControl
    {
        public delegate void TaskPaneCallback(object sender, TaskPaneArgs args);

        public event TaskPaneCallback Confirmed;
        public event TaskPaneCallback Canceled;

        public EditTaskPane()
        {
            InitializeComponent();
        }

        public virtual void OnOk() { }
        public virtual void OnCancel() { }

        private void OnOkClicked(object sender, EventArgs e)
        {
            OnOk();
            TaskPaneArgs args = new TaskPaneArgs();

            Confirmed?.Invoke(this, args);
            Visible = args.KeepOpen;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            OnCancel();
            TaskPaneArgs args = new TaskPaneArgs();

            Canceled?.Invoke(this, args);
            Visible = args.KeepOpen;
        }
    }

    public class TaskPaneArgs : EventArgs
    {
        public bool KeepOpen { get; set; }
    }
}
