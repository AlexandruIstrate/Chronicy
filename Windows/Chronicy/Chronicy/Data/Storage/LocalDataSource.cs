using Chronicy.Sql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Add(item);
        }

        public Task CreateAsync(Notebook item)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Add(item);

            return Task.CompletedTask;
        }

        public void Delete(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Remove(Get(id));
        }

        public async Task DeleteAsync(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Remove(await GetAsync(id));
        }

        public Notebook Get(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(existing.Find((item) => item.SystemId == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public async Task<Notebook> GetAsync(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(await existing.FindAsync((item) => item.SystemId == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public IEnumerable<Notebook> GetAll()
        {
            return database.Context.Set<Notebook>();
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return GetAll();
            });
        }

        public void Update(Notebook item)
        {
            database.Context.SaveChanges();
        }

        public Task UpdateAsync(Notebook item)
        {
            return database.Context.SaveChangesAsync();
        }
    }
}
