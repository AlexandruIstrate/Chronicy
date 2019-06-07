namespace Chronicy.Data
{
    public class CustomField
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
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
