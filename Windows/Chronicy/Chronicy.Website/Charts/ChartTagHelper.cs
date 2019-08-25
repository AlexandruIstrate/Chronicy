using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Chronicy.Website.Charts
{
    public class ChartTagHelper : TagHelper
    {
        public ChartType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IChart chart = ChartFactory.Create(Type);
            output.Content.AppendHtml(chart.GenerateHtml());
        }
    }
}
