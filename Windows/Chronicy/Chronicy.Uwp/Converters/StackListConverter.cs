using Chronicy.Data;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace Chronicy.Uwp.Converters
{
    public class StackListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<Stack> stacks = (List<Stack>)value;
            return string.Join(", ", stacks.ConvertAll((stack) => stack.Name));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("Cannot convert stack names back to stacks");
        }
    }
}
