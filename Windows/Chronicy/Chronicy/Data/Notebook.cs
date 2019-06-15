using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chronicy.Data
{
    [DataContract]
    public class Notebook
    {
        [DataMember]
        public long Id { get; set; }

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
    }
}
