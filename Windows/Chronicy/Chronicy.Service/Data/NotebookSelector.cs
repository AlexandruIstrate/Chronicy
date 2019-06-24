using Chronicy.Data;
using Chronicy.Data.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Service.Data
{
    public class NotebookSelector
    {
        private IDataSource dataSource;

        // TODO: Maybe fetch at an interval?
        public List<Notebook> Notebooks => new List<Notebook>(dataSource.GetNotebooks());

        public Notebook SelectedNotebook { get; private set; }
        public Stack SelectedStack { get; private set; }

        public NotebookSelector()
        {
            dataSource = new WebDataSource();
        }

        public void AddNotebook(Notebook notebook)
        {
            // TODO: Provide a method that lets us upload a new notebook so that we can skip the ID transfer
            Notebook created = dataSource.CreateNotebook(notebook.Name);
            notebook.Id = created.Id;
            dataSource.UpdateNotebook(notebook);
        }

        public async Task AddNotebookAsync(Notebook notebook)
        {
            // TODO: Provide a method that lets us upload a new notebook so that we can skip the ID transfer
            Notebook created = await dataSource.CreateNotebookAsync(notebook.Name);
            notebook.Id = created.Id;
            await dataSource.UpdateNotebookAsync(notebook);
        }

        public void AddStack(Stack stack)
        {
            SelectedNotebook.Add(stack);
            dataSource.UpdateNotebook(SelectedNotebook);
        }

        public async Task AddStackAsync(Stack stack)
        {
            SelectedNotebook.Add(stack);
            await dataSource.UpdateNotebookAsync(SelectedNotebook);
        }

        public void AddCard(Card card)
        {
            SelectedStack.Add(card);
            dataSource.UpdateNotebook(SelectedNotebook);
        }

        public async Task AddCardAsync(Card card)
        {
            SelectedStack.Add(card);
            await dataSource.UpdateNotebookAsync(SelectedNotebook);
        }

        public void SelectNotebook(string name)
        {
            SelectedNotebook = Notebooks.Find((item) => item.Name == name);
            SelectedStack = null;   // Invalidate the stack

            if (SelectedNotebook == null)
            {
                throw new ItemNotFoundException(name);
            }
        }

        public void SelectNotebook(Notebook notebook)
        {
            SelectedNotebook = Notebooks.Find((item) => item == notebook);
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
            SelectedStack = SelectedNotebook.Stacks.Find((item) => item== stack);

            if (SelectedStack == null)
            {
                throw new ItemNotFoundException(stack.Name);
            }
        }
    }
}
