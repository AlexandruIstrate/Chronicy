using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Web.Api
{
    public class NotebookApi : INotebook
    {
        private readonly IDataSource<Notebook> dataSource;

        public NotebookApi(IDataSource<Notebook> dataSource)
        {
            this.dataSource = dataSource;
        }

        public void Create(Notebook notebook)
        {
            dataSource.Create(notebook);
        }

        public Task CreateAsync(Notebook notebook)
        {
            return dataSource.CreateAsync(notebook);
        }

        public void Delete(string id)
        {
            dataSource.Delete(id);
        }

        public Task DeleteAsync(string id)
        {
            return dataSource.DeleteAsync(id);
        }

        public Notebook Get(string id)
        {
            return dataSource.Get(id);
        }

        public Task<Notebook> GetAsync(string id)
        {
            return dataSource.GetAsync(id);
        }

        public IEnumerable<Notebook> GetAll()
        {
            return dataSource.GetAll();
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            return dataSource.GetAllAsync();
        }

        public void Update(Notebook notebook)
        {
            dataSource.Update(notebook);
        }

        public Task UpdateAsync(Notebook notebook)
        {
            return dataSource.UpdateAsync(notebook);
        }
    }
}
