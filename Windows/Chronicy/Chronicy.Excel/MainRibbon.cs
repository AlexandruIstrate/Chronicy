using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Excel.App;
using Chronicy.Excel.Data;
using Chronicy.Excel.History;
using Chronicy.Excel.Information;
using Chronicy.Excel.Properties;
using Chronicy.Excel.Tracking;
using Chronicy.Excel.UI;
using Chronicy.Excel.UI.Pane;
using Chronicy.Excel.User;
using Chronicy.Excel.Utils;
using Chronicy.Information;
using Chronicy.Tracking;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using CategoryRecord = System.Collections.Generic.Dictionary<string, System.Collections.Generic.IList<Chronicy.Excel.History.HistoryItem>>;

namespace Chronicy.Excel
{
    public partial class MainRibbon
    {
        private MessageBoxContext informationContext = new MessageBoxContext();

        private IExcelExtension extension;
        private ExcelTracker tracker;

        public MainRibbon(IExcelExtension extension) : this()
        {
            this.extension = extension;
        }

        private void SetGroupState(bool enabled)
        {
            enableButton.Visible = enabled;
            selectionGroup.Visible = enabled;
            trackingGroup.Visible = enabled;
            toolsGroup.Visible = enabled;
        }

        private void SetupActivationCallbacks()
        {
            extension.ConnectionChanged += (connected) => SetGroupState(connected);
            extension.StateChanged += (enabled) => extension.Tracking.Enabled = enabled;

            SetGroupState(false);
            extension.Enabled = false;
        }

        private void InitializeTrackingMenus()
        {
            tracker = new ExcelTracker();
            tracker.Register<Workbook>(new WorkbookTrackable());
            tracker.Register<Worksheet>(new WorksheetTrackable());
            tracker.Register<Range>(new RangeTrackable());

            // Upon clicking the checkbox, the StateUpdated event is triggered, which sets the checkbox to its value again.
            // This is not the end of the world, however we're wasting a call by creating this cycle

            workbookEnableCheckBox.Click += (sender, args) => { tracker.Get<Workbook>().Enabled = (sender as RibbonCheckBox).Checked; };
            sheetEnableCheckBox.Click += (sender, args) => { tracker.Get<Worksheet>().Enabled = (sender as RibbonCheckBox).Checked; };
            cellsEnableCheckBox.Click += (sender, args) => { tracker.Get<Range>().Enabled = (sender as RibbonCheckBox).Checked; };

            ITrackable workbook = tracker.Get<Workbook>();
            workbook.StateUpdated += (enabled) => { workbookEnableCheckBox.Checked = enabled; };
            workbook.TrackedValueUpdated += (value) => { workbookCurrentLabel.Label = "Tracked workbook: " + (value as Workbook).Name; };

            ITrackable sheet = tracker.Get<Worksheet>();
            sheet.StateUpdated += (enabled) => { sheetEnableCheckBox.Checked = enabled; };
            sheet.TrackedValueUpdated += (value) => { sheetCurrentLabel.Label = "Tracked sheet: " + (value as Worksheet).Name; };

            ITrackable cells = tracker.Get<Range>();
            cells.StateUpdated += (enabled) => { cellsEnableCheckBox.Checked = enabled; };
            cells.TrackedValueUpdated += (value) => { cellsCurrentLabel.Label = "Tracked range: " + (value as Range).ToDisplayAddressString(); };
        }

        private void InitializeNotebooks()
        {
            extension.NotebooksUpdated += (notebooks) =>
            {
                LoadNotebooks(new List<Notebook>(notebooks));
                LoadStacks();
            };

            extension.Notebooks.NotebookSelectionChanged += (sender, args) => extension.SelectNotebook(extension.Notebooks.SelectedNotebook);
            extension.Notebooks.StackSelectionChanged += (sender, args) => extension.SelectStack(extension.Notebooks.SelectedStack);
        }

        private void LoadDataSources()
        {
            DataSourceType[] sources = { DataSourceType.Local, DataSourceType.Web };

            foreach (DataSourceType source in sources)
            {
                RibbonDropDownItem ribbonDropDown = Factory.CreateRibbonDropDownItem();
                ribbonDropDown.Label = source.ToString();
                dataSourceDropDown.Items.Add(ribbonDropDown);
            }
        }

        private void LoadNotebooks(List<Notebook> items = null)
        {
            List<Notebook> notebooks = items ?? extension.Notebooks.GetNotebooks();

            RibbonUI.InvalidateControl(notebookDropDown.Id);
            notebookDropDown.Items.Clear();

            if (notebooks.Count < 1)
            {
                return;
            }

            foreach (Notebook notebook in notebooks)
            {
                RibbonDropDownItem dropDownItem = Factory.CreateRibbonDropDownItem();
                dropDownItem.Label = notebook.Name;
                notebookDropDown.Items.Add(dropDownItem);
            }

            // Try to reselect the already selected notebook
            Notebook selected = extension.Notebooks.SelectedNotebook;

            // Otherwise, use the first item
            if (selected == null)
            {
                selected = notebooks[0];
                extension.Notebooks.SelectNotebook(selected);
            }

            extension.SelectNotebook(selected);

            int index = notebooks.FindIndex((item) => item.ID == selected.ID);
            notebookDropDown.SelectedItemIndex = index;
        }

