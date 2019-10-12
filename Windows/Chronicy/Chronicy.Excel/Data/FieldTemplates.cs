using Chronicy.Data;
using System.Collections.Generic;

namespace Chronicy.Excel.Data
{
    /// <summary>
    /// Represents a template for the way fields should be structured.
    /// </summary>
    public class FieldTemplates
    {
        public static FieldTemplate ExtensionDefault = new FieldTemplate(new List<CustomField>
        {
            new CustomField("ObjectName", FieldType.String),
            new CustomField("ModifiedItems", FieldType.String)
        });
    }
}
