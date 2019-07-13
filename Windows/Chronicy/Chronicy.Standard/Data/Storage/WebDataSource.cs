using Chronicy.Web;
using Chronicy.Web.Converters;
using Chronicy.Web.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class WebDataSource : IDataSource<Notebook>
    {
        private ChronicyWebApi api = new ChronicyWebApi();
        private NotebookConverter converter = new NotebookConverter();

        public void Create(Notebook item)
        {
            try
            {
                api.CreateNotebook(converter.Convert(item));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public Task CreateAsync(Notebook item)
        {
            try
            {
                return api.CreateNotebookAsync(converter.Convert(item));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public void Delete(string id)
        {
            try
            {
                api.DeleteNotebook(id);
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await api.DeleteNotebookAsync(id);
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public Notebook Get(string id)
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

        public async Task<Notebook> GetAsync(string id)
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

        public IEnumerable<Notebook> GetAll()
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

        public async Task<IEnumerable<Notebook>> GetAllAsync()
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

        public void Update(Notebook item)
        {
            try
            {
                api.UpdateNotebook(converter.Convert(item));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not update notebook", e);
            }
        }

        public async Task UpdateAsync(Notebook item)
        {
            try
            {
                await api.UpdateNotebookAsync(converter.Convert(item));
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not update notebook", e);
            }
        }
    }
}
