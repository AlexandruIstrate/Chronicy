using Chronicy.Data.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Data
{
    [Authorize]
    public class ActionsModel : PageModel
    {
        public ActionManager Manager { get; set; }

        public void OnGet()
        {

        }
    }
}