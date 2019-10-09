using Microsoft.AspNetCore.Html;

namespace Chronicy.Website.Charts
{
    /// <summary>
    /// Represents a chart generated in HTML.
    /// </summary>
    public interface IChart
    {
        /// <summary>
        /// Generates the chart as HTML code.
        /// </summary>
        /// <returns>The generated chart content</returns>
        IHtmlContent GenerateHtml();
    }
}
