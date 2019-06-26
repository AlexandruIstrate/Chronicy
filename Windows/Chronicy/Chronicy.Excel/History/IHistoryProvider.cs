using System;
using System.Threading.Tasks;

namespace Chronicy.Excel.History
{
    public interface IHistoryProvider
    {
        HistoryRecord Get();
        Task<HistoryRecord> GetAsync();

        HistoryRecord GetOlderThan(DateTime date);
        Task<HistoryRecord> GetOlderThanAsync(DateTime date);

        HistoryRecord GetNewerThan(DateTime date);
        Task<HistoryRecord> GetNewerThanAsync(DateTime date);
    }
}
