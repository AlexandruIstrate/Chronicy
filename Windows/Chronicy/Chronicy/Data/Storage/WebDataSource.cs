using Chronicy.Web;
using Chronicy.Web.Converters;
using Chronicy.Web.Exceptions;
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
            try
            {
                return converter.ReverseConvert(api.CreateNotebook(name));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public async Task<Notebook> CreateNotebookAsync(string name)
        {
            try
            {
                return converter.ReverseConvert(await api.CreateNotebookAsync(name));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public void DeleteNotebook(Notebook notebook)
        {
            try
            {
                api.DeleteNotebook(notebook.Id.ToString());
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public async Task DeleteNotebookAsync(Notebook notebook)
        {
            try
            {
                await api.DeleteNotebookAsync(notebook.Id.ToString());
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public Notebook GetNotebook(string id)
        {
            try
            {
                return converter.ReverseConvert(api.GetNotebook(id));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not get notebook", e);
            }
        }

        public async Task<Notebook> GetNotebookAsync(string id)
        {
            try
            {
                return converter.ReverseConvert(await api.GetNotebookAsync(id));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not get notebook", e);
            }
        }

        public IEnumerable<Notebook> GetNotebooks()
        {
            try
            {
                return api.GetNotebooks().List.ConvertAll(item => converter.ReverseConvert(item));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not get notebooks", e);
            }
        }

        public async Task<IEnumerable<Notebook>> GetNotebooksAsync()
        {
            try
            {
                return (await api.GetNotebooksAsync()).List.ConvertAll(input => converter.ReverseConvert(input));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not get notebooks", e);
            }
        }

        public void UpdateNotebook(Notebook notebook)
        {
            try
            {
                api.UpdateNotebook(converter.Convert(notebook));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not update notebook", e);
            }
        }

        public async Task UpdateNotebookAsync(Notebook notebook)
        {
            try
            {
                await api.UpdateNotebookAsync(converter.Convert(notebook));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not update notebook", e);
            }
        }
    }
}
