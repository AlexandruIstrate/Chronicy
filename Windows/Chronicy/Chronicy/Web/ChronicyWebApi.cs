using Chronicy.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public Task<Token> Authenticate(string username, string password)
        {
            JObject body = new JObject
            {
                { "username", username },
                { "password", password }
            };

            return UploadDataAsync<Token>(urlBuilder.GetToken(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public Task<ListResponse<Notebook>> GetNotebooksAsync()
        {
            return DownloadDataAsync<ListResponse<Notebook>>(urlBuilder.GetNotebooks());
        }

        public Task<Notebook> GetNotebookAsync(string id)
        {
            return DownloadDataAsync<Notebook>(urlBuilder.GetNotebook(id));
        }

        public Task<Notebook> CreateNotebookAsync(string name)
        {
            JObject body = new JObject
            {
                { "name", name }
            };

            return UploadDataAsync<Notebook>(urlBuilder.CreateNotebook(), body.ToString(Formatting.None), ClientMethod.Post);
        }

        public Task<ErrorResponse> DeleteNotebookAsync(Notebook notebook)
        {
            return UploadDataAsync<ErrorResponse>(urlBuilder.DeleteNotebook(notebook.Id), string.Empty, ClientMethod.Delete);
        }

        public Task<ErrorResponse> UpdateNotebookAsync(Notebook notebook)
        {
            return UploadDataAsync<ErrorResponse>(urlBuilder.UpdateNotebook(notebook.Id), JsonConvert.SerializeObject(notebook), ClientMethod.Put);
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

        private async Task<T> DownloadDataAsync<T>(string url) where T : ModelBase
        {
            Tuple<ResponseInfo, T> response = await DownloadDataAltAsync<T>(url).ConfigureAwait(false);
            response.Item2.SetResponseInfo(response.Item1);
            return response.Item2;
        }

        private Task<Tuple<ResponseInfo, T>> DownloadDataAltAsync<T>(string url)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", AccessToken);
            return webClient.DownloadJsonAsync<T>(url, headers);
        }

    }
}
