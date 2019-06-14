using Chronicy.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Web
{
    // TODO: Add synchronous methods as well
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
            JObject body = new JObject
            {
                { "username", username },
                { "password", password }
            };

            return UploadData<Token>(urlBuilder.GetToken(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public Task<Token> AuthenticateAsync(string username, string password)
        {
            JObject body = new JObject
            {
                { "username", username },
                { "password", password }
            };

            return UploadDataAsync<Token>(urlBuilder.GetToken(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public ListResponse<Notebook> GetNotebooks()
        {
            return DownloadData<ListResponse<Notebook>>(urlBuilder.GetNotebooks());
        }

        public Task<ListResponse<Notebook>> GetNotebooksAsync()
        {
            return DownloadDataAsync<ListResponse<Notebook>>(urlBuilder.GetNotebooks());
        }

        public Notebook GetNotebook(string id)
        {
            return DownloadData<Notebook>(urlBuilder.GetNotebook(id));
        }

        public Task<Notebook> GetNotebookAsync(string id)
        {
            return DownloadDataAsync<Notebook>(urlBuilder.GetNotebook(id));
        }

        public Notebook CreateNotebook(string name)
        {
            JObject body = new JObject
            {
                { "name", name }
            };

            return UploadData<Notebook>(urlBuilder.CreateNotebook(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public Task<Notebook> CreateNotebookAsync(string name)
        {
            JObject body = new JObject
            {
                { "name", name }
            };

            return UploadDataAsync<Notebook>(urlBuilder.CreateNotebook(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public ErrorResponse DeleteNotebook(string id)
        {
            return UploadData<ErrorResponse>(urlBuilder.DeleteNotebook(id), string.Empty, ClientMethod.Delete);
        }

        public Task<ErrorResponse> DeleteNotebookAsync(string id)
        {
            return UploadDataAsync<ErrorResponse>(urlBuilder.DeleteNotebook(id), string.Empty, ClientMethod.Delete);
        }

        public ErrorResponse UpdateNotebook(Notebook notebook)
        {
            return UploadData<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.Id.ToString()), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
        }

        public Task<ErrorResponse> UpdateNotebookAsync(Notebook notebook)
        {
            return UploadDataAsync<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.Id.ToString()), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
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
