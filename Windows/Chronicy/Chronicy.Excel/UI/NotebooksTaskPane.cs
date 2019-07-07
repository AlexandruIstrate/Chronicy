using Chronicy.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class NotebooksTaskPane : UserControl
    {
        private List<Notebook> notebooks;
        public List<Notebook> Notebooks
        {
            get => notebooks;
            set { notebooks = value ?? throw new ArgumentNullException(nameof(notebooks)); LoadData(); }
        }

        public NotebooksTaskPane(List<Notebook> notebooks)
        {
            Notebooks = notebooks ?? throw new ArgumentNullException(nameof(notebooks));
            InitializeComponent();
        }

        public NotebooksTaskPane()
        {
            InitializeComponent();
        }

        private void LoadData()
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

            notebookComboBox.SelectedIndexChanged += (sender, args) =>
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
            };
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
