using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Data.Statistics
{
    public class StatisticsManager : IStatisticsManager
    {
        public IStatisticsGenerator StatisticsGenerator { get; set; }

        public StatisticsManager(/* IStatisticsGenerator statisticsGenerator */)
        {
            //StatisticsGenerator = statisticsGenerator;
        }

        public IEnumerable<StatisticsItem> GetItems()
        {
            return StatisticsGenerator.Generate();
        }

        public Task<IEnumerable<StatisticsItem>> GetItemsAsync()
        {
            return StatisticsGenerator.GenerateAsync();
        }
    }
}
