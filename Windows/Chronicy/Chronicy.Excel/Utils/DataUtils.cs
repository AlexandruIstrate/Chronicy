using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Chronicy.Excel.Utils
{
    public static class DataUtils
    {
        public static DataColumn[] CreateDataColumns<T>(params string[] ignoredColumns)
        {
            Type type = typeof(T);

            List<DataColumn> columns = new List<DataColumn>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (ignoredColumns.ToList().Contains(property.Name))
                {
                    // Skip ignored columns
                    continue;
                }

                if (property.PropertyType.IsCollection())
                {
                    // Skip child collections
                    continue;
                }

                columns.Add(new DataColumn { ColumnName = property.Name, DataType = property.PropertyType });
            }

            return columns.ToArray();
        }
    }
}