        private void LoadStacks()
        {
            if (extension.Notebooks.SelectedNotebook == null)
            {
                // We don't have any notebooks
                return;
            }

            List<Stack> stacks = extension.Notebooks.SelectedNotebook.Stacks
                                    .FindAll((item) => FieldTemplates.ExtensionDefault.Matches(new FieldTemplate(item.Fields)));

            RibbonUI.InvalidateControl(stackDropDown.Id);
            stackDropDown.Items.Clear();

            if (stacks.Count < 1)
            {
                return;
            }

            foreach (Stack stack in stacks)
            {
                RibbonDropDownItem dropDownItem = Factory.CreateRibbonDropDownItem();
                dropDownItem.Label = stack.Name;
                stackDropDown.Items.Add(dropDownItem);
            }

            // Try to reselect the already selected stack
            Stack selected = extension.Notebooks.SelectedStack;

            // Otherwise, select the first item
            if (selected == null)
            {
                selected = stacks[0];
                extension.Notebooks.SelectStack(selected);
            }

            extension.SelectStack(selected);

            int index = stacks.FindIndex((item) => item.Name == selected.Name);
            stackDropDown.SelectedItemIndex = index;
        }

        private void OnRibbonLoad(object sender, RibbonUIEventArgs e)
        {
            InitializeTrackingMenus();
            SetupActivationCallbacks();
            InitializeNotebooks();

            extension.StateChanged += (enabled) => 
            {
                enableButton.Checked = enabled;
                enableButton.Label = enabled ? "Enabled" : "Disabled";
            };
            extension.ConnectionChanged += (connected) => { connectButton.Visible = !connected; };

            extension.OnRibbonLoad();

            try
            {
                extension.Connect();

                LoadDataSources();
                LoadNotebooks();
                LoadStacks();
            }
            catch (EndpointConnectionException)
            {
                // If we can't connect when we initialize the extension, just ignore for now and let the user try to connect
                // We should change this in the future to use some kind of passive, start-up display system
                // However, for now, we just log the failure

                InformationDispatcher.Default.Dispatch("Could not auto-connect to service!", DebugLogContext.Current, InformationKind.Error);
            }
        }

        private void OnRibbonClose(object sender, EventArgs e)
        {
            extension.OnRibbonUnload();
        }

        private void OnConnectClicked(object sender, RibbonControlEventArgs e)
        {
            try
            {
                extension.Connect();

                LoadDataSources();
                LoadNotebooks();
                LoadStacks();
            }
            catch (EndpointConnectionException)
            {
                InformationDispatcher.Default.Dispatch("Could not connect to the Chronicy service!\n" +
                                                       "Make sure that the service is installed and running.",
                                                        informationContext, 
                                                        InformationKind.Error);
            }
            catch (Exception ex)
            {
                InformationDispatcher.Default.Dispatch("An unknown error occurred while trying to connect to the service: " + ex.Message,
                                                        informationContext);
            }
        }

        private void OnEnableToggled(object sender, RibbonControlEventArgs e)
        {
            extension.Enabled = enableButton.Checked;
        }

        private void OnDataSourceChanged(object sender, RibbonControlEventArgs e)
        {
            // TODO: Support dynamic data sources

            bool success = Enum.TryParse(dataSourceDropDown.SelectedItem.Label, out DataSourceType type);

            if (!success)
            {
                throw new ArgumentException("The selected value does not match any available data sources");
            }

            extension.Notebooks.ClearSelection();
            extension.SelectDataSource(type);

            LoadNotebooks();
            LoadStacks();
        }

        private void OnNotebookChanged(object sender, RibbonControlEventArgs e)
        {
            extension.Notebooks.SelectNotebook(notebookDropDown.SelectedItem.Label);
            LoadStacks();
        }

        private void OnStackChanged(object sender, RibbonControlEventArgs e)
        {
            extension.Notebooks.SelectStack(stackDropDown.SelectedItem.Label);
        }

        private void OnNewNotebookClicked(object sender, RibbonControlEventArgs e)
        {
            EditNotebookTaskPane control = new EditNotebookTaskPane();
            control.EditedNotebook = new Notebook(string.Empty);
            control.Confirmed += (s, args) =>
            {
                extension.Notebooks.AddNotebook(control.EditedNotebook);

                LoadNotebooks();
                LoadStacks();
            };

            TaskPane<EditNotebookTaskPane> taskPane = new TaskPane<EditNotebookTaskPane>("Edit Notebook", control);
            taskPane.Visible = true;
        }

