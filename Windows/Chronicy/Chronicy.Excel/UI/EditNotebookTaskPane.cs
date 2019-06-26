using Chronicy.Data;
using System;

namespace Chronicy.Excel.UI
{
    public partial class EditNotebookTaskPane : EditTaskPane
    {
        private Notebook notebook;
        public Notebook EditedNotebook
        {
            get => notebook;
            set { notebook = value; LoadData(); }
        }

        public EditNotebookTaskPane(Notebook notebook)
        {
            this.notebook = notebook;
            InitializeComponent();
        }

        public EditNotebookTaskPane()
        {
            InitializeComponent();
        }

        public override void OnOk()
        {
            if (notebook == null)
            {
                return;
            }

            notebook.Name = nameTextBox.Text;
        }

        private void LoadData()
        {
            if (notebook == null)
            {
                return;
            }

            nameTextBox.Name = notebook.Name;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
