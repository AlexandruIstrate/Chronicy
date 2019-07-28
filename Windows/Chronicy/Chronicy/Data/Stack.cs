using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Stack
    {
        [DataMember]
        [JsonProperty("id")]
        public int ID { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("fields")]
        public List<CustomField> Fields { get; set; }

        [DataMember]
        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }

        public Stack(string name)
        {
            Name = name;
            Fields = new List<CustomField>();
            Cards = new List<Card>();
        }

        public Stack()
        {
            Name = string.Empty;
            Fields = new List<CustomField>();
            Cards = new List<Card>();
        }

        public bool IsCompatible(FieldTemplate template)
        {
            FieldTemplate current = new FieldTemplate(Fields);
            return current.Matches(template);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Stack))
            {
                return false;
            }

            Stack other = (Stack)obj;
            return ID == other.ID &&
                   Name == other.Name &&
                   Cards.SequenceEqual(other.Cards);
        }

        public override int GetHashCode()
        {
            // As per Jon Skeet's StackOverflow answer
            // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Cards.GetHashCode();
                return hash;
            }
        }
    }
}
