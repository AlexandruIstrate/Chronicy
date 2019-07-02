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

        T Create(string name);
        Task<T> CreateAsync(string name);
        void Delete(T item);
        Task DeleteAsync(T item);

        void Update(T item);
        Task UpdateAsync(T item);
    }
}
