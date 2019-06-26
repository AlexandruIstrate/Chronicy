using Chronicy.Web.Exceptions;
using Chronicy.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chronicy.Web
{
    public class ChronicyWebApi : IDisposable
    {
        private IClient webClient;
        private ChronicyUrlBuilder urlBuilder;

        public string AccessToken { get; set; }

        public ChronicyWebApi()
        {
            webClient = new ChronicyWebClient();
            urlBuilder = new ChronicyUrlBuilder("URL Here");
        }

        public void Dispose()
        {
            webClient.Dispose();
            GC.SuppressFinalize(true);
        }

        public Token Authenticate(string username, string password)
        {
            try
            {
                JObject body = new JObject
                {
                    { "username", username },
                    { "password", password }
                };

                return UploadData<Token>(urlBuilder.GetToken(), body.ToString(Formatting.None), ClientMethod.Post);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetToken(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetToken(), e);
            }
        }

        public Task<Token> AuthenticateAsync(string username, string password)
        {
            try
            {
                JObject body = new JObject
                {
                    { "username", username },
                    { "password", password }
                };

                return UploadDataAsync<Token>(urlBuilder.GetToken(), body.ToString(Formatting.None), ClientMethod.Post);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetToken(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetToken(), e);
            }
        }

        public ListResponse<Notebook> GetNotebooks()
        {
            try
            {
                return DownloadData<ListResponse<Notebook>>(urlBuilder.GetNotebooks());
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetNotebooks(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetNotebooks(), e);
            }
        }

        public Task<ListResponse<Notebook>> GetNotebooksAsync()
        {
            try
            {
                return DownloadDataAsync<ListResponse<Notebook>>(urlBuilder.GetNotebooks());
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetNotebooks(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetNotebooks(), e);
            }
        }

        public Notebook GetNotebook(string id)
        {
            try
            {
                return DownloadData<Notebook>(urlBuilder.GetNotebook(id));
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetNotebook(id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetNotebook(id), e);
            }
        }

        public Task<Notebook> GetNotebookAsync(string id)
        {
            try
            {
                return DownloadDataAsync<Notebook>(urlBuilder.GetNotebook(id));
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.GetNotebook(id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.GetNotebook(id), e);
            }
        }

        public Notebook CreateNotebook(string name)
        {
            try
            {
                JObject body = new JObject
                {
                    { "name", name }
                };

                return UploadData<Notebook>(urlBuilder.CreateNotebook(), body.ToString(Formatting.None), ClientMethod.Post);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.CreateNotebook(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.CreateNotebook(), e);
            }
        }

        public Task<Notebook> CreateNotebookAsync(string name)
        {
            try
            {
                JObject body = new JObject
                {
                    { "name", name }
                };

                return UploadDataAsync<Notebook>(urlBuilder.CreateNotebook(), body.ToString(Formatting.None), ClientMethod.Post);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.CreateNotebook(), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.CreateNotebook(), e);
            }
        }

        public ErrorResponse DeleteNotebook(string id)
        {
            try
            {
                return UploadData<ErrorResponse>(urlBuilder.DeleteNotebook(id), string.Empty, ClientMethod.Delete);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.DeleteNotebook(id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.DeleteNotebook(id), e);
            }
        }

        public Task<ErrorResponse> DeleteNotebookAsync(string id)
        {
            try
            {
                return UploadDataAsync<ErrorResponse>(urlBuilder.DeleteNotebook(id), string.Empty, ClientMethod.Delete);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.DeleteNotebook(id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.DeleteNotebook(id), e);
            }
        }

        public ErrorResponse UpdateNotebook(Notebook notebook)
        {
            try
            {
                return UploadData<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.Id), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.UpdateNotebook(notebook.Id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.UpdateNotebook(notebook.Id), e);
            }
        }

        public Task<ErrorResponse> UpdateNotebookAsync(Notebook notebook)
        {
            try
            {
                return UploadDataAsync<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.Id), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.UpdateNotebook(notebook.Id), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.UpdateNotebook(notebook.Id), e);
            }
        }

        private T UploadData<T>(string url, string data, ClientMethod method) where T : ModelBase
        {
            // TODO: Create header name constants (or use existing .NET Framework ones)
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", AccessToken },
                { "Content-Type", "application/json" }
            };

            Tuple<ResponseInfo, T> response = webClient.UploadJson<T>(url, data, method, headers);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private async Task<T> UploadDataAsync<T>(string url, string data, ClientMethod method) where T : ModelBase
        {
            // TODO: Create header name constants (or use existing .NET Framework ones)
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", AccessToken },
                { "Content-Type", "application/json" }
            };

            Tuple<ResponseInfo, T> response = await webClient.UploadJsonAsync<T>(url, data, method, headers).ConfigureAwait(false);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private T DownloadData<T>(string url) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = DownloadDataAlt<T>(url);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private async Task<T> DownloadDataAsync<T>(string url) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = await DownloadDataAltAsync<T>(url).ConfigureAwait(false);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private Tuple<ResponseInfo, T> DownloadDataAlt<T>(string url)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", AccessToken);
            return webClient.DownloadJson<T>(url, headers);
        }

        private Task<Tuple<ResponseInfo, T>> DownloadDataAltAsync<T>(string url)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", AccessToken);
            return webClient.DownloadJsonAsync<T>(url, headers);
        }
    }
}
