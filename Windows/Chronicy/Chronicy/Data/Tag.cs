using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public Tag(string name, string description = "")
        {
            Name = name;
            Description = description;
        }
    }
}
