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

        Notebook Get(string id);
        Task<Notebook> GetAsync(string id);

        void Create(Notebook notebook);
        Task CreateAsync(Notebook notebook);

        void Update(Notebook notebook);
        Task UpdateAsync(Notebook notebook);

        void Delete(string id);
        Task DeleteAsync(string id);
    }
}
