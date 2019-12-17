using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Statistics
{
    public interface IStatisticsManager
    {
        IEnumerable<StatisticsItem> GetItems();
        Task<IEnumerable<StatisticsItem>> GetItemsAsync();
    }
}
