using Chronicy.Data;
using Chronicy.Utils;
using System;

namespace Chronicy.Web.Converters
{
    public class NotebookConverter : IConverter<Data.Notebook, Web.Models.Notebook>
    {
        private Lazy<StackConverter> stackConverter = new Lazy<StackConverter>();

        public bool CanReverseConvert => true;

        public Models.Notebook Convert(Data.Notebook value)
        {
            return new Models.Notebook
            {
                ID = value.ID,
                Name = value.Name,
                Stacks = DataUtils.ValueOrDefault(value.Stacks).ConvertAll(input => stackConverter.Value.Convert(input))
            };
        }

        public Data.Notebook ReverseConvert(Models.Notebook value)
        {
            return new Data.Notebook(value.Name)
            {
                ID = value.ID,
                Stacks = DataUtils.ValueOrDefault(value.Stacks).ConvertAll(input => stackConverter.Value.ReverseConvert(input))
            };
        }
    }

    public static class NotebookConverterExtensions
    {
        public static Models.Notebook ToWebNotebook(this Data.Notebook notebook)
        {
            NotebookConverter converter = new NotebookConverter();
            return converter.Convert(notebook);
        }

        public static Data.Notebook ToDataNotebook(this Models.Notebook notebook)
        {
            NotebookConverter converter = new NotebookConverter();
            return converter.ReverseConvert(notebook);
        }
    }
}
