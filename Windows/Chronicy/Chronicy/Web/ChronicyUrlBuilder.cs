namespace Chronicy.Web
{
    public class ChronicyUrlBuilder
    {
        public string BaseUrl { get; set; }

        public ChronicyUrlBuilder(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string GetToken()
        {
            return $"{ BaseUrl }/auth";
        }

        public string GetNotebooks()
        {
            return $"{ BaseUrl }/notebooks";
        }

        public string GetNotebook(string id)
        {
            return $"{ BaseUrl }/notebooks?id={ id }";
        }

        public string CreateNotebook()
        {
            return $"{ BaseUrl }/notebooks/create";
        }

        public string DeleteNotebook(string id)
        {
            return $"{ BaseUrl }/notebooks/delete?id={ id }";
        }

        public string UpdateNotebook(string id)
        {
            return $"{ BaseUrl }/notebooks/update?id={ id }";
        }
    }
}
