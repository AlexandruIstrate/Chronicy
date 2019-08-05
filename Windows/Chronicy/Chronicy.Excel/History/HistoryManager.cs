using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryRecord = System.Collections.Generic.Dictionary<string, System.Collections.Generic.IList<Chronicy.Excel.History.HistoryItem>>;

namespace Chronicy.Excel.History
{
    public class HistoryManager
    {
        public List<IHistoryProvider> Providers { get; }

        public HistoryManager()
        {
            Providers = new List<IHistoryProvider>();
        }

        public HistoryRecord GetCombinedRecords()
        {
            HistoryRecord record = new HistoryRecord("Combined");

            foreach (IHistoryProvider provider in Providers)
            {
                record.Items.AddRange(provider.Get().Items);
            }

            return record;
        }

        public async Task<HistoryRecord> GetCombinedRecordsAsync()
        {
            HistoryRecord record = new HistoryRecord("Combined");

            foreach (IHistoryProvider provider in Providers)
            {
                record.Items.AddRange((await provider.GetAsync()).Items);
            }

            return record;
        }

        public CategoryRecord GetItemsByCategory()
        {
            CategoryRecord result = new CategoryRecord();

            foreach (IHistoryProvider provider in Providers)
            {
                HistoryRecord record = provider.Get();

                List<HistoryItem> items = record.Items;
                items.Sort(new HistoryItemComparer());

                foreach (HistoryItem historyItem in items)
                {
                    try
                    {
                        IList<HistoryItem> list = result[historyItem.Category];
                        list.Add(historyItem);
                    }
                    catch (KeyNotFoundException)
                    {
                        // If we don't have a list for this key, then create one and add the item to it
                        IList<HistoryItem> list = new List<HistoryItem>();
                        list.Add(historyItem);

                        result[historyItem.Category] = list;
                    }
                }
            }

            return result;
        }

        public async Task<CategoryRecord> GetItemsByCategoryAsync()
        {
            CategoryRecord result = new CategoryRecord();

            foreach (IHistoryProvider provider in Providers)
            {
                HistoryRecord record = await provider.GetAsync();

                List<HistoryItem> items = record.Items;
                items.Sort(new HistoryItemComparer());

                foreach (HistoryItem historyItem in items)
                {
                    try
                    {
                        IList<HistoryItem> list = result[historyItem.Category];
                        list.Add(historyItem);
                    }
                    catch (KeyNotFoundException)
                    {
                        // If we don't have a list for this key, then create one and add the item to it
                        IList<HistoryItem> list = new List<HistoryItem>();
                        list.Add(historyItem);

                        result[historyItem.Category] = list;
                    }
                }
            }

            return result;
        }

        private class HistoryItemComparer : IComparer<HistoryItem>
        {
            public int Compare(HistoryItem x, HistoryItem y)
            {
                if (x.Date > y.Date)
                {
                    return 1;
                }

                if (x.Date < y.Date)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
