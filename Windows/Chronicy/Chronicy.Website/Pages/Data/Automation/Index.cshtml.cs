using Chronicy.Data.Automation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data.Automation
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAutomationManager automationManager;

        public List<AutomationAction> Items { get; set; }

        public IndexModel(IAutomationManager automationManager)
        {
            this.automationManager = automationManager;
        }

        public async Task OnGetAsync()
        {
            Items = new List<AutomationAction>(await automationManager.GetAllAsync());
        }
    }
}