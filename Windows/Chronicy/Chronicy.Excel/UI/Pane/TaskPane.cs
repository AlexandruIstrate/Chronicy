using Microsoft.Office.Tools;
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
            set => Pane.Visible = value;
        }

        public TaskPane(string title, T control)
        {
            Control = control;

            ITaskPaneFactory factory = new TaskPaneFactory();
            Pane = factory.Create(title, Control);

            control.VisibleChanged += (sender, args) => Visible = control.Visible;
        }
    }
}
