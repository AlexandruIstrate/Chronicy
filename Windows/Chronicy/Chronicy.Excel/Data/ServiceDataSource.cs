using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Excel.Data
{
    public class ServiceDataSource : IDataSource<Notebook>
    {
        private IServerService service;

        public ServiceDataSource(IServerService service)
        {
            this.service = service;
        }

        public void Create(Notebook item)
        {
            service.Create(item);
        }

        public Task CreateAsync(Notebook item)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public Notebook Get(int id)
        {
            return service.Get(id);
        }

        public Task<Notebook> GetAsync(int id)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public IEnumerable<Notebook> GetAll()
        {
            return service.GetAll();
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public void Update(Notebook item)
        {
            service.Update(item);
        }

        public Task UpdateAsync(Notebook item)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        private const string NotSupportedMessage = "The service does not support async operations";
    }
}
