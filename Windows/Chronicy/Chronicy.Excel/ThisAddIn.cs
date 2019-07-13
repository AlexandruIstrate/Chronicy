using System;
using Chronicy.Excel.App;
using Microsoft.Office.Tools.Ribbon;

namespace Chronicy.Excel
{
    public partial class ThisAddIn
    {
        private readonly IExcelExtension extension = new AppExtension();

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            extension.OnStart();
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
