using Chronicy.Data;
using Chronicy.Data.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Statistics
{
    public class StatisticsGenerator : IStatisticsGenerator
    {
        public IDataSource<Notebook> DataSource { get; set; }

        public StatisticsGenerator(IDataSource<Notebook> dataSource)
        {
            DataSource = dataSource;
        }

        public IEnumerable<StatisticsItem> Generate()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StatisticsItem>> GenerateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
