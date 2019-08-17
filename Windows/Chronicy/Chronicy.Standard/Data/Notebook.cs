using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Notebook
    {
        [DataMember]
        [JsonProperty("id")]
        public int ID { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("stacks")]
        public List<Stack> Stacks { get; set; }

        public Notebook(string name)
        {
            Name = name;
            Stacks = new List<Stack>();
        }

        public Notebook()
        {
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Notebook))
            {
                return false;
            }

            Notebook other = (Notebook)obj;

            return ID == other.ID &&
                   Name == other.Name &&
                   Stacks.SequenceEqual(other.Stacks);
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
                hash = hash * 23 + Stacks.GetHashCode();
                return hash;
            }
        }
    }
}
