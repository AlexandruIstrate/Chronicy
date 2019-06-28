using Chronicy.Data;
using System;
using System.Data;

namespace Chronicy.Sql
{
    public class DataSetConverter : IConverter<DataSet, Notebook>
    {
        public bool CanReverseConvert => false;

        public Notebook Convert(DataSet value)
        {
            throw new NotImplementedException();
        }

        public DataSet ReverseConvert(Notebook value)
        {
            throw new Exception("This class does not support reverse conversion");
        }
    }
}
