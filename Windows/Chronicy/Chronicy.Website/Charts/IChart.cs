using Microsoft.AspNetCore.Html;

namespace Chronicy.Website.Charts
{
    public interface IChart
    {
        IHtmlContent GenerateHtml();
    }
}
