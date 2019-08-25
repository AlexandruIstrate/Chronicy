using Microsoft.AspNetCore.Html;

namespace Chronicy.Website.Charts
{
    public abstract class Chart<T> : IChart where T : ChartDataSet
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public T DataSet { get; set; }

        public abstract IHtmlContent GenerateHtml();
    }
}
