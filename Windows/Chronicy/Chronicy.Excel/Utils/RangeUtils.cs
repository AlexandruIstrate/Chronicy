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

        public static Range Intersection(this Range range, Range other)
        {
            // If the ranges aren't in the same sheet, then we can't compare them
            if (range.Worksheet != other.Worksheet)
            {
                return null;
            }

            return Globals.ThisAddIn.Application.Intersect(range, other);
        }

        public static bool Intersects(this Range range, Range other)
        {
            // If the ranges aren't in the same sheet, then we can't compare them
            if (range.Worksheet != other.Worksheet)
            {
                return false;
            }

            return (Globals.ThisAddIn.Application.Intersect(range, other) != null);
        }

        public static string ToAddressString(this Range range)
        {
            return range.Address[true, true, XlReferenceStyle.xlA1, false, null];
        }

        public static string ToDisplayAddressString(this Range range)
        {
            return range.Address[true, true, XlReferenceStyle.xlA1, false, null].Replace("$", "");
        }
    }
}
