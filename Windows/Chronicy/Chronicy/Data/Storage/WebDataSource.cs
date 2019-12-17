using Chronicy.Web;
using Chronicy.Web.Converters;
using Chronicy.Web.Exceptions;
using Chronicy.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class WebDataSource : IDataSource<Notebook>
    {
        private readonly ChronicyWebApi api = ChronicyWebApi.Shared;
        private readonly NotebookConverter converter = new NotebookConverter();

        public void Create(Notebook item)
        {
            try
            {
                ErrorResponse response = api.CreateNotebook(converter.Convert(item));

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public async Task CreateAsync(Notebook item)
        {
            try
            {
                ErrorResponse response = await api.CreateNotebookAsync(converter.Convert(item));

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not create notebook", e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                ErrorResponse response = api.DeleteNotebook(id);

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                ErrorResponse response = await api.DeleteNotebookAsync(id);

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not delete notebook", e);
            }
        }

        public Notebook Get(int id)
        {
            try
            {
                Web.Models.Notebook response = api.GetNotebook(id);

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }

                return converter.ReverseConvert(response);
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not get notebook", e);
            }
        }

        public async Task<Notebook> GetAsync(int id)
        {
            try
            {
                Web.Models.Notebook response = await api.GetNotebookAsync(id);

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }

                return converter.ReverseConvert(response);
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
                ListResponse<Web.Models.Notebook> response = api.GetNotebooks();

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }

                if (response.List == null)
                {
                    throw new DataSourceException("The API did not return any data");
                }

                return response.List.ConvertAll(item => converter.ReverseConvert(item));
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
                ListResponse<Web.Models.Notebook> response = await api.GetNotebooksAsync();

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }

                if (response.List == null)
                {
                    throw new DataSourceException("The API did not return any data");
                }

                return response.List.ConvertAll(item => converter.ReverseConvert(item));
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
                ErrorResponse response = api.UpdateNotebook(converter.Convert(item));

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
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
                ErrorResponse response = await api.UpdateNotebookAsync(converter.Convert(item));

                if (response.HasError)
                {
                    throw new DataSourceException("The API returned an error");
                }
            }
            catch (WebApiException e)
            {
                throw new DataSourceException("Could not update notebook", e);
            }
        }
    }
}
