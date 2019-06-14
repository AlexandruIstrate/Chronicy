﻿using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Card
    {
        public string Name { get; set; }
        public string Comment { get; set; }
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