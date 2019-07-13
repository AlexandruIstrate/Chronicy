using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Data
{
    [Authorize]
    public class ChartsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}