using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Stack
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<Card> Cards { get; }

        public Stack(string name, string comment = "")
        {
            Name = name;
            Comment = comment;
            Cards = new List<Card>();
        }

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public void Remove(Card card)
        {
            Cards.Remove(card);
        }
    }
}
