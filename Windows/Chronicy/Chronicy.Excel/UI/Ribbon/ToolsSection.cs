using Chronicy.Authentication;
using Chronicy.Excel.History;
using Chronicy.Excel.Properties;
using Chronicy.Excel.UI.Pane;
using Humanizer;
using Microsoft.Office.Tools.Ribbon;
using System.Collections.Generic;
using CategoryRecord = System.Collections.Generic.Dictionary<string, System.Collections.Generic.IList<Chronicy.Excel.History.HistoryItem>>;

namespace Chronicy.Excel.UI.Ribbon
{
    public class ToolsSection : Section
    {
        public MainRibbon Ribbon { get; set; }

        public HistoryManager History { get; set; }

        public CredentialsManager Credentials { get; set; }

        public ToolsSection(MainRibbon ribbon, HistoryManager history, CredentialsManager credentials)
        {
            Ribbon = ribbon;
            History = history;
            Credentials = credentials;

            InitializeCalls();
        }

        public void ShowHistory()
        {
            RibbonMenu historyMenu = Ribbon.historyMenu;
            historyMenu.Items.Clear();

            CategoryRecord record = History.GetItemsByCategory();

            foreach (string key in record.Keys)
            {
                RibbonButton categoryLabel = Ribbon.Factory.CreateRibbonButton();
                categoryLabel.Description = key;
                categoryLabel.Enabled = false;
                historyMenu.Items.Add(categoryLabel);

                IEnumerable<HistoryItem> items = record[key];

                foreach (HistoryItem item in items)
                {
                    RibbonButton historyItem = Ribbon.Factory.CreateRibbonButton();
                    historyItem.Image = Resources.IconHistoryItem32;
                    historyItem.Label = item.Title + " - " + item.Date.Humanize(utcDate: false);
                    historyItem.Description = item.Description;
                    historyMenu.Items.Add(historyItem);
                }
            }
        }

        public void Sync()
        {
            
        }

        public void Login()
        {
            LoginTaskPane control = new LoginTaskPane(Credentials);

            TaskPane<LoginTaskPane> taskPane = new TaskPane<LoginTaskPane>("Chronicy Login", control);
            taskPane.Visible = true;
        }

        private void InitializeCalls()
        {
            Actions[ActionIdentifiers.ShowHistory] = () => ShowHistory();
            Actions[ActionIdentifiers.Sync] = () => Sync();
            Actions[ActionIdentifiers.Login] = () => Login();
        }

        public static class ActionIdentifiers
        {
            public const string ShowHistory = "ShowHistory";
            public const string Sync = "Sync";
            public const string Login = "Login";
        }
    }
}
