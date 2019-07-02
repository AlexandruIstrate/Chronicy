using Chronicy.Data;
using System.Collections.Generic;

namespace Chronicy.Excel.Data
{
    public class FieldTemplates
    {
        public static FieldTemplate ExtensionDefault = new FieldTemplate(new List<CustomField>
        {
            new CustomField("ObjectName", FieldType.String),
            new CustomField("ModifiedItems", FieldType.String)
        });
    }
}
