using System;
using Chronicy.Excel.App;
using Microsoft.Office.Tools.Ribbon;
using Chronicy.Information;

namespace Chronicy.Excel
{
    public partial class ThisAddIn
    {
        // TODO: Better initialization!
        // TODO 2: Single instance of this object per application
        private IExcelExtension extension = new AppExtension();

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            try
            {
                extension.OnStart();
                extension.Connect();
            }
            catch (EndpointConnectionException)
            {
                // If we can't connect when we initialize the extension, just ignore for now and let the user try to connect
                // We should change this in the future to use some kind of passive, start-up display system
                // However, for now, we just log the failure

                InformationDispatcher.Default.Dispatch("Could not auto-connect to service!", DebugLogContext.Default, InformationKind.Error);
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            extension.OnShutdown();
        }

        protected override IRibbonExtension[] CreateRibbonObjects()
        {
            return new[] { new MainRibbon(extension) };
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
