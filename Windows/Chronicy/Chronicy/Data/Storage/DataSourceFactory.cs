namespace Chronicy.Data.Storage
{
    class DataSourceFactory
    {
        public IDataSource<Notebook> Create(DataSourceType sourceType)
        {
            switch (sourceType)
            {
                case DataSourceType.Web:
                    return new WebDataSource();
                case DataSourceType.Local:
                    return new LocalDataSource();
            }

            return null;
        }
    }

    public enum DataSourceType
    {
        Web, Local
    }
}
