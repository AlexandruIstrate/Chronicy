using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Chronicy.Excel.App;
using Microsoft.Office.Tools.Ribbon;

namespace Chronicy.Excel
{
    public partial class ThisAddIn
    {
        // TODO: Better initialization!
        // TODO 2: Single instance of this object per application
        private IExtension extension = new AppExtension();

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
