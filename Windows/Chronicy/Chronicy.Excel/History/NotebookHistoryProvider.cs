using Chronicy.Data;
using Chronicy.Data.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Excel.History
{
    public class NotebookHistoryProvider : IHistoryProvider
    {
        public IDataSource<Notebook> DataSource { get; set; }

        public NotebookHistoryProvider(IDataSource<Notebook> dataSource)
        {
            DataSource = dataSource;
        }

        public HistoryRecord Get()
        {
            return GetFiltered();
        }

        public Task<HistoryRecord> GetAsync()
        {
            return GetFilteredAsync();
        }

        public HistoryRecord GetNewerThan(DateTime date)
        {
            return GetFiltered((compareTo) => { return compareTo > date; });
        }

        public Task<HistoryRecord> GetNewerThanAsync(DateTime date)
        {
            return GetFilteredAsync((compareTo) => { return compareTo > date; });
        }

        public HistoryRecord GetOlderThan(DateTime date)
        {
            return GetFiltered((compareTo) => { return compareTo < date; });
        }

        public Task<HistoryRecord> GetOlderThanAsync(DateTime date)
        {
            return GetFilteredAsync((compareTo) => { return compareTo < date; });
        }

        private HistoryRecord GetFiltered(Func<DateTime, bool> filter = null)
        {
            List<Notebook> notebooks = new List<Notebook>(DataSource.GetAll());

            List<HistoryItem> historyItems = new List<HistoryItem>();

            foreach (Notebook notebook in notebooks)
            {
                foreach (Stack stack in notebook.Stacks)
                {
                    foreach (Card card in stack.Cards)
                    {
                        // If we haven't got a filter then we can just return all of the values
                        if (filter != null)
                        {
                            // If the filter doesn't want this value we go to the next one in the list
                            if (!filter(card.Date))
                            {
                                continue;
                            }
                        }

                        historyItems.Add(new HistoryItem
                        {
                            Title = "New Card Added",
                            Description = $"Card \"{ card.Name }\" was added to the stack \"{ stack.Name }\"",
                            Category = notebook.Name,
                            Date = card.Date
                        });
                    }
                }
            }

            return new HistoryRecord("Card History")
            {
                Items = historyItems
            };
        }

        private async Task<HistoryRecord> GetFilteredAsync(Func<DateTime, bool> filter = null)
        {
            List<Notebook> notebooks = new List<Notebook>(await DataSource.GetAllAsync());

            List<HistoryItem> historyItems = new List<HistoryItem>();

            foreach (Notebook notebook in notebooks)
            {
                foreach (Stack stack in notebook.Stacks)
                {
                    foreach (Card card in stack.Cards)
                    {
                        // If we haven't got a filter then we can just return all of the values
                        if (filter != null)
                        {
                            // If the filter doesn't want this value we go to the next one in the list
                            if (!filter(card.Date))
                            {
                                continue;
                            }
                        }

                        historyItems.Add(new HistoryItem
                        {
                            Title = "New Card Added",
                            Description = $"A new card was added in the stack { stack.Name }",
                            Category = notebook.Name,
                            Date = card.Date
                        });
                    }
                }
            }

            return new HistoryRecord("Card History")
            {
                Items = historyItems
            };
        }
    }
}
