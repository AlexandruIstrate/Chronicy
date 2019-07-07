using Chronicy.Data;
using System.Data;

namespace Chronicy.Excel.UI
{
    public partial class EditStackTaskPane : EditTaskPane
    {
        private Stack stack;
        public Stack EditedStack
        {
            get => stack;
            set { stack = value; LoadData(); }
        }

        public EditStackTaskPane(Stack stack)
        {
            this.stack = stack;

            InitializeComponent();
            LoadData();
        }

        public EditStackTaskPane()
        {
            InitializeComponent();
        }

        public override void OnOk()
        {
            if (stack == null)
            {
                return;
            }

            stack.Name = nameTextBox.Text;
        }

        private void LoadData()
        {
            if (stack == null)
            {
                return;
            }

            nameTextBox.Text = stack.Name;
            fieldsDataGrid.DataSource = CreateDataSet(stack);
        }

        private DataSet CreateDataSet(Stack stack)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));

            foreach (CustomField field in stack.Fields)
            {
                DataRow row = dataTable.NewRow();
                row[0] = field.Name;
                row[1] = field.Type.ToString();

                dataTable.Rows.Add(row);
            }

            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            LoadData();
        }
    }
}
