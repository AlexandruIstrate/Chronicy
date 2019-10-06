using Chronicy.Standard.Data.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Data
{
    //[Authorize]
    public class StatisticsModel : PageModel
    {
        private readonly IStatisticsManager statisticsManager;

        public List<StatisticsItem> Items { get; set; }

        public StatisticsModel(IStatisticsManager statisticsManager)
        {
            this.statisticsManager = statisticsManager;
        }

        public Task OnGetAsync()
        {
            //Items = new List<StatisticsItem>(await statisticsManager.GetAllAsync());
            return Task.CompletedTask;
        }
    }
}