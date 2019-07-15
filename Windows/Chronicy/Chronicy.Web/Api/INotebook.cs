using Chronicy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Web.Api
{
    public interface INotebook
    {
        IEnumerable<Notebook> GetAll();
        Task<IEnumerable<Notebook>> GetAllAsync();

        Notebook Get(int id);
        Task<Notebook> GetAsync(int id);

        void Create(Notebook notebook);
        Task CreateAsync(Notebook notebook);

        void Update(Notebook notebook);
        Task UpdateAsync(Notebook notebook);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
