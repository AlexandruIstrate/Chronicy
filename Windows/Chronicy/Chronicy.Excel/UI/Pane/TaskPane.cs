using Chronicy.Information;
using Microsoft.Office.Tools;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chronicy.Excel.UI.Pane
{
    public class TaskPane<T> where T : UserControl
    {
        public CustomTaskPane Pane { get; }
        public T Control { get; set; }

        public bool Visible
        {
            get => GetVisible();
            set => SetVisible(value);
        }

        public TaskPane(string title, T control)
        {
            Control = control;

            ITaskPaneFactory factory = new TaskPaneFactory();
            Pane = factory.Create(title, Control);
            Pane.Width = control.Width;

            control.VisibleChanged += (sender, args) => Visible = control.Visible;
        }

        private bool GetVisible()
        {
            try
            {
                return Pane.Visible;
            }
            catch (COMException)
            {
                // If we get here, then the control has been disposed.
                // This can happen if the pane is still open when the app is closed

                InformationDispatcher.Default.Dispatch("Attempt to query the visibility of a disposed pane", InformationKind.Warning);

                return false;
            }
        }

        private void SetVisible(bool state)
        {
            try
            {
                Pane.Visible = state;
            }
            catch (COMException)
            {
                // If we get here, then the control has been disposed.
                // This can happen if the pane is still open when the app is closed

                InformationDispatcher.Default.Dispatch("Attempt to change the visibility of a disposed pane", InformationKind.Warning);
            }
        }
    }
}
