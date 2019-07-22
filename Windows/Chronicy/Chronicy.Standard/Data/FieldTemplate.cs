using System.Collections.Generic;

namespace Chronicy.Data
{
    public class FieldTemplate
    {
        public List<CustomField> Fields { get; }

        public FieldTemplate(IEnumerable<CustomField> fields)
        {
            Fields = new List<CustomField>(fields);
        }

        public FieldTemplate(params CustomField[] fields)
        {
            Fields = new List<CustomField>(fields);
        }

        public bool Matches(FieldTemplate other)
        {
            if (other.Fields.Count < Fields.Count)
            {
                return false;
            }
            foreach (CustomField field in Fields)
            {
                if (!other.Fields.Exists((iter) => iter.Type == field.Type))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
