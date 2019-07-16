using Chronicy.Information;
using Chronicy.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class LocalDataSource : IDataSource<Notebook>, IDisposable
    {
        private readonly SqliteDatabase database;

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
                // Clean up unmanaged resources
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
            try
            {
                DbSet<Notebook> existing = database.Context.Set<Notebook>();
                existing.Add(item);

                Save();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public async Task CreateAsync(Notebook item)
        {
            try
            {
                DbSet<Notebook> existing = database.Context.Set<Notebook>();
                existing.Add(item);

                await SaveAsync();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                DbSet<Notebook> existing = database.Context.Set<Notebook>();
                existing.Remove(Get(id));

                Save();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                DbSet<Notebook> existing = database.Context.Set<Notebook>();
                existing.Remove(await GetAsync(id));

                await SaveAsync();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public Notebook Get(int id)
        {
            // Make sure any pending changes are saved
            Save();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(existing.Find((item) => item.ID == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public async Task<Notebook> GetAsync(int id)
        {
            // Make sure any pending changes are saved
            await SaveAsync();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = new List<Notebook>(await existing.FindAsync((item) => item.ID == id));

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public IEnumerable<Notebook> GetAll()
        {
            try
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
            catch (Exception e)
            {
                throw new DataSourceException("Could not get notebooks", e);
            }
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync()
        {
            try
            {
                // Make sure any pending changes are saved
                await SaveAsync();
                return GetAll();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get notebooks", e);
            }
        }

        public void Update(Notebook item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;
                Notebook existing = context.Notebooks.Find(item.ID);

                foreach (Stack stack in item.Stacks)
                {
                    bool exists = existing.Stacks.Exists((iter) => iter.Name == stack.Name);

                    if (exists)
                    {
                        continue;
                    }

                    existing.Stacks.Add(stack);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);  // TODO: Remove
                throw new DataSourceException("Could not update notebook", e);
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
                    bool exists = existing.Stacks.Exists((iter) => iter.Name == stack.Name);

                    if (exists)
                    {
                        continue;
                    }

                    existing.Stacks.Add(stack);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e); // TODO: Remove
                throw new DataSourceException("Could not update notebook", e);
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
