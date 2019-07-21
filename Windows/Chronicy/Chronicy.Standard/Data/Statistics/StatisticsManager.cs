using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Statistics
{
    public class StatisticsManager : IStatisticsManager
    {
        public List<StatisticsItem> StatisticsItems { get; set; }

        public StatisticsManager()
        {
            StatisticsItems = new List<StatisticsItem>();
        }

        public void Create(StatisticsItem item)
        {
            if (StatisticsItems.Count < 1)
            {
                List<int> indices = StatisticsItems.ConvertAll((iter) => iter.ID);
                indices.Sort();
                item.ID = indices.Last();
            }

            StatisticsItems.Add(item);
        }

        public Task CreateAsync(StatisticsItem item)
        {
            return Task.Run(() => Create(item));
        }

        public void Delete(int id)
        {
            StatisticsItems.RemoveAll((item) => item.ID == id);
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        public StatisticsItem Get(int id)
        {
            return StatisticsItems.Find((item) => item.ID == id);
        }

        public Task<StatisticsItem> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }

        public IEnumerable<StatisticsItem> GetAll()
        {
            return StatisticsItems;
        }

        public Task<IEnumerable<StatisticsItem>> GetAllAsync()
        {
            return Task.FromResult(StatisticsItems.AsEnumerable());
        }

        public void Update(StatisticsItem item)
        {
            int index = StatisticsItems.FindIndex((iter) => iter.ID == item.ID);
            StatisticsItems[index] = item;
        }

        public Task UpdateAsync(StatisticsItem item)
        {
            return Task.Run(() => Update(item));
        }
    }
}
