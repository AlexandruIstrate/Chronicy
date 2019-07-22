using Chronicy.Data;

namespace Chronicy.Web.Converters
{
    public class TagConverter : IConverter<Data.Tag, Web.Models.Tag>
    {
        public bool CanReverseConvert => true;

        public Models.Tag Convert(Data.Tag value)
        {
            return new Models.Tag
            {
                ID = value.ID,
                Name = value.Name,
                Description = value.Description
            };
        }

        public Data.Tag ReverseConvert(Models.Tag value)
        {
            return new Data.Tag
            {
                ID = value.ID,
                Name = value.Name,
                Description = value.Description
            };
        }
    }

    public static class TagConverterExtensions
    {
        public static Models.Tag ToWebTag(this Data.Tag card)
        {
            TagConverter converter = new TagConverter();
            return converter.Convert(card);
        }

        public static Data.Tag ToDataTag(this Models.Tag card)
        {
            TagConverter converter = new TagConverter();
            return converter.ReverseConvert(card);
        }
    }
}
