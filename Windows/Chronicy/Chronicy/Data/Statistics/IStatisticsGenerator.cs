using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Statistics
{
    public interface IStatisticsGenerator
    {
        IEnumerable<StatisticsItem> Generate();
        Task<IEnumerable<StatisticsItem>> GenerateAsync();
    }
}
