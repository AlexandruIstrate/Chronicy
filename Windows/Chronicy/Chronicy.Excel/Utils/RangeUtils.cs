using Microsoft.Office.Interop.Excel;

namespace Chronicy.Excel.Utils
{
    public static class RangeUtils
    {
        public static bool Contains(this Range range, Range other)
        {
            bool xContains = ((other.Column >= range.Column) && (other.Column + other.Columns.Count <= range.Column + range.Columns.Count));
            bool yContains = ((other.Row >= range.Row) && (other.Row + other.Rows.Count <= range.Row + range.Rows.Count));

            return xContains && yContains;
        }
    }
}
