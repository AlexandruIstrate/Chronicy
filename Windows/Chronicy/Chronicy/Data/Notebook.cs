using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Chronicy.Data
{
    [DataContract]
    public class Notebook
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Uuid { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Stack> Stacks { get; set; }

        public Notebook(string name)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Stacks = new List<Stack>();
        }

        public Notebook()
        {
            Uuid = Guid.NewGuid().ToString();
            Name = string.Empty;
            Stacks = new List<Stack>();
        }

        public void Add(Stack stack)
        {
            Stacks.Add(stack);
        }

        public void Add(IEnumerable<Stack> stacks)
        {
            foreach (Stack stack in stacks)
            {
                Stacks.Add(stack);
            }

            //Stacks.AddRange(stacks);
        }

        public void Remove(Stack stack)
        {
            Stacks.Remove(stack);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(nameof(Notebook)).Append(" {");
            builder.Append("Uuid = ").Append(Uuid).Append(", ");
            builder.Append("Name = ").Append(Name).Append(", ");
            builder.Append("Stacks = ").Append(string.Join(", ", Stacks.ConvertAll((item) => item.ToString())));
            builder.Append(" }");
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Notebook))
            {
                return false;
            }

            Notebook other = (Notebook)obj;
            return Uuid == other.Uuid &&
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
                hash = hash * 23 + Uuid.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Stacks.GetHashCode();
                return hash;
            }
        }
    }
}
