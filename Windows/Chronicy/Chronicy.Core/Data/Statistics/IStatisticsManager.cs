using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Statistics
{
    public interface IStatisticsManager
    {
        IEnumerable<StatisticsItem> GetItems();
        Task<IEnumerable<StatisticsItem>> GetItemsAsync();
    }
}
