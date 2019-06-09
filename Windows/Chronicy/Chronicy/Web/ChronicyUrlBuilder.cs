namespace Chronicy.Web
{
    public class ChronicyUrlBuilder
    {
        public string BaseUrl { get; set; }

        public ChronicyUrlBuilder(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string Token()
        {
            return $"{ BaseUrl }/auth";
        }
    }
}
