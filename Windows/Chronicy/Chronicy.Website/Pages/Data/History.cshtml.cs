using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Data
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}