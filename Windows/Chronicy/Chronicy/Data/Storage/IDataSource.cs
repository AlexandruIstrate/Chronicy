using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    // TODO: Make generic so that it supports any type
    public interface IDataSource
    {
        Notebook GetNotebook(string id);
        Task<Notebook> GetNotebookAsync(string id);
        IEnumerable<Notebook> GetNotebooks();
        Task<IEnumerable<Notebook>> GetNotebooksAsync();

        Notebook CreateNotebook(string name);
        Task<Notebook> CreateNotebookAsync(string name);
        void DeleteNotebook(Notebook notebook);
        Task DeleteNotebookAsync(Notebook notebook);

        void UpdateNotebook(Notebook notebook);
        Task UpdateNotebookAsync(Notebook notebook);
    }
}
