using Chronicy.Data;
using Chronicy.Excel.UI.Pane;
using System;

namespace Chronicy.Excel.UI
{
    public partial class EditNotebookTaskPane : EditTaskPane
    {
        private Notebook notebook;
        public Notebook EditedNotebook
        {
            get => notebook;
            set { notebook = value ?? throw new ArgumentNullException(nameof(EditedNotebook)); LoadData(); }
        }

        public EditNotebookTaskPane(Notebook notebook)
        {
            this.notebook = notebook ?? throw new ArgumentNullException(nameof(notebook));
            InitializeComponent();
        }

        public EditNotebookTaskPane()
        {
            InitializeComponent();
        }

        public override void OnOk()
        {
            notebook.Name = nameTextBox.Text;
        }

        private void LoadData()
        {
            nameTextBox.Name = notebook.Name;

            stacksListView.Items.Clear();

            foreach (Stack stack in notebook.Stacks)
            {
                stacksListView.Items.Add(stack.Name);
            }
        }

        private bool HasSelection() => stacksListView.SelectedItems.Count > 0;

        private Stack GetSelectedStack()
        {
            if (!HasSelection())
            {
                throw new InvalidOperationException("No item is selected");
            }

            return notebook.Stacks[stacksListView.SelectedItems[0].Index];
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        private void OnSelectedStackChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = HasSelection();
            editButton.Enabled = HasSelection();
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            EditStackTaskPane control = new EditStackTaskPane();
            control.EditedStack = new Stack();
            control.Confirmed += (s, args) =>
            {
                notebook.Add(control.EditedStack);
                LoadData();
            };

            TaskPane<EditStackTaskPane> taskPane = new TaskPane<EditStackTaskPane>("New Stack", control);
            taskPane.Visible = true;
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            EditStackTaskPane control = new EditStackTaskPane();
            control.EditedStack = GetSelectedStack();
            control.Confirmed += (s, args) =>
            {
                LoadData();
            };

            TaskPane<EditStackTaskPane> taskPane = new TaskPane<EditStackTaskPane>("Edit Stack", control);
            taskPane.Visible = true;
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            notebook.Stacks.Remove(GetSelectedStack());
        }
        
    }
}
