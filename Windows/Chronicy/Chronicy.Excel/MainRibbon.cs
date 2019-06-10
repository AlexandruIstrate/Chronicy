using Chronicy.Excel.App;
using Microsoft.Office.Tools.Ribbon;

namespace Chronicy.Excel
{
    public partial class MainRibbon
    {
        private IExtension extension;

        public MainRibbon(IExtension extension) : this()
        {
            this.extension = extension;
        }

        private void MainRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            extension.OnRibbonLoad();
        }

        private void OnRefreshClicked(object sender, RibbonControlEventArgs e)
        {

        }

        private void OnEnableToggled(object sender, RibbonControlEventArgs e)
        {
            extension.Enabled = enableButton.Checked;
        }
    }
}
