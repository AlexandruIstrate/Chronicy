using Chronicy.Data;
using System;

namespace Chronicy.Web.Converters
{
    public class CardConverter : IConverter<Data.Card, Web.Models.Card>
    {
        private Lazy<CustomFieldConverter> fieldConverter = new Lazy<CustomFieldConverter>();

        public bool CanReverseConvert => true;

        public Models.Card Convert(Data.Card value)
        {
            return new Models.Card
            {
                Name = value.Name,
                Comment = value.Comment,
                Fields = value.Fields.ConvertAll(input => fieldConverter.Value.Convert(input))
            };
        }

        public Data.Card ReverseConvert(Models.Card value)
        {
            return new Data.Card(value.Name)
            {
                Comment = value.Comment,
                Fields = value.Fields.ConvertAll(input => fieldConverter.Value.ReverseConvert(input))
            };
        }
    }
}
