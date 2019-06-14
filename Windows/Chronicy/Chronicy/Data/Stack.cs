using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Stack
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }

        public Stack(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public void Add(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);
        }

        public void Remove(Card card)
        {
            Cards.Remove(card);
        }
    }
}
