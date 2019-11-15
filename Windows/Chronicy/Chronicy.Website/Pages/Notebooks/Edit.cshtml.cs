using Chronicy.Data.Storage;
using Chronicy.Data.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data.Automation
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAutomationManager automationManager;

        [BindProperty]
        public AutomationAction EditedAction { get; set; }

        public EditModel(IAutomationManager automationManager)
        {
            this.automationManager = automationManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get Notebook from database
            EditedAction = await automationManager.GetAsync(id.Value);

            if (EditedAction == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Save Notebook to database
                await automationManager.UpdateAsync(EditedAction);
            }
            catch (DataSourceException)
            {
                if (!(await ActionExistsAsync(EditedAction.ID)))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ActionExistsAsync(int id)
        {
            return (await automationManager.GetAsync(id) != null);
        }
    }
}