using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public interface IDataSource<T>
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        void Create(T item);
        Task CreateAsync(T item);
        void Delete(int id);
        Task DeleteAsync(int id);

        void Update(T item);
        Task UpdateAsync(T item);
    }
}
