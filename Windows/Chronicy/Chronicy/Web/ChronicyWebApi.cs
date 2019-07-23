using Chronicy.Data.Encoders;
using Chronicy.Web.Exceptions;
using Chronicy.Web.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HeaderCollection = System.Collections.Generic.Dictionary<string, string>;

namespace Chronicy.Web
{
    public class ChronicyWebApi : IDisposable
    {
        private readonly IClient webClient;
        private readonly ChronicyUrlBuilder urlBuilder;
        private readonly IEncoder encoder;

        public string Url
        {
            get => urlBuilder.BaseUrl;
            set => urlBuilder.BaseUrl = value;
        }

        public Token AccessToken { get; set; }

        public ChronicyWebApi(string apiUrl)
        {
            webClient = new ChronicyWebClient(Encoding.UTF8, JsonContentType);
            urlBuilder = new ChronicyUrlBuilder(apiUrl);
            encoder = new Base64Encoder();
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
                HeaderCollection headers = new HeaderCollection
                {
                    { AuthorizationHeader, $"Basic { encoder.Encode($"{ username }:{ password }") }" }
                };

                AccessToken = UploadData<Token>(urlBuilder.GetToken(), string.Empty, ClientMethod.Post, headers);
                return AccessToken;
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

        public async Task<Token> AuthenticateAsync(string username, string password)
        {
            try
            {
                HeaderCollection headers = new HeaderCollection
                {
                    { AuthorizationHeader, $"Basic { encoder.Encode($"{ username }:{ password }") }" }
                };

                AccessToken = await UploadDataAsync<Token>(urlBuilder.GetToken(), string.Empty, ClientMethod.Post, headers);
                return AccessToken;
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

        public Notebook GetNotebook(int id)
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

        public Task<Notebook> GetNotebookAsync(int id)
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

        public void CreateNotebook(Notebook notebook)
        {
            try
            {
                string body = JsonConvert.SerializeObject(notebook);
                UploadData<ErrorResponse>(urlBuilder.CreateNotebook(), body, ClientMethod.Post);
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

        public Task CreateNotebookAsync(Notebook notebook)
        {
            try
            {
                string body = JsonConvert.SerializeObject(notebook);
                return UploadDataAsync<ErrorResponse>(urlBuilder.CreateNotebook(), body, ClientMethod.Post);
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

        public ErrorResponse DeleteNotebook(int id)
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

        public Task<ErrorResponse> DeleteNotebookAsync(int id)
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
                return UploadData<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.ID), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.UpdateNotebook(notebook.ID), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.UpdateNotebook(notebook.ID), e);
            }
        }

        public Task<ErrorResponse> UpdateNotebookAsync(Notebook notebook)
        {
            try
            {
                return UploadDataAsync<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.ID), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
            }
            catch (HttpRequestException e)
            {
                throw new WebApiConnectionException(urlBuilder.UpdateNotebook(notebook.ID), e);
            }
            catch (Exception e)
            {
                throw new WebApiException(urlBuilder.UpdateNotebook(notebook.ID), e);
            }
        }

        private T UploadData<T>(string url, string data, ClientMethod method, HeaderCollection headers = null) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = webClient.UploadJson<T>(url, data, method, headers ?? DefaultHeaders);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private async Task<T> UploadDataAsync<T>(string url, string data, ClientMethod method, HeaderCollection headers = null) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = await webClient.UploadJsonAsync<T>(url, data, method, headers ?? DefaultHeaders).ConfigureAwait(false);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private T DownloadData<T>(string url, HeaderCollection headers = null) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = DownloadDataAlt<T>(url, headers ?? DefaultHeaders);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private async Task<T> DownloadDataAsync<T>(string url, HeaderCollection headers = null) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = await DownloadDataAltAsync<T>(url, headers ?? DefaultHeaders).ConfigureAwait(false);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private Tuple<ResponseInfo, T> DownloadDataAlt<T>(string url, HeaderCollection headers)
        {
            return webClient.DownloadJson<T>(url, headers);
        }

        private Task<Tuple<ResponseInfo, T>> DownloadDataAltAsync<T>(string url, HeaderCollection headers)
        {
            return webClient.DownloadJsonAsync<T>(url, headers);
        }

        public HeaderCollection DefaultHeaders => new HeaderCollection
        {
            { AuthorizationHeader, AccessToken.AccessToken }
        };

        // TODO: We should get rid of this static field and find a better way of
        // accessing the API from multiple places in the application
        public static ChronicyWebApi Shared = new ChronicyWebApi(string.Empty);

        public const string AuthorizationHeader = "Authorization";
        public const string JsonContentType = "application/json";
    }
}
