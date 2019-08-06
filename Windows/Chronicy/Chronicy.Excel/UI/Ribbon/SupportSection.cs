using Chronicy.Excel.Properties;
using Chronicy.Excel.Utils;

namespace Chronicy.Excel.UI.Ribbon
{
    public class SupportSection : Section
    {
        public SupportSection()
        {
            InitializeCalls();
        }
        
        public void OpenHelp()
        {
            OpenLink(Resources.LINK_HELP);
        }

        public void ReportBug()
        {
            OpenLink(Resources.LINK_SUBMIT_BUG);
        }

        public void OpenProjectPage()
        {
            OpenLink(Resources.LINK_PROJECT_PAGE);
        }

        private void OpenLink(string url)
        {
            ExternalLink link = new ExternalLink(url);
            link.Open();
        }

        private void InitializeCalls()
        {
            Actions[ActionIdentifiers.OpenHelp] = () => OpenHelp();
            Actions[ActionIdentifiers.ReportBug] = () => ReportBug();
            Actions[ActionIdentifiers.OpenProjectPage] = () => OpenProjectPage();
        }

        public static class ActionIdentifiers
        {
            public const string OpenHelp = "OpenHelp";
            public const string ReportBug = "ReportBug";
            public const string OpenProjectPage = "OpenProjectPage";
        }
    }
}
