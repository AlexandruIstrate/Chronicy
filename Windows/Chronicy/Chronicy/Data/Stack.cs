using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Stack
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
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
            return Name == other.Name &&
                   Cards == other.Cards;
        }

        public override int GetHashCode()
        {
            // As per Jon Skeet's StackOverflow answer
            // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Cards.GetHashCode();
                return hash;
            }
        }
    }
}
