using Microsoft.AspNetCore.Html;

namespace Chronicy.Website.Charts
{
    public abstract class Chart
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public abstract IHtmlContent GenerateHtml();
    }
}
