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
            
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