        private void OnNewStackClicked(object sender, RibbonControlEventArgs e)
        {
            EditStackTaskPane control = new EditStackTaskPane();
            control.EditedStack = new Stack(string.Empty);
            control.Confirmed += (s, args) =>
            {
                extension.Notebooks.AddStack(control.EditedStack);
                LoadStacks();
            };

            TaskPane<EditStackTaskPane> taskPane = new TaskPane<EditStackTaskPane>("Edit Stack", control);
            taskPane.Visible = true;
        }

        private void OnViewAllClicked(object sender, RibbonControlEventArgs e)
        {
            NotebooksTaskPane control = new NotebooksTaskPane();
            control.Notebooks = extension.Notebooks.GetNotebooks();
            control.Confirmed += (s, args) =>
            {
                foreach (Notebook notebook in control.Notebooks)
                {
                    extension.Notebooks.UpdateNotebook(notebook);
                }

                LoadStacks();
            };

            TaskPane<NotebooksTaskPane> taskPane = new TaskPane<NotebooksTaskPane>("Notebooks", control);
            taskPane.Visible = true;
        }

        private void OnHistoryMenuLoad(object sender, RibbonControlEventArgs e)
        {
            historyMenu.Items.Clear();

            CategoryRecord record = extension.History.GetItemsByCategory();

            foreach (string key in record.Keys)
            {
                RibbonButton categoryLabel = Factory.CreateRibbonButton();
                categoryLabel.Description = key;
                categoryLabel.Enabled = false;
                historyMenu.Items.Add(categoryLabel);

                IEnumerable<HistoryItem> items = record[key];

                foreach (HistoryItem item in items)
                {
                    RibbonButton historyItem = Factory.CreateRibbonButton();
                    historyItem.Image = Resources.IconHistoryItem32;
                    historyItem.Label = item.Title;
                    historyItem.Description = item.Description;
                    historyMenu.Items.Add(historyItem);
                }
            }
        }

        private void OnSyncClicked(object sender, RibbonControlEventArgs e)
        {
            try
            {
                extension.Sync();

                LoadNotebooks();
                LoadStacks();
            }
            catch (EndpointConnectionException ex)
            {
                InformationDispatcher.Default.Dispatch(ex.Message, informationContext, InformationKind.Error);
            }
        }

        private void OnTrackWorkbook(object sender, RibbonControlEventArgs e)
        {
            // 1. Submit the current workbook to ExcelTracker
            ITrackable trackable = tracker.Get<Workbook>();
            trackable.ValueUpdated += (value) => extension.Tracking.Post<Workbook>(TrackingEvent.Create((Workbook)value));
            trackable.TrackedValue = Globals.ThisAddIn.Application.ActiveWorkbook;
            trackable.Enabled = true;
        }

        private void OnTrackSheet(object sender, RibbonControlEventArgs e)
        {
            // 1. Ask the user to pick a sheet
            SelectSheetAction action = new SelectSheetAction();
            action.ActionCompleted += (sheet) => 
            {
                /* 2. Submit the sheet to ExcelTracker */
                ITrackable trackable = tracker.Get<Worksheet>();
                trackable.ValueUpdated += (value) => extension.Tracking.Post<Worksheet>(TrackingEvent.Create((Worksheet)value));
                trackable.TrackedValue = sheet;
                trackable.Enabled = true;
            };
            action.Run();
        }

        private void OnTrackCellRange(object sender, RibbonControlEventArgs e)
        {
            // 1. Ask the user to pick a range
            SelectCellRangeAction action = new SelectCellRangeAction();
            action.ActionCompleted += (range) => 
            {
                /* 2. Submit the range to ExcelTracker */
                ITrackable trackable = tracker.Get<Range>();
                trackable.ValueUpdated += (value) => extension.Tracking.Post<Range>(TrackingEvent.Create((Range)value));
                trackable.TrackedValue = range;
                trackable.Enabled = true;
            };
            action.Run();
        }

        private void OnLoginClicked(object sender, RibbonControlEventArgs e)
        {
            LoginTaskPane control = new LoginTaskPane(extension.CredentialsManager);

            TaskPane<LoginTaskPane> taskPane = new TaskPane<LoginTaskPane>("Chronicy Login", control);
            taskPane.Visible = true;
        }

        private void OnHelpClicked(object sender, RibbonControlEventArgs e)
        {
            ExternalLink link = new ExternalLink(Properties.Resources.LINK_HELP);
            link.Open();
        }

        private void OnReportBugClicked(object sender, RibbonControlEventArgs e)
        {
            ExternalLink link = new ExternalLink(Properties.Resources.LINK_SUBMIT_BUG);
            link.Open();
        }

        private void OnViewGitHubClicked(object sender, RibbonControlEventArgs e)
        {
            ExternalLink link = new ExternalLink(Properties.Resources.LINK_PROJECT_PAGE);
            link.Open();
        }
    }
}
