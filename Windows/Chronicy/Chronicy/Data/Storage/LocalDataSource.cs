using Chronicy.Information;
using Chronicy.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class LocalDataSource : IDataSource<Notebook>, IDisposable
    {
        private SqliteDatabase database;

        public LocalDataSource()
        {
            database = new SqliteDatabase();
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

            Save();
        }

        public async Task CreateAsync(Notebook item)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Add(item);
            
            await SaveAsync();
        }

        public void Delete(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Remove(Get(id));

            Save();
        }

        public async Task DeleteAsync(string id)
        {
            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            existing.Remove(await GetAsync(id));

            await SaveAsync();
        }

        public Notebook Get(string id)
        {
            // Make sure any pending changes are saved
            Save();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(existing.Find((item) => item.Uuid == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public async Task<Notebook> GetAsync(string id)
        {
            // Make sure any pending changes are saved
            await SaveAsync();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(await existing.FindAsync((item) => item.Uuid == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public IEnumerable<Notebook> GetAll()
        {
            // Make sure any pending changes are saved
            Save();

            DbSet<Notebook> notebooks = database.Context.Set<Notebook>();

            foreach (Notebook notebook in notebooks)
            {
                database.Context.Entry(notebook).Collection(n => n.Stacks).Load();
            }

            return notebooks;
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync()
        {
            // Make sure any pending changes are saved
            await SaveAsync();
            return GetAll();
        }

        public void Update(Notebook item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;
                Notebook existing = context.Notebooks.Find(item.ID);

                foreach (Stack stack in item.Stacks)
                {
                    existing.Stacks.Add(stack);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);
            }
        }

        public async Task UpdateAsync(Notebook item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;
                Notebook existing = await context.Notebooks.FindAsync(item.ID);

                foreach (Stack stack in item.Stacks)
                {
                    existing.Stacks.Add(stack);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);
            }
        }

        public void Save()
        {
            database.Context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return database.Context.SaveChangesAsync();
        }
    }
}
