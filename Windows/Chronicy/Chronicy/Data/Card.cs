using System;
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
        public DateTime Date { get; set; }

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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Card))
            {
                return false;
            }

            Card other = (Card)obj;
            return Name == other.Name &&
                   Comment == other.Comment &&
                   Date == other.Date &&
                   Fields == other.Fields &&
                   Tags == other.Tags;
        }

        public override int GetHashCode()
        {
            // As per Jon Skeet's StackOverflow answer
            // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Comment.GetHashCode();
                hash = hash * 23 + Date.GetHashCode();
                hash = hash * 23 + Fields.GetHashCode();
                hash = hash * 23 + Tags.GetHashCode();
                return hash;
            }
        }
    }
}
