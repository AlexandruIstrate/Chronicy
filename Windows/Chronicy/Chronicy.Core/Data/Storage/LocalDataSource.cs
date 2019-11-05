using Chronicy.Information;
using Chronicy.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                EnsureItemDoesNotExist(existing, item);

                existing.Add(item);

                database.Context.SaveChanges();
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
                await EnsureItemDoesNotExistAsync(existing, item);

                existing.Add(item);

                await database.Context.SaveChangesAsync();
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

                database.Context.SaveChanges();
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

                await database.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public Notebook Get(int id)
        {
            // Make sure any pending changes are saved
            database.Context.SaveChanges();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = existing.Find((item) => item.ID == id).ToList();

            if (notebooks.Count < 1)
            {
                throw new DataSourceException("The object with the given id does not exist");
            }

            return notebooks[0];
        }

        public async Task<Notebook> GetAsync(int id)
        {
            // Make sure any pending changes are saved
            await database.Context.SaveChangesAsync();

            DbSet<Notebook> existing = database.Context.Set<Notebook>();
            List<Notebook> notebooks = (await existing.FindAsync((item) => item.ID == id)).ToList();

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
                database.Context.SaveChanges();

                DbSet<Notebook> notebooks = database.Context.Notebooks;

                foreach (Notebook notebook in notebooks)
                {
                    database.Context.Entry(notebook).Collection(n => n.Stacks).Load();

                    foreach (Stack stack in notebook.Stacks)
                    {
                        database.Context.Entry(stack).Collection(c => c.Cards).Load();
                        database.Context.Entry(stack).Collection(s => s.Fields).Load();
                    }
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
                await database.Context.SaveChangesAsync();
                return GetAll();
            }
            catch (Exception e)
            {
                throw new DataSourceException("Could not get notebooks", e);
            }
        }

        // TODO: Clean this up and use an algorithm that is scalable for child items
        public void Update(Notebook item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;
                context.SaveChanges();

                Notebook existingNotebook = context.Notebooks
                    .Where(n => n.ID == item.ID)
                    .Include(n => n.Stacks)
                    .SingleOrDefault();

                if (existingNotebook == null)
                {
                    throw new Exception($"The notebook with ID { item.ID } could not be found");
                }

                context.Entry(existingNotebook).CurrentValues.SetValues(item);

                // Delete children
                foreach (Stack existingStack in existingNotebook.Stacks)
                {
                    if (!item.Stacks.Any(s => s.ID == existingStack.ID))
                    {
                        context.Stacks.Remove(existingStack);
                    }
                }

                // Update and insert children
                foreach (Stack stack in item.Stacks)
                {
                    Stack existingStack = existingNotebook.Stacks
                        .Where(s => s.ID == stack.ID && s.ID != default)
                        .SingleOrDefault();

                    if (existingStack == null)
                    {
                        // Insert child
                        existingNotebook.Stacks.Add(stack);
                    }
                    else
                    {
                        // Update child
                        context.Entry(existingStack).CurrentValues.SetValues(stack);
                    }

                    UpdateStack(stack);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);  // TODO: Remove
                throw new DataSourceException("Could not update notebook", e);
            }
        }

        // TODO: Clean this up and use an algorithm that is scalable for child items
        public async Task UpdateAsync(Notebook item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;
                context.SaveChanges();

                Notebook existingNotebook = context.Notebooks
                    .Where(n => n.ID == item.ID)
                    .Include(n => n.Stacks)
                    .SingleOrDefault();

                if (existingNotebook == null)
                {
                    throw new Exception($"The notebook with ID { item.ID } could not be found");
                }

                context.Entry(existingNotebook).CurrentValues.SetValues(item);

                // Delete children
                foreach (Stack existingStack in existingNotebook.Stacks)
                {
                    if (!item.Stacks.Any(s => s.ID == existingStack.ID))
                    {
                        context.Stacks.Remove(existingStack);
                    }
                }

                // Update and insert children
                foreach (Stack stack in item.Stacks)
                {
                    Stack existingStack = existingNotebook.Stacks
                        .Where(s => s.ID == stack.ID && s.ID != default)
                        .SingleOrDefault();

                    if (existingStack == null)
                    {
                        // Insert child
                        existingNotebook.Stacks.Add(stack);
                    }
                    else
                    {
                        // Update child
                        context.Entry(existingStack).CurrentValues.SetValues(stack);
                    }

                    await UpdateStackAsync(stack);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);  // TODO: Remove
                throw new DataSourceException("Could not update notebook", e);
            }
        }

        private void UpdateStack(Stack item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;

                Stack existingStack = context.Stacks
                    .Where(s => s.ID == item.ID)
                    .Include(s => s.Fields)
                    .SingleOrDefault();

                if (existingStack == null)
                {
                    return;
                }

                context.Entry(existingStack).CurrentValues.SetValues(item);

                // Delete children
                foreach (CustomField existingField in existingStack.Fields)
                {
                    if (!item.Fields.Any(f => f.ID == existingField.ID))
                    {
                        context.Fields.Remove(existingField);
                    }
                }

                // Update and insert children
                foreach (CustomField field in item.Fields)
                {
                    CustomField existingField = existingStack.Fields
                        .Where(f => f.ID == field.ID && f.ID != default)
                        .SingleOrDefault();

                    if (existingField == null)
                    {
                        // Insert child
                        existingStack.Fields.Add(field);
                    }
                    else
                    {
                        // Update child
                        context.Entry(existingField).CurrentValues.SetValues(field);
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);  // TODO: Remove
                throw new DataSourceException("Could not update stack", e);
            }
        }

        private async Task UpdateStackAsync(Stack item)
        {
            try
            {
                SQLiteDatabaseContext context = database.Context;

                Stack existingStack = context.Stacks
                    .Where(s => s.ID == item.ID)
                    .Include(s => s.Fields)
                    .SingleOrDefault();

                if (existingStack == null)
                {
                    return;
                }

                context.Entry(existingStack).CurrentValues.SetValues(item);

                // Delete children
                foreach (CustomField existingField in existingStack.Fields)
                {
                    if (!item.Fields.Any(f => f.ID == existingField.ID))
                    {
                        context.Fields.Remove(existingField);
                    }
                }

                // Update and insert children
                foreach (CustomField field in item.Fields)
                {
                    CustomField existingField = existingStack.Fields
                        .Where(f => f.ID == field.ID && f.ID != default)
                        .SingleOrDefault();

                    if (existingField == null)
                    {
                        // Insert child
                        existingStack.Fields.Add(field);
                    }
                    else
                    {
                        // Update child
                        context.Entry(existingField).CurrentValues.SetValues(field);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e);  // TODO: Remove
                throw new DataSourceException("Could not update stack", e);
            }
        }

        private void EnsureItemDoesNotExist(DbSet<Notebook> dbSet, Notebook notebook)
        {
            if (dbSet.Any(item => item.Name == notebook.Name))
            {
                throw new DataSourceException($"A notebook named { notebook.Name } already exists");
            }
        }

        private async Task EnsureItemDoesNotExistAsync(DbSet<Notebook> dbSet, Notebook notebook)
        {
            if (await dbSet.AnyAsync(item => item.Name == notebook.Name))
            {
                throw new DataSourceException($"A notebook named { notebook.Name } already exists");
            }
        }
    }
}
