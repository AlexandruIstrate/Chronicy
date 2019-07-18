using Chronicy.Data;
using Chronicy.Excel.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
            fieldsGridView.AutoGenerateColumns = true;
        }

        public EditStackTaskPane()
        {
            InitializeComponent();
            fieldsGridView.AutoGenerateColumns = true;
        }

        public override void OnOk()
        {
            if (stack == null)
            {
                return;
            }

            SaveData();
        }

        private void LoadData()
        {
            if (stack == null)
            {
                return;
            }

            nameTextBox.Text = stack.Name;

            fieldsGridView.DataSource = CreateDataSource(stack);
            fieldsGridView.DataMember = stack.Name;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SaveData()
        {
            stack.Name = nameTextBox.Text;

            DataSet dataSet = (DataSet)fieldsGridView.DataSource;

            if (dataSet == null)
            {
                return;
            }

            if (dataSet.HasErrors)
            {
                MessageBox.Show("The current data set contains errors!", "Cannot Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!dataSet.HasChanges())
            {
                return;
            }

            DataTable dataTable = dataSet.Tables[0];
            EditedStack.Fields.Clear();

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

        private DataSet CreateDataSource(Stack stack)
        {
            DataSet dataSet = new DataSet();

            //foreach (Stack stack in notebook.Stacks)
            //{
                DataTable dataTable = new DataTable(stack.Name);
                dataTable.Columns.AddRange(CreateDataColumns<CustomField>(ignoredColumns: new string[] { "ID", "Value" }));

                foreach (CustomField field in stack.Fields)
                {
                    DataRow row = dataTable.NewRow();
                    row.ItemArray = new object[] { field.Name, field.Type };

                    dataTable.Rows.Add(row);
                }

                dataSet.Tables.Add(dataTable);
            //}

            return dataSet;
        }

        private DataColumn[] CreateDataColumns<T>(params string[] ignoredColumns)
        {
            Type type = typeof(T);

            List<DataColumn> columns = new List<DataColumn>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (ignoredColumns.ToList().Contains(property.Name))
                {
                    // Skip ignored columns
                    continue;
                }

                if (property.PropertyType.IsCollection())
                {
                    // Skip child collections
                    continue;
                }

                columns.Add(new DataColumn { ColumnName = property.Name, DataType = property.PropertyType });
            }

            return columns.ToArray();
        }
    }
}
