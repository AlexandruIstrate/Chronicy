using Chronicy.Standard.Data.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Chronicy.Website.Pages.Data
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private IActivityManager activityManager;

        [BindProperty]
        public List<ActivityItem> Items { get; set; }

        public HistoryModel(IActivityManager activityManager)
        {
            this.activityManager = activityManager;
        }

        public void OnGet()
        {
            Items = new List<ActivityItem>(activityManager.GetActivityItems());
        }
    }
}