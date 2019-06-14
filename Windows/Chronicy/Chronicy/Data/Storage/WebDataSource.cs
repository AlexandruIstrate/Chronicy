using Chronicy.Web;
using Chronicy.Web.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class WebDataSource : IDataSource
    {
        private ChronicyWebApi api = new ChronicyWebApi();
        private NotebookConverter converter = new NotebookConverter();

        public Notebook CreateNotebook(string name)
        {
            return converter.ReverseConvert(api.CreateNotebook(name));
        }

        public async Task<Notebook> CreateNotebookAsync(string name)
        {
            return converter.ReverseConvert(await api.CreateNotebookAsync(name));
        }

        public void DeleteNotebook(Notebook notebook)
        {
            api.DeleteNotebook(notebook.Id.ToString());
        }

        public async Task DeleteNotebookAsync(Notebook notebook)
        {
            await api.DeleteNotebookAsync(notebook.Id.ToString());
        }

        public Notebook GetNotebook(long id)
        {
            return converter.ReverseConvert(api.GetNotebook(id.ToString()));
        }

        public async Task<Notebook> GetNotebookAsync(long id)
        {
            return converter.ReverseConvert(await api.GetNotebookAsync(id.ToString()));
        }

        public IEnumerable<Notebook> GetNotebooks()
        {
            return api.GetNotebooks().List.ConvertAll(item => converter.ReverseConvert(item));
        }

        public async Task<IEnumerable<Notebook>> GetNotebooksAsync()
        {
            return (await api.GetNotebooksAsync()).List.ConvertAll(input => converter.ReverseConvert(input));
        }

        public void UpdateNotebook(Notebook notebook)
        {
            api.UpdateNotebook(converter.Convert(notebook));
        }

        public async Task UpdateNotebookAsync(Notebook notebook)
        {
            await api.UpdateNotebookAsync(converter.Convert(notebook));
        }
    }
}
