namespace Chronicy.Data.Statistics
{
    public class StatisticsGeneratorFactory
    {
        public static IStatisticsGenerator Create(StatisticsType type)
        {
            switch (type)
            {
                case StatisticsType.Usage:
                    return new UsageStatisticsGenerator(null);  // TODO: DataSource
                default:
                    return null;
            }
        }
    }

    public enum StatisticsType
    {
        Usage
    }
}
