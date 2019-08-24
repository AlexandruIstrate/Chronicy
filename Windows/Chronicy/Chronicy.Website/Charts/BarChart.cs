using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace Chronicy.Website.Charts
{
    public class BarChart : Chart
    {
        public override IHtmlContent GenerateHtml()
        {
            IHtmlContentBuilder contentBuilder = new HtmlContentBuilder();
            contentBuilder.AppendHtml("h1");
            contentBuilder.AppendHtml("h2");
            //contentBuilder.SetContent("This is the content");

            StringWriter writer = new StringWriter();
            contentBuilder.WriteTo(writer, HtmlEncoder.Default);

            string str = writer.ToString();

            return contentBuilder;
        }
    }
}
