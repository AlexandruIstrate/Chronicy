using Chronicy.Data;
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
                Id = value.SystemId,
                Name = value.Name,
                Stacks = value.Stacks.ConvertAll(input => stackConverter.Value.Convert(input))
            };
        }

        public Data.Notebook ReverseConvert(Models.Notebook value)
        {
            return new Data.Notebook(value.Name)
            {
                SystemId = value.Id,
                Stacks = value.Stacks.ConvertAll(input => stackConverter.Value.ReverseConvert(input))
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
