using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;

namespace Chronicy.Website.Charts
{
    public class BarChart : Chart<BarChartDataSet>
    {
        public override IHtmlContent GenerateHtml()
        {
            IHtmlContentBuilder contentBuilder = new HtmlContentBuilder();
            contentBuilder.AppendHtml("h1");
            contentBuilder.AppendHtml("h2");
            //contentBuilder.SetContent("This is the content");

            StringWriter writer = new StringWriter();
            contentBuilder.WriteTo(writer, HtmlEncoder.Default);

            return contentBuilder;
        }
    }

    public class BarChartDataSet : ChartDataSet
    {
        public IEnumerable<KeyValuePair<string, float>> LabelPoints { get; set; }

        public IEnumerable<float> DataPoints
        {
            get => LabelPoints.ToList().ConvertAll(item => item.Value);
            set => LabelPoints = value.ToList().ConvertAll(item => new KeyValuePair<string, float>(string.Empty, item));
        }
    }
}
