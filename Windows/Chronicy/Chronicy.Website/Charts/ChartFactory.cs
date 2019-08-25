using System;

namespace Chronicy.Website.Charts
{
    public class ChartFactory
    {
        public static IChart Create(ChartType chartType)
        {
            switch (chartType)
            {
                case ChartType.Line:
                    break;
                case ChartType.Bar:
                    return new BarChart();
                case ChartType.Pie:
                    break;
                default:
                    return null;
            }

            throw new NotSupportedException();
        }
    }

    public enum ChartType
    {
        Line, Bar, Pie
    }
}
