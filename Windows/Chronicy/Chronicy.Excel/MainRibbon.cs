using Chronicy.Excel.App;
using Chronicy.Excel.Information;
using Chronicy.Excel.UI;
using Chronicy.Excel.User;
using Chronicy.Excel.Utils;
using Chronicy.Information;
using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Ribbon;
using System;

namespace Chronicy.Excel
{
    public partial class MainRibbon
    {
        private MessageBoxContext informationContext = new MessageBoxContext();

        private CustomTaskPane taskPane;
        private NotebookTaskPane notebookTaskPaneForm;

        private IExcelExtension extension;

        public MainRibbon(IExcelExtension extension) : this()
        {
            this.extension = extension;
        }

        private void InitializeTaskPane()
        {
            notebookTaskPaneForm = new NotebookTaskPane();
            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(notebookTaskPaneForm, "Notebook");
        }

        private void OnRibbonLoad(object sender, RibbonUIEventArgs e)
        {
            InitializeTaskPane();

            extension.StateChanged += (enabled) => { enableButton.Checked = enabled; };
            extension.ConnectionChanged += (connected) => { connectButton.Visible = !connected; };

            extension.OnRibbonLoad();
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

        private void OnNewNotebookClicked(object sender, RibbonControlEventArgs e)
        {
            taskPane.Visible = true;
        }

        private void OnNewStackClicked(object sender, RibbonControlEventArgs e)
        {
            taskPane.Visible = true;
        }

        private void OnViewAllClicked(object sender, RibbonControlEventArgs e)
        {
            taskPane.Visible = true;
        }

        private void OnSyncClicked(object sender, RibbonControlEventArgs e)
        {
            try
            {
                extension.Sync();
            }
            catch (EndpointConnectionException ex)
            {
                InformationDispatcher.Default.Dispatch(ex.Message, informationContext, InformationKind.Error);
            }
        }

        private void OnTrackWorkbook(object sender, RibbonControlEventArgs e)
        {
            // 1. Submit the current workbook to ExcelTracker
            extension.Tracker.Track(Globals.ThisAddIn.Application.ThisWorkbook);
        }

        private void OnTrackSheet(object sender, RibbonControlEventArgs e)
        {
            // 1. Ask the user to pick a sheet
            SelectSheetAction action = new SelectSheetAction();
            action.ActionCompleted += (sheet) => { extension.Tracker.Track(sheet); /* 2. Submit the sheet to ExcelTracker */ };
            action.Run();
        }

        private void OnTrackCellRange(object sender, RibbonControlEventArgs e)
        {
            // 1. Ask the user to pick a range
            SelectCellRangeAction action = new SelectCellRangeAction();
            action.ActionCompleted += (range) => { extension.Tracker.Track(range); /* 2. Submit the range to ExcelTracker */ };
            action.Run();
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
