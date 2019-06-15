using Chronicy.Excel.App;
using Chronicy.Excel.Information;
using Chronicy.Information;
using Microsoft.Office.Tools.Ribbon;
using System;

namespace Chronicy.Excel
{
    public partial class MainRibbon
    {
        private IExtension extension;

        public MainRibbon(IExtension extension) : this()
        {
            this.extension = extension;
        }

        private void OnRibbonLoad(object sender, RibbonUIEventArgs e)
        {
            extension.StateChanged += ((enabled) => { enableButton.Checked = enabled; });
            extension.ConnectionChanged += ((connected) => { connectButton.Visible = !connected; });

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
                                                        new MessageBoxContext(), 
                                                        InformationKind.Error);
            }
            catch (Exception ex)
            {
                InformationDispatcher.Default.Dispatch("An unknown error occurred while trying to connect to the service: " + ex.Message,
                                                        new MessageBoxContext());
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
            extension.Sync();
        }
    }
}
