using Chronicy.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Web
{
    public class ChronicyWebApi : IDisposable
    {
        private IClient webClient;
        private ChronicyUrlBuilder urlBuilder;

        public int RetryAfter { get; set; } = 50;
        public bool UseAutoRetry { get; set; } = false;
        public int RetryTimes { get; set; } = 10;

        public bool TooManyRequestConsumesARetry { get; set; } = false;

        public IEnumerable<int> RetryErrorCodes { get; set; } = new[] { 500, 502, 503 };

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
    }
}
