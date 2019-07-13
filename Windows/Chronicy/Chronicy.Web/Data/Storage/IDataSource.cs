using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public interface IDataSource<T>
    {
        T Get(string id);
        Task<T> GetAsync(string id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        void Create(T item);
        Task CreateAsync(T item);
        void Delete(string id);
        Task DeleteAsync(string id);

        void Update(T item);
        Task UpdateAsync(T item);
    }
}
