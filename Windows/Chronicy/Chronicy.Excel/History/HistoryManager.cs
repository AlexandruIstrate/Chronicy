using System.Collections.Generic;

namespace Chronicy.Excel.History
{
    public class HistoryManager
    {
        public List<IHistoryProvider> Providers { get; }

        public HistoryManager()
        {
            Providers = new List<IHistoryProvider>();
        }

        public void Register(IHistoryProvider provider)
        {
            Providers.Add(provider);
        }

        public void Deregister(IHistoryProvider provider)
        {
            Providers.Remove(provider);
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

        //public Task<HistoryRecord> GetCombinedRecordsAsync()
        //{

        //}
    }
}
