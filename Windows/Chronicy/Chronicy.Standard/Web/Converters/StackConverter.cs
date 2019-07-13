using Chronicy.Data;
using System;
using System.Linq;

namespace Chronicy.Web.Converters
{
    public class StackConverter : IConverter<Data.Stack, Web.Models.Stack>
    {
        private Lazy<CardConverter> cardConverter = new Lazy<CardConverter>();

        public bool CanReverseConvert => true;

        public Models.Stack Convert(Data.Stack value)
        {
            return new Models.Stack
            {
                Name = value.Name,
                Cards = value.Cards.ToList().ConvertAll(input => cardConverter.Value.Convert(input))
            };
        }

        public Data.Stack ReverseConvert(Models.Stack value)
        {
            return new Data.Stack(value.Name)
            {
                Cards = value.Cards.ConvertAll(input => cardConverter.Value.ReverseConvert(input))
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
