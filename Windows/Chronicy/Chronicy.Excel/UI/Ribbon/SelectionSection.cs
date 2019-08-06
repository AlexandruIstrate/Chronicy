using Chronicy.Data.Storage;
using Chronicy.Excel.App;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronicy.Excel.UI.Ribbon
{
    public class SelectionSection : Section
    {
        public MainRibbon Ribbon { get; set; }

        public AppExtension Extension { get; set; }

        public SelectionSection(MainRibbon ribbon, AppExtension extension)
        {
            Ribbon = ribbon;
            Extension = extension;

            InitializeCalls();
        }

        public void ChangeDataSource()
        {
            // TODO: Support dynamic data sources

            bool success = Enum.TryParse(Ribbon.dataSourceDropDown.SelectedItem.Label, out DataSourceType type);

            if (!success)
            {
                throw new ArgumentException("The selected value does not match any available data sources");
            }

            Ribbon.notebookDropDown.Items.Clear();
            Ribbon.stackDropDown.Items.Clear();

            Extension.Notebooks.ClearSelection();
            Extension.SelectDataSource(type);
        }

        public void ChangeNotebook()
        {
            Extension.Notebooks.SelectNotebook(Ribbon.notebookDropDown.SelectedItem.Label);
        }

        public void ChangeStack()
        {
            Extension.Notebooks.SelectStack(Ribbon.stackDropDown.SelectedItem.Label);
        }

        public void NewNotebook()
        {

        }

        public void NewStack()
        {

        }

        public void ViewAll()
        {

        }

        private void InitializeCalls()
        {
            Actions[ActionIdentifiers.ChangeDataSource] = () => ChangeDataSource();
            Actions[ActionIdentifiers.ChangeNotebook] = () => ChangeNotebook();
            Actions[ActionIdentifiers.ChangeStack] = () => ChangeStack();

            Actions[ActionIdentifiers.NewNotebook] = () => NewNotebook();
            Actions[ActionIdentifiers.NewStack] = () => NewStack();
            Actions[ActionIdentifiers.ViewAll] = () => ViewAll();
        }

        public static class ActionIdentifiers
        {
            public const string ChangeDataSource = "ChangeDataSource";
            public const string ChangeNotebook = "ChangeNotebook";
            public const string ChangeStack = "ChangeStack";

            public const string NewNotebook = "NewNotebook";
            public const string NewStack = "NewStack";
            public const string ViewAll = "ViewAll";
        }
    }
}
