using Chronicy.Standard.Data.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data.Automation
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IAutomationManager automationManager;

        [BindProperty]
        public AutomationAction EditedAction { get; set; }

        public CreateModel(IAutomationManager automationManager)
        {
            this.automationManager = automationManager;
        }

        public IActionResult OnGet()
        {
            EditedAction = new AutomationAction();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await automationManager.CreateAsync(EditedAction);

            return RedirectToPage("./Index");
        }
    }
}