using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public Tag(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public Tag()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Tag))
            {
                return false;
            }

            Tag other = (Tag)obj;
            return ID == other.ID &&
                   Name == other.Name &&
                   Description == other.Description;
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
                hash = hash * 23 + Description.GetHashCode();
                return hash;
            }
        }
    }
}
