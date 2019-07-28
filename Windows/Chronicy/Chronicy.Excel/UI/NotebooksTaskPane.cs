using Chronicy.Data;
using Chronicy.Excel.Utils;
using Chronicy.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class NotebooksTaskPane : EditTaskPane
    {
        private int lastSelectedNotebookIndex;

        private List<Notebook> notebooks;
        public List<Notebook> Notebooks
        {
            get => notebooks;
            set { notebooks = value ?? throw new ArgumentNullException(nameof(Notebooks)); LoadData(); }
        }

        public Notebook SelectedNotebook
        {
            get
            {
                if (notebookComboBox.SelectedIndex < 0)
                {
                    throw new Exception("The current Notebook selection is invalid");
                }

                return notebooks[notebookComboBox.SelectedIndex];
            }
        }

        public Stack SelectedStack
        {
            get
            {
                if (notebookComboBox.SelectedIndex < 0)
                {
                    throw new Exception("The current Stack selection is invalid");
                }

                return SelectedNotebook.Stacks[stackComboBox.SelectedIndex];
            }
        }

        public NotebooksTaskPane(List<Notebook> notebooks)
        {
            Notebooks = notebooks ?? throw new ArgumentNullException(nameof(notebooks));
            InitializeComponent();
        }

        public NotebooksTaskPane()
        {
            InitializeComponent();
            fieldsGridView.AutoGenerateColumns = true;
        }

        public override void OnOk()
        {
            SaveData();
        }

        private void LoadData()
        {
            LoadNotebooks();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        private void OnNotebookChanged(object sender, EventArgs e)
        {
            fieldsGridView.DataSource = CreateDataSource(SelectedNotebook);
            LoadStacks();

            lastSelectedNotebookIndex = notebookComboBox.SelectedIndex;
        }

        private void OnNotebookChangeCommited(object sender, EventArgs e)
        {
            SaveData();
        }

        private void OnStackChanged(object sender, EventArgs e)
        {
            fieldsGridView.DataMember = SelectedStack.Name;
        }

        private void LoadNotebooks()
        {
            notebookComboBox.Items.Clear();

            foreach (Notebook notebook in notebooks)
            {
                notebookComboBox.Items.Add(notebook.Name);
            }

            if (notebooks.Count > 0)
            {
                notebookComboBox.SelectedIndex = 0;
            }
        }

        private void LoadStacks()
        {
            stackComboBox.Items.Clear();

            Notebook selected = notebooks[notebookComboBox.SelectedIndex];

            foreach (Stack stack in selected.Stacks)
            {
                stackComboBox.Items.Add(stack.Name);
            }

            if (selected.Stacks.Count > 0)
            {
                stackComboBox.SelectedIndex = 0;
            }
        }

        private void SaveData()
        {
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

            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                DataTable dataTable = dataSet.Tables[i];
                Stack stack = notebooks[lastSelectedNotebookIndex].Stacks[i];

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
        }

        private DataSet CreateDataSource(Notebook notebook)
        {
            DataSet dataSet = new DataSet();

            foreach (Stack stack in notebook.Stacks)
            {
                DataTable dataTable = new DataTable(stack.Name);
                dataTable.Columns.AddRange(DataUtils.CreateDataColumns<CustomField>(ignoredColumns: new string[] { "ID", "Value", "SerializedValue" }));

                foreach (CustomField field in stack.Fields)
                {
                    DataRow row = dataTable.NewRow();
                    row.ItemArray = new object[] { field.Name, field.Type };

                    dataTable.Rows.Add(row);
                }

                dataSet.Tables.Add(dataTable); 
            }

            return dataSet;
        }
    }
}
