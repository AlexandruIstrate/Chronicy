using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Notebook
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Stack> Stacks { get; set; }

        public Notebook(string name)
        {
            Name = name;
            Stacks = new List<Stack>();
        }

        public void Add(Stack stack)
        {
            Stacks.Add(stack);
        }

        public void Add(IEnumerable<Stack> stacks)
        {
            Stacks.AddRange(stacks);
        }

        public void Remove(Stack stack)
        {
            Stacks.Remove(stack);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Notebook))
            {
                return false;
            }

            Notebook other = (Notebook)obj;
            return Id == other.Id &&
                   Name == other.Name &&
                   Stacks == other.Stacks;
        }

        public override int GetHashCode()
        {
            // As per Jon Skeet's StackOverflow answer
            // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Stacks.GetHashCode();
                return hash;
            }
        }
    }
}
