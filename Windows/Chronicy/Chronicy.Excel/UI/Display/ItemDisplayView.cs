using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Excel.UI.Display
{
    public class ItemDisplayView<T> : IDisplayView<T>
    {
        public List<T> Items { get; set; }

        public ItemDisplayView(List<T> items = null)
        {
            Items = items ?? new List<T>();
        }

        public IEnumerable<T> GetDisplayItems()
        {
            return Items;
        }

        public Task<IEnumerable<T>> GetDisplayItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetFilteredDisplayItems(Func<T, bool> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            List<T> results = new List<T>();

            foreach (T item in Items)
            {
                if (!filter(item))
                {
                    continue;
                }

                results.Add(item);
            }

            return results;
        }

        public Task<IEnumerable<T>> GetFilteredDisplayItemsAsync(Func<T, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
