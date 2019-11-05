using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class CustomField
    {
        [DataMember]
        [JsonProperty("id")]
        public int ID { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("type")]
        public FieldType Type { get; set; }

        [DataMember]
        [NotMapped] // We cannot store objects in the database
        [JsonProperty("value")]
        public object Value { get; set; }

        public string SerializedValue
        {
            get => JsonConvert.SerializeObject(Value);
            set => Value = JsonConvert.DeserializeObject(value ?? string.Empty);
        }

        public CustomField(string name, FieldType type, object value = null)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public CustomField()
        {
            Name = string.Empty;
            Type = FieldType.String;
            Value = null;
            SerializedValue = null;
        }

        public Type GetSystemType()
        {
            return FieldTypeToSystemType(Type);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is CustomField))
            {
                return false;
            }

            CustomField other = (CustomField)obj;

            return ID == other.ID &&
                   Name == other.Name &&
                   Type == other.Type &&
                   object.Equals(Value, other.Value);
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
                hash = hash * 23 + Type.GetHashCode();
                hash = hash * 23 + Value.GetHashCode();
                return hash;
            }
        }

        public static Type FieldTypeToSystemType(FieldType type)
        {
            switch (type)
            {
                case FieldType.Number:
                    return typeof(float);
                case FieldType.String:
                    return typeof(string);
            }

            return typeof(object);
        }
    }

    public enum FieldType
    {
        Number,
        String
    }
}
