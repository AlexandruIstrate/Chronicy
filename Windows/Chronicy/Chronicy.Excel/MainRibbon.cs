using Chronicy.Communication;
using Chronicy.Excel.App;
using Chronicy.Excel.Information;
using Chronicy.Information;
using Microsoft.Office.Tools.Ribbon;
using System;

namespace Chronicy.Excel
{
    public partial class MainRibbon : IInformationProvider
    {
        private MessageBoxContext messageBoxContext = new MessageBoxContext();
        public IInformationContext InformationContext => messageBoxContext;

        private IExtension extension;

        public MainRibbon(IExtension extension) : this()
        {
            this.extension = extension;
        }

        private void OnRibbonLoad(object sender, RibbonUIEventArgs e)
        {
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
                                                        InformationContext, 
                                                        InformationKind.Error);
            }
            catch (Exception ex)
            {
                InformationDispatcher.Default.Dispatch("An unknown error occurred while trying to connect to the service: " + ex.Message,
                                                        InformationContext);
            }
        }

        private void OnEnableToggled(object sender, RibbonControlEventArgs e)
        {
            extension.Enabled = enableButton.Checked;
        }

        private void OnNewNotebookClicked(object sender, RibbonControlEventArgs e)
        {

        }

        private void OnNewStackClicked(object sender, RibbonControlEventArgs e)
        {

        }

        private void OnViewAllClicked(object sender, RibbonControlEventArgs e)
        {

        }

        private void OnSyncClicked(object sender, RibbonControlEventArgs e)
        {
            try
            {
                extension.Sync();
            }
            catch (EndpointConnectionException ex)
            {
                InformationDispatcher.Default.Dispatch(ex.Message, InformationContext, InformationKind.Error);
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
            // 2. Submit the sheet to ExcelTracker
        }

        private void OnTrackCellRange(object sender, RibbonControlEventArgs e)
        {
            // 1. Ask the user to pick a range
            // 2. Submit the range to ExcelTracker
        }
    }
}
