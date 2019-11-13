using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Activity
{
    public class ActivityManager : IActivityManager
    {
        public List<ActivityItem> ActivityItems { get; set; }

        public ActivityManager()
        {
            ActivityItems = new List<ActivityItem>();
        }

        public void Create(ActivityItem item)
        {
            if (ActivityItems.Count < 1)
            {
                List<int> indices = ActivityItems.ConvertAll((iter) => iter.ID);
                indices.Sort();
                item.ID = indices.Last();
            }

            ActivityItems.Add(item);
        }

        public Task CreateAsync(ActivityItem item)
        {
            return Task.Run(() => Create(item));
        }

        public void Delete(int id)
        {
            ActivityItems.RemoveAll((item) => item.ID == id);
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        public ActivityItem Get(int id)
        {
            return ActivityItems.Find((item) => item.ID == id);
        }

        public Task<ActivityItem> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }

        public IEnumerable<ActivityItem> GetAll()
        {
            return ActivityItems;
        }

        public Task<IEnumerable<ActivityItem>> GetAllAsync()
        {
            return Task.FromResult(ActivityItems.AsEnumerable());
        }

        public void Update(ActivityItem item)
        {
            int index = ActivityItems.FindIndex((iter) => iter.ID == item.ID);
            ActivityItems[index] = item;
        }

        public Task UpdateAsync(ActivityItem item)
        {
            return Task.Run(() => Update(item));
        }
    }
}
