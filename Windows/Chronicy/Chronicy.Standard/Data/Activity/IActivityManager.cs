using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Activity
{
    public interface IActivityManager
    {
        IEnumerable<ActivityItem> GetActivityItems();
        Task<IEnumerable<ActivityItem>> GetActivityItemsAsync();
    }
}
