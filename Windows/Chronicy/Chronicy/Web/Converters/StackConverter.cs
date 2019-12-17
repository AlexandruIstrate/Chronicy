using Chronicy.Data;
using Chronicy.Utils;
using System;

namespace Chronicy.Web.Converters
{
    public class StackConverter : IConverter<Data.Stack, Web.Models.Stack>
    {
        private readonly Lazy<CustomFieldConverter> fieldConverter = new Lazy<CustomFieldConverter>();
        private readonly Lazy<CardConverter> cardConverter = new Lazy<CardConverter>();

        public bool CanReverseConvert => true;

        public Models.Stack Convert(Data.Stack value)
        {
            return new Models.Stack
            {
                ID = value.ID,
                Name = value.Name,
                Fields = DataUtils.ValueOrDefault(value.Fields).ConvertAll((input) => fieldConverter.Value.Convert(input)),
                Cards = DataUtils.ValueOrDefault(value.Cards).ConvertAll(input => cardConverter.Value.Convert(input))
            };
        }

        public Data.Stack ReverseConvert(Models.Stack value)
        {
            return new Data.Stack(value.Name)
            {
                ID = value.ID,
                Fields = DataUtils.ValueOrDefault(value.Fields).ConvertAll(input => fieldConverter.Value.ReverseConvert(input)),
                Cards = DataUtils.ValueOrDefault(value.Cards).ConvertAll(input => cardConverter.Value.ReverseConvert(input))
            };
        }
    }

    public static class StackConverterExtensions
    {
        public static Models.Stack ToWebStack(this Data.Stack stack)
        {
            StackConverter converter = new StackConverter();
            return converter.Convert(stack);
        }

        public static Data.Stack ToDataStack(this Models.Stack stack)
        {
            StackConverter converter = new StackConverter();
            return converter.ReverseConvert(stack);
        }
    }
}
