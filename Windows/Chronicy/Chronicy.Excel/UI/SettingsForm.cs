using Chronicy.Excel.Properties;
using Chronicy.Utils;
using System;
using System.Text;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            // General tab
            showRibbonBrandingCheckBox.Checked = Settings.Default.ShowRibbonBranding;

            // Services tab
            serverAddressTextBox.Text = Settings.Default.WebServiceAddress;
            autoConnectCheckBox.Checked = Settings.Default.AutoConnect;

            usernameTextBox.Text = ProtectedDataStorage.Unprotect(Settings.Default.EncryptedUsername);
            passwordTextBox.Text = ProtectedDataStorage.Unprotect(Settings.Default.EncryptedPassword);
        }

        private void SaveSettings()
        {
            // General tab
            Settings.Default.ShowRibbonBranding = showRibbonBrandingCheckBox.Checked;

            // Services tab
            Settings.Default.WebServiceAddress = serverAddressTextBox.Text;
            Settings.Default.AutoConnect = autoConnectCheckBox.Checked;

            Settings.Default.EncryptedUsername = ProtectedDataStorage.Protect(usernameTextBox.Text);
            Settings.Default.EncryptedPassword = ProtectedDataStorage.Protect(passwordTextBox.Text);

            // Save settings
            Settings.Default.Save();
        }

        private void OnRestoreDefaultClicked(object sender, EventArgs e)
        {
            Settings.Default.WebServiceAddress = Settings.Default.DefaultWebServiceAddress;
            serverAddressTextBox.Text = Settings.Default.WebServiceAddress;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void OnAutoConnectChanged(object sender, EventArgs e)
        {
            usernameTextBox.Enabled = autoConnectCheckBox.Checked;
            passwordTextBox.Enabled = autoConnectCheckBox.Checked;
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OnApplyClicked(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
