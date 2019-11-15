using System.Threading.Tasks;
using Chronicy.Data.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Data.Automation
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IAutomationManager automationManager;

        public AutomationAction EditedAction { get; set; }

        public DetailsModel(IAutomationManager automationManager)
        {
            this.automationManager = automationManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedAction = await automationManager.GetAsync(id.Value);

            return Page();
        }
    }
}