using Chronicy.Sql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class LocalDataSource : IDataSource<Notebook>, IDisposable
    {
        private SQLiteDatabase database;

        public LocalDataSource()
        {
            database = new SQLiteDatabase();
        }

        ~LocalDataSource()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // No-Op
            }

            database.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Create(Notebook item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Notebook item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Notebook Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Notebook> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notebook> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Notebook item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Notebook item)
        {
            throw new NotImplementedException();
        }
    }
}
