using Chronicy.Data.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data.Automation
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAutomationManager automationManager;

        [BindProperty]
        public AutomationAction EditedAction { get; set; }

        public DeleteModel(IAutomationManager automationManager)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedAction = await automationManager.GetAsync(id.Value);

            if (EditedAction == null)
            {
                return NotFound();
            }

            await automationManager.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}