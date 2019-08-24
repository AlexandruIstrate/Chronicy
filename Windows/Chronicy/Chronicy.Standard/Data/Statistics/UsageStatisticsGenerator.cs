using Chronicy.Data;
using Chronicy.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Statistics
{
    public class UsageStatisticsGenerator : IStatisticsGenerator
    {
        public IDataSource<Notebook> DataSource { get; set; }

        public UsageStatisticsGenerator(IDataSource<Notebook> dataSource)
        {
            DataSource = dataSource;
        }

        public IEnumerable<StatisticsItem> Generate()
        {
            List<StatisticsItem> items = new List<StatisticsItem>();
            Dictionary<DateTime, List<Card>> sortedCards = SortCardsByDay(GetAllCards());

            foreach (var pair in sortedCards)
            {
                items.Add(new StatisticsItem
                {
                    Name = pair.Key.ToString(),
                    Value = pair.Value.Count.ToString()
                });
            }

            return items;
        }

        public async Task<IEnumerable<StatisticsItem>> GenerateAsync()
        {
            List<StatisticsItem> items = new List<StatisticsItem>();
            Dictionary<DateTime, List<Card>> sortedCards = SortCardsByDay(await GetAllCardsAsync());

            foreach (var pair in sortedCards)
            {
                items.Add(new StatisticsItem
                {
                    Name = pair.Key.ToString(),
                    Value = pair.Value.Count.ToString()
                });
            }

            return items;
        }

        private List<Card> GetAllCards()
        {
            List<Card> cards = new List<Card>();

            foreach (Notebook notebook in DataSource.GetAll())
            {
                foreach (Stack stack in notebook.Stacks)
                {
                    foreach (Card card in stack.Cards)
                    {
                        cards.Add(card);
                    }
                } 
            }

            return cards;
        }

        private async Task<List<Card>> GetAllCardsAsync()
        {
            List<Card> cards = new List<Card>();

            foreach (Notebook notebook in await DataSource.GetAllAsync())
            {
                foreach (Stack stack in notebook.Stacks)
                {
                    foreach (Card card in stack.Cards)
                    {
                        cards.Add(card);
                    }
                } 
            }

            return cards;
        }

        private Dictionary<DateTime, List<Card>> SortCardsByDay(IEnumerable<Card> cards)
        {
            List<Card> cardList = cards.ToList().ConvertAll(item =>
            {
                item.Date = item.Date.Date;
                return item;
            });

            Dictionary<DateTime, List<Card>> result = new Dictionary<DateTime, List<Card>>();

            foreach (Card card in cardList)
            {
                // Try to get an existing list
                if (!result.TryGetValue(card.Date, out List<Card> existingList))
                {
                    // If the list doesn't exist then create a new one and set it in the key
                    existingList = new List<Card>();
                    result[card.Date] = existingList;
                }

                existingList.Add(card);
            }

            return result;
        }
    }
}
