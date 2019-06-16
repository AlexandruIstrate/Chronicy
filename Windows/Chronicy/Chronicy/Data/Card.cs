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

        [DataMember]
        public List<Tag> Tags { get; set; }

        public Card(string name, string comment = "")
        {
            Name = name;
            Comment = comment;
            Fields = new List<CustomField>();
            Tags = new List<Tag>();
        }

        public void AddField(CustomField field)
        {
            Fields.Add(field);
        }

        public void AddFields(IEnumerable<CustomField> fields)
        {
            Fields.AddRange(fields);
        }

        public void RemoveField(CustomField field)
        {
            Fields.Remove(field);
        }

        public void AddTag(Tag tag)
        {
            Tags.Add(tag);
        }

        public void AddTags(IEnumerable<Tag> tags)
        {
            Tags.AddRange(tags);
        }

        public void RemoveTag(Tag tag)
        {
            Tags.Remove(tag);
        }
    }
}
