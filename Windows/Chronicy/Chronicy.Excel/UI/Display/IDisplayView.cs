using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Excel.UI.Display
{
    public interface IDisplayView<T>
    {
        IEnumerable<T> GetDisplayItems();
        Task<IEnumerable<T>> GetDisplayItemsAsync();

        IEnumerable<T> GetFilteredDisplayItems(Func<T, bool> filter);
        Task<IEnumerable<T>> GetFilteredDisplayItemsAsync(Func<T, bool> filter);
    }
}
