using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Card
    {
        public string Name { get; set; }
        public List<CustomField> Fields { get; }

        public Card(string name)
        {
            Name = name;
            Fields = new List<CustomField>();
        }

        public void Add(CustomField field)
        {
            Fields.Add(field);
        }

        public void Remove(CustomField field)
        {
            Fields.Remove(field);
        }
    }
}
