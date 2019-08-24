using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Website.Charts
{
    public class ChartFactory
    {
        public static Chart Create(ChartType chartType)
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
