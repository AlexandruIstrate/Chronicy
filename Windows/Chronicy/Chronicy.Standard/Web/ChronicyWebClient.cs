using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chronicy.Web
{
    public class ChronicyWebClient : IClient
    {
        private HttpClient client;

        public Encoding Encoding { get; set; }
        public JsonSerializerSettings JsonSettings { get; set; }

        public ChronicyWebClient(Encoding encoding = null)
        {
            client = new HttpClient();
            Encoding = encoding ?? Encoding.UTF8;
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        public Tuple<ResponseInfo, string> Download(string url, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, byte[]> raw = DownloadRaw(url, headers);
            return new Tuple<ResponseInfo, string>(raw.Item1, raw.Item2.Length > 0 ? Encoding.GetString(raw.Item2) : ClientConstants.EmptyJson);
        }

        public async Task<Tuple<ResponseInfo, string>> DownloadAsync(string url, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, byte[]> raw = await DownloadRawAsync(url, headers).ConfigureAwait(false);
            return new Tuple<ResponseInfo, string>(raw.Item1, raw.Item2.Length > 0 ? Encoding.GetString(raw.Item2) : ClientConstants.EmptyJson);
        }

        public Tuple<ResponseInfo, byte[]> DownloadRaw(string url, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                AddHeaders(headers);
            }

            using (HttpResponseMessage response = Task.Run(() => client.GetAsync(url)).Result)
            {
                return new Tuple<ResponseInfo, byte[]>(new ResponseInfo
                {
                    StatusCode = response.StatusCode,
                    Headers = ConvertHeaders(response.Headers)
                }, Task.Run(() => response.Content.ReadAsByteArrayAsync()).Result);
            }
        }

        public async Task<Tuple<ResponseInfo, byte[]>> DownloadRawAsync(string url, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                AddHeaders(headers);
            }

            using (HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false))
            {
                return new Tuple<ResponseInfo, byte[]>(new ResponseInfo
                {
                    StatusCode = response.StatusCode,
                    Headers = ConvertHeaders(response.Headers)
                }, await response.Content.ReadAsByteArrayAsync());
            }
        }

        public Tuple<ResponseInfo, T> DownloadJson<T>(string url, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, string> response = Download(url, headers);

            try
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(response.Item2, JsonSettings));
            }
            catch (JsonException)
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(ClientConstants.UnknownErrorJson, JsonSettings));
            }
        }

        public async Task<Tuple<ResponseInfo, T>> DownloadJsonAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, string> response = await DownloadAsync(url, headers).ConfigureAwait(false);

            try
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(response.Item2, JsonSettings));
            }
            catch (JsonException)
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(ClientConstants.UnknownErrorJson, JsonSettings));
            }
        }

        public Tuple<ResponseInfo, string> Upload(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, byte[]> data = UploadRaw(url, body, method, headers);
            return new Tuple<ResponseInfo, string>(data.Item1, data.Item2.Length > 0 ? Encoding.GetString(data.Item2) : ClientConstants.EmptyJson);
        }

        public async Task<Tuple<ResponseInfo, string>> UploadAsync(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, byte[]> data = await UploadRawAsync(url, body, method, headers).ConfigureAwait(false);
            return new Tuple<ResponseInfo, string>(data.Item1, data.Item2.Length > 0 ? Encoding.GetString(data.Item2) : ClientConstants.EmptyJson);
        }

        public Tuple<ResponseInfo, byte[]> UploadRaw(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                AddHeaders(headers);
            }

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod(method.ToString().ToUpper()), url);
            message.Content = new StringContent(body, Encoding);

            using (HttpResponseMessage response = Task.Run(() => client.SendAsync(message)).Result)
            {
                return new Tuple<ResponseInfo, byte[]>(new ResponseInfo
                {
                    StatusCode = response.StatusCode,
                    Headers = ConvertHeaders(response.Headers)
                }, Task.Run(() => response.Content.ReadAsByteArrayAsync()).Result);
            }
        }

        public async Task<Tuple<ResponseInfo, byte[]>> UploadRawAsync(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                AddHeaders(headers);
            }

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod(method.ToString().ToUpper()), url);
            message.Content = new StringContent(body, Encoding);

            using (HttpResponseMessage response = await client.SendAsync(message))
            {
                return new Tuple<ResponseInfo, byte[]>(new ResponseInfo
                {
                    StatusCode = response.StatusCode,
                    Headers = ConvertHeaders(response.Headers)
                }, await response.Content.ReadAsByteArrayAsync());
            }
        }

        public Tuple<ResponseInfo, T> UploadJson<T>(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, string> response = Upload(url, body, method, headers);

            try
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(response.Item2, JsonSettings));
            }
            catch (JsonException)
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(ClientConstants.UnknownErrorJson, JsonSettings));
            }
        }

        public async Task<Tuple<ResponseInfo, T>> UploadJsonAsync<T>(string url, string body, ClientMethod method, Dictionary<string, string> headers = null)
        {
            Tuple<ResponseInfo, string> response = await UploadAsync(url, body, method, headers).ConfigureAwait(false);

            try
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(response.Item2, JsonSettings));
            }
            catch (JsonException)
            {
                return new Tuple<ResponseInfo, T>(response.Item1, JsonConvert.DeserializeObject<T>(ClientConstants.UnknownErrorJson, JsonSettings));
            }
        }

        private void AddHeaders(Dictionary<string, string> headers)
        {
            client.DefaultRequestHeaders.Clear();

            foreach (KeyValuePair<string, string> headerPair in headers)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(headerPair.Key, headerPair.Value);
            }
        }

        private WebHeaderCollection ConvertHeaders(HttpResponseHeaders headers)
        {
            WebHeaderCollection result = new WebHeaderCollection();

            foreach (KeyValuePair<string, IEnumerable<string>> headerPair in headers)
            {
                foreach (string headerValue in headerPair.Value)
                {
                    result.Add(headerPair.Key, headerValue);
                }
            }

            return result;
        }
    }
}
