using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public interface IDataSource<T>
    {
        T PullData();
        Task<T> PullDataAsync();

        void PushData(T data);
        Task PushDataAsync(T data);

        bool HasChanges();
        Task<bool> HasChangesAsync();
    }
}
