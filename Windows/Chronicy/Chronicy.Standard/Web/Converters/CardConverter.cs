using Chronicy.Data;
using Chronicy.Utils;
using System;

namespace Chronicy.Web.Converters
{
    public class CardConverter : IConverter<Data.Card, Web.Models.Card>
    {
        private readonly Lazy<CustomFieldConverter> fieldConverter = new Lazy<CustomFieldConverter>();
        private readonly Lazy<TagConverter> tagConverter = new Lazy<TagConverter>();

        public bool CanReverseConvert => true;

        public Models.Card Convert(Data.Card value)
        {
            return new Models.Card
            {
                ID = value.ID,
                Name = value.Name,
                Comment = value.Comment,
                Fields = DataUtils.ValueOrDefault(value.Fields).ConvertAll(input => fieldConverter.Value.Convert(input)),
                Tags = DataUtils.ValueOrDefault(value.Tags).ConvertAll((input) => tagConverter.Value.Convert(input))
            };
        }

        public Data.Card ReverseConvert(Models.Card value)
        {
            return new Data.Card(value.Name)
            {
                ID = value.ID,
                Comment = value.Comment,
                Fields = DataUtils.ValueOrDefault(value.Fields).ConvertAll(input => fieldConverter.Value.ReverseConvert(input)),
                Tags = DataUtils.ValueOrDefault(value.Tags).ConvertAll((input) => tagConverter.Value.ReverseConvert(input))
            };
        }
    }

    public static class CardConverterExtensions
    {
        public static Models.Card ToWebCard(this Data.Card card)
        {
            CardConverter converter = new CardConverter();
            return converter.Convert(card);
        }

        public static Data.Card ToDataCard(this Models.Card card)
        {
            CardConverter converter = new CardConverter();
            return converter.ReverseConvert(card);
        }
    }
}
