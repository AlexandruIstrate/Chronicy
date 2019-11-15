using Chronicy.Data.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly IActivityManager activityManager;

        [BindProperty]
        public List<ActivityItem> Items { get; set; }

        public HistoryModel(IActivityManager activityManager)
        {
            this.activityManager = activityManager;
        }

        public async Task OnGet()
        {
            Items = new List<ActivityItem>(await activityManager.GetAllAsync());
        }
    }
}