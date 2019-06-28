using System.Collections.Generic;

namespace Chronicy.Excel.History
{
    public class HistoryRecord
    {
        public string Name { get; set; }
        public List<HistoryItem> Items { get; set; }

        public HistoryRecord(string name)
        {
            Name = name;
            Items = new List<HistoryItem>();
        }

        public void Add(HistoryItem historyItem)
        {
            Items.Add(historyItem);
        }
    }
}
