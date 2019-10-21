using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Excel.Data
{
    /// <summary>
    /// Represents a service data source.
    /// </summary>
    public class ServiceDataSource : IDataSource<Notebook>
    {
        private readonly IServerService service;

        public ServiceDataSource(IServerService service)
        {
            this.service = service;
        }

        public void Create(Notebook item)
        {
            EnsureNoErrors(service.Create(item));
        }

        public Task CreateAsync(Notebook item)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public void Delete(int id)
        {
            EnsureNoErrors(service.Delete(id));
        }

        public Task DeleteAsync(int id)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public Notebook Get(int id)
        {
            return EnsureNoErrors(service.Get(id));
        }

        public Task<Notebook> GetAsync(int id)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public IEnumerable<Notebook> GetAll()
        {
            return EnsureNoErrors(service.GetAll());
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        public void Update(Notebook item)
        {
            EnsureNoErrors(service.Update(item));
        }

        public Task UpdateAsync(Notebook item)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        private void EnsureNoErrors(DataResult result)
        {
            if (result.HasError)
            {
                throw new DataSourceException(result.ErrorMessage);
            }
        }

        private T EnsureNoErrors<T>(DataResult<T> result)
        {
            if (result.HasError)
            {
                throw new DataSourceException(result.ErrorMessage);
            }

            return result.Value;
        }

        private const string NotSupportedMessage = "The service does not support async operations";
    }
}
