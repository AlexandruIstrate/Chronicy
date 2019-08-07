using Chronicy.Data;
using Chronicy.Excel.UI.Errors;
using Chronicy.Utils;
using System;
using System.Data;

namespace Chronicy.Excel.UI
{
    public partial class EditStackTaskPane : EditTaskPane
    {
        private Stack stack;
        public Stack EditedStack
        {
            get => stack;
            set { stack = value ?? throw new ArgumentNullException(nameof(EditedStack)); LoadData(); }
        }

        public EditStackTaskPane(Stack stack)
        {
            this.stack = stack;

            InitializeComponent();
            InitializeGrid();
        }

        public EditStackTaskPane()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void OnOk()
        {
            SaveData();
        }

        public override void OnCancel()
        {
            DataTable dataTable = (DataTable)fieldsGridView.DataSource;
            dataTable.RejectChanges();
        }

        private void LoadData()
        {
            nameTextBox.Text = stack.Name;
            fieldsGridView.DataSource = CreateDataSource(stack);
        }

        private void SaveData()
        {
            stack.Name = nameTextBox.Text;
            DataTable dataTable = (DataTable)fieldsGridView.DataSource;

            if (dataTable == null)
            {
                return;
            }

            if (dataTable.HasErrors)
            {
                return;
            }

            stack.Fields.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                CustomField customField = new CustomField
                {
                    Name = (string)row[nameof(Name)],
                    Type = (FieldType)row[nameof(Type)]
                };

                stack.Fields.Add(customField);
            }
        }

        private DataTable CreateDataSource(Stack stack)
        {
            DataTable dataTable = new DataTable(stack.Name);
            dataTable.Columns.AddRange(DataUtils.CreateDataColumns<CustomField>(ignoredColumns: new string[] { "ID", "Value", "SerializedValue" }));

            foreach (CustomField field in stack.Fields)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow.ItemArray = new object[] { field.Name, field.Type };

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        private void InitializeGrid()
        {
            fieldsGridView.AutoGenerateColumns = true;

            IGridViewErrorHandler errorHandler = new MessageBoxErrorHandler();
            fieldsGridView.DataError += errorHandler.ErrorHandler;
        }
    }
}
