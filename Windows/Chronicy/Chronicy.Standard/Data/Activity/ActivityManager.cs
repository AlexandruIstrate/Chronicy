using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Activity
{
    public class ActivityManager : IActivityManager
    {
        public IEnumerable<ActivityItem> GetActivityItems()
        {
            return new List<ActivityItem>
            {
                new ActivityItem { Date = DateTime.Now, Component = "TestComponent", Message = "A test message" }
            };
        }

        public Task<IEnumerable<ActivityItem>> GetActivityItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
