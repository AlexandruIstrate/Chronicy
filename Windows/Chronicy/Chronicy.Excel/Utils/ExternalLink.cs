using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Chronicy.Excel.Utils
{
    public class ExternalLink
    {
        public Uri Url { get; set; }

        public ExternalLink(Uri url)
        {
            Url = url;
        }

        public ExternalLink(string url)
        {
            Url = new Uri(url);
        }

        public void Open()
        {
            Process.Start(Url.ToString());
        }

        public void PromptOpen()
        {
            DialogResult result = MessageBox.Show($"Open { Url.ToString() }?", "Open External Link", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Open();
            }
        }
    }
}
