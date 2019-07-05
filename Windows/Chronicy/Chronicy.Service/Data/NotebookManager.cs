using Chronicy.Data;
using Chronicy.Data.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Service.Data
{
    public class NotebookManager
    {
        private IDataSource<Notebook> dataSource;

        public Notebook SelectedNotebook { get; private set; }
        public Stack SelectedStack { get; private set; }

        public NotebookManager()
        {
            dataSource = new LocalDataSource();
        }

        public List<Notebook> GetNotebooks()
        {
            return new List<Notebook>(dataSource.GetAll());
        }

        public async Task<List<Notebook>> GetNotebooksAsync()
        {
            return new List<Notebook>(await dataSource.GetAllAsync());
        }

        public void AddNotebook(Notebook notebook)
        {
            dataSource.Create(notebook);
        }

        public Task AddNotebookAsync(Notebook notebook)
        {
            return dataSource.CreateAsync(notebook);
        }

        public void AddStack(Stack stack)
        {
            SelectedNotebook.Add(stack);
            dataSource.Update(SelectedNotebook);
        }

        public async Task AddStackAsync(Stack stack)
        {
            SelectedNotebook.Add(stack);
            await dataSource.UpdateAsync(SelectedNotebook);
        }

        public void AddCard(Card card)
        {
            SelectedStack.Add(card);
            dataSource.Update(SelectedNotebook);
        }

        public async Task AddCardAsync(Card card)
        {
            SelectedStack.Add(card);
            await dataSource.UpdateAsync(SelectedNotebook);
        }

        public void SelectNotebook(string name)
        {
            SelectedNotebook = GetNotebooks().Find((item) => item.Name == name);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(name);
            }
        }

        public void SelectNotebook(Notebook notebook)
        {
            SelectedNotebook = GetNotebooks().Find((item) => item == notebook);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(notebook.Name);
            }
        }

        public void SelectStack(string name)
        {
            SelectedStack = SelectedNotebook.Stacks.Find((item) => item.Name == name);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(name);
            }
        }

        public void SelectStack(Stack stack)
        {
            SelectedStack = SelectedNotebook.Stacks.Find((item) => item == stack);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(stack.Name);
            }
        }
    }
}
