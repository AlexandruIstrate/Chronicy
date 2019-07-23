using Microsoft.Office.Tools;
using System;
using System.Windows.Forms;

namespace Chronicy.Excel.UI.Pane
{
    public class TaskPane<T> where T : UserControl
    {
        public CustomTaskPane Pane { get; }
        public T Control { get; set; }

        public bool Visible
        {
            get => Pane.Visible;
            set => SetVisible(value);
        }

        public TaskPane(string title, T control)
        {
            Control = control;

            ITaskPaneFactory factory = new TaskPaneFactory();
            Pane = factory.Create(title, Control);

            control.VisibleChanged += (sender, args) => Visible = control.Visible;
        }

        private void SetVisible(bool state)
        {
            try
            {
                Pane.Visible = state;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
        }
    }
}
