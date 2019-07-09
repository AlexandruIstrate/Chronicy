using Chronicy.Data.Storage;
using Chronicy.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Data.Managers
{
    public class NotebookManager
    {
        public IDataSource<Notebook> DataSource { get; set; }

        public Notebook SelectedNotebook { get; private set; }
        public Stack SelectedStack { get; private set; }

        public event EventHandler NotebooksChanged;
        public event EventHandler NotebookSelectionChanged;
        public event EventHandler StackSelectionChanged;

        public NotebookManager(IDataSource<Notebook> dataSource)
        {
            DataSource = dataSource;
        }

        public List<Notebook> GetNotebooks()
        {
            return new List<Notebook>(DataSource.GetAll());
        }

        public async Task<List<Notebook>> GetNotebooksAsync()
        {
            return new List<Notebook>(await DataSource.GetAllAsync());
        }

        public void AddNotebook(Notebook notebook)
        {
            DataSource.Create(notebook);
            OnNotebooksChanged();
        }

        public async Task AddNotebookAsync(Notebook notebook)
        {
            await DataSource.CreateAsync(notebook);
            OnNotebooksChanged();
        }

        public void AddStack(Stack stack)
        {
            SelectedNotebook.Add(stack);
            DataSource.Update(SelectedNotebook);

            OnNotebooksChanged();
        }

        public async Task AddStackAsync(Stack stack)
        {
            SelectedNotebook.Add(stack);
            await DataSource.UpdateAsync(SelectedNotebook);

            OnNotebooksChanged();
        }

        public void AddCard(Card card)
        {
            SelectedStack.Add(card);
            DataSource.Update(SelectedNotebook);

            OnNotebooksChanged();
        }

        public async Task AddCardAsync(Card card)
        {
            SelectedStack.Add(card);
            await DataSource.UpdateAsync(SelectedNotebook);

            OnNotebooksChanged();
        }

        public void SelectNotebook(string name)
        {
            SelectedNotebook = GetNotebooks().Find((item) => item.Name == name);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(name);
            }

            OnNotebookSelectionChanged();
        }

        public void SelectNotebook(Notebook notebook)
        {
            SelectedNotebook = GetNotebooks().Find((item) => item.Uuid == notebook.Uuid);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(notebook.Name);
            }

            OnNotebookSelectionChanged();
        }

        public void SelectStack(string name)
        {
            SelectedStack = SelectedNotebook.Stacks.ToList().Find((item) => item.Name == name);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(name);
            }

            OnStackSelectionChanged();
        }

        public void SelectStack(Stack stack)
        {
            SelectedStack = SelectedNotebook.Stacks.ToList().Find((item) => item == stack);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(stack.Name);
            }

            OnStackSelectionChanged();
        }

        private void OnNotebooksChanged()
        {
            NotebooksChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnNotebookSelectionChanged()
        {
            NotebookSelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnStackSelectionChanged()
        {
            StackSelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
