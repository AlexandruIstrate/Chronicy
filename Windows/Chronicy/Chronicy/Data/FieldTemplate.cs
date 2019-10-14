using System.Collections.Generic;
using System.Linq;

namespace Chronicy.Data
{
    /// <summary>
    /// Represents a set of fields.
    /// </summary>
    public class FieldTemplate
    {
        /// <summary>
        /// Gets the list of field this template contains.
        /// </summary>
        public List<CustomField> Fields { get; }

        /// <summary>
        /// Initializes a new template from the given <see cref="System.Collections.Generic.IEnumerable{T}"/>.
        /// </summary>
        /// <param name="fields">The fields to use for initialization</param>
        public FieldTemplate(IEnumerable<CustomField> fields)
        {
            Fields = new List<CustomField>(fields);
        }

        /// <summary>
        /// Initializes a new template from the given array of fields.
        /// </summary>
        /// <param name="fields">The fields to use for initialization</param>
        public FieldTemplate(params CustomField[] fields)
        {
            Fields = new List<CustomField>(fields);
        }

        /// <summary>
        /// Checks if this template matches another one.
        /// </summary>
        /// <param name="other">The other template to check for</param>
        /// <returns>True if the two templates match, false otherwise</returns>
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
