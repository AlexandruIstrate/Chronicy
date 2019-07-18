using Chronicy.Authentication;
using System;
using System.Windows.Forms;

namespace Chronicy.Excel.UI
{
    public partial class LoginTaskPane : UserControl
    {
        public ICredentialsManager CredentialsManager { get; set; }

        public LoginTaskPane(ICredentialsManager credentialsManager)
        {
            CredentialsManager = credentialsManager;
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            CredentialsManager.Signin(usernameTextBox.Text, passwordtextBox.Text);
        }

        private void OnCancel(object sender, EventArgs e)
        {

        }
    }
}
