using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class CustomField
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public FieldType Type { get; set; }

        [DataMember]
        public object Value { get; set; }

        public CustomField(string name, FieldType type, object value = null)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    public enum FieldType
    {
        Number,
        String
    }
}
