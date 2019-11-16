using Chronicy.Data.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Excel.UI.Display
{
    public class DataDisplayView<T> : IDisplayView<T>
    {
        public IDataSource<T> DataSource { get; set; }

        public DataDisplayView(IDataSource<T> dataSource)
        {
            DataSource = dataSource;
        }

        public IEnumerable<T> GetDisplayItems()
        {
            return DataSource.GetAll();
        }

        public Task<IEnumerable<T>> GetDisplayItemsAsync()
        {
            return DataSource.GetAllAsync();
        }

        public IEnumerable<T> GetFilteredDisplayItems(Func<T, bool> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            List<T> results = new List<T>();

            foreach (T item in DataSource.GetAll())
            {
                if (!filter(item))
                {
                    continue;
                }

                results.Add(item);
            }

            return results;
        }

        public async Task<IEnumerable<T>> GetFilteredDisplayItemsAsync(Func<T, bool> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            List<T> results = new List<T>();

            foreach (T item in await DataSource.GetAllAsync())
            {
                if (!filter(item))
                {
                    continue;
                }

                results.Add(item);
            }

            return results;
        }
    }
}
