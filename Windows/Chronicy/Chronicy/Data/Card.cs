using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public List<CustomField> Fields { get; set; }

        public Card(string name, string comment = "")
        {
            Name = name;
            Comment = comment;
            Fields = new List<CustomField>();
        }

        public void Add(CustomField field)
        {
            Fields.Add(field);
        }

        public void Add(IEnumerable<CustomField> fields)
        {
            Fields.AddRange(fields);
        }

        public void Remove(CustomField field)
        {
            Fields.Remove(field);
        }
    }
}
