﻿namespace Chronicy.Web
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
            return $"{ BaseUrl }/notebook";
        }

        public string GetNotebook(string id)
        {
            return $"{ BaseUrl }/notebook?id={ id }";
        }

        public string CreateNotebook()
        {
            return $"{ BaseUrl }/notebook/create";
        }

        public string DeleteNotebook(string id)
        {
            return $"{ BaseUrl }/notebook/delete?id={ id }";
        }

        public string UpdateNotebook(string id)
        {
            return $"{ BaseUrl }/notebook/update?id={ id }";
        }
    }
}