using Chronicy.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Data.Managers
{
    /// <summary>
    /// Provides methods for managing the retrieval and selection of notebooks.
    /// </summary>
    public class NotebookManager
    {
        public IDataSource<Notebook> dataSource;
        public IDataSource<Notebook> DataSource
        {
            get => dataSource;
            set
            {
                dataSource = value;
                DataSourceChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Notebook SelectedNotebook { get; private set; }
        public Stack SelectedStack { get; private set; }

        public event EventHandler NotebooksChanged;
        public event EventHandler NotebookSelectionChanged;
        public event EventHandler StackSelectionChanged;

        public event EventHandler DataSourceChanged;

        public NotebookManager(IDataSource<Notebook> dataSource)
        {
            this.dataSource = dataSource;
        }

        public List<Notebook> GetNotebooks()
        {
            IEnumerable<Notebook> notebooks = dataSource.GetAll();

            if (notebooks == null)
            {
                return new List<Notebook>();
            }

            return notebooks.ToList();
        }

        public async Task<List<Notebook>> GetNotebooksAsync()
        {
            IEnumerable<Notebook> notebooks = await dataSource.GetAllAsync();

            if (notebooks == null)
            {
                return new List<Notebook>();
            }

            return notebooks.ToList();
        }

        public void AddNotebook(Notebook notebook)
        {
            dataSource.Create(notebook);
            OnNotebooksChanged();
        }

        public async Task AddNotebookAsync(Notebook notebook)
        {
            await dataSource.CreateAsync(notebook);
            OnNotebooksChanged();
        }

        public void AddStack(Stack stack)
        {
            SelectedNotebook.Add(stack);
            dataSource.Update(SelectedNotebook);

            OnNotebooksChanged();
        }

        public async Task AddStackAsync(Stack stack)
        {
            SelectedNotebook.Add(stack);
            await dataSource.UpdateAsync(SelectedNotebook);

            OnNotebooksChanged();
        }

        public void AddCard(Card card)
        {
            SelectedStack.Cards.Add(card);
            dataSource.Update(SelectedNotebook);

            OnNotebooksChanged();
        }

        public async Task AddCardAsync(Card card)
        {
            SelectedStack.Cards.Add(card);
            await dataSource.UpdateAsync(SelectedNotebook);

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
            SelectedNotebook = GetNotebooks().Find((item) => item.ID == notebook.ID);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(notebook.Name);
            }

            OnNotebookSelectionChanged();
        }

        public void SelectStack(string name)
        {
            SelectedStack = SelectedNotebook.Stacks.Find((item) => item.Name == name);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(name);
            }

            OnStackSelectionChanged();
        }

        public void SelectStack(Stack stack)
        {
            SelectedStack = SelectedNotebook.Stacks.Find((item) => item.Name == stack.Name);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(stack.Name);
            }

            OnStackSelectionChanged();
        }

        public void ClearSelection()
        {
            SelectedNotebook = null;
            SelectedStack = null;
        }

        public void UpdateNotebook(Notebook notebook)
        {
            dataSource.Update(notebook);
        }

        public Task UpdateNotebookAsync(Notebook notebook)
        {
            return dataSource.UpdateAsync(notebook);
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
