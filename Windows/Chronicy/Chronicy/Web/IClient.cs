using System;
using System.Threading.Tasks;
using HeaderCollection = System.Collections.Generic.Dictionary<string, string>;

namespace Chronicy.Web
{
    public interface IClient : IDisposable
    {
        Task<Tuple<ResponseInfo, string>> DownloadAsync(string url, HeaderCollection headers = null);
        Task<Tuple<ResponseInfo, byte[]>> DownloadRawAsync(string url, HeaderCollection headers = null);
        Task<Tuple<ResponseInfo, T>> DownloadJsonAsync<T>(string url, HeaderCollection headers = null);

        Task<Tuple<ResponseInfo, string>> UploadAsync(string url, string body, ClientMethod method, HeaderCollection headers = null);
        Task<Tuple<ResponseInfo, byte[]>> UploadRawAsync(string url, string body, ClientMethod method, HeaderCollection headers = null);
        Task<Tuple<ResponseInfo, T>> UploadJsonAsync<T>(string url, string body, ClientMethod method, HeaderCollection headers = null);
    }

    public enum ClientMethod
    {
        Put, Post, Delete, Head, Options, Trace
    }
}
