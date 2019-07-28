using System.Collections.Generic;
using System.Linq;

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

            List<FieldType> currentTypes = Fields.ConvertAll(iter => iter.Type);
            List<FieldType> otherTypes = other.Fields.ConvertAll(iter => iter.Type);

            foreach (FieldType type in otherTypes)
            {
                if (currentTypes.Count == 0)
                {
                    return true;
                }

                if (!currentTypes.Contains(type))
                {
                    return false;
                }

                currentTypes.Remove(type);
            }

            return true;
        }
    }
}
