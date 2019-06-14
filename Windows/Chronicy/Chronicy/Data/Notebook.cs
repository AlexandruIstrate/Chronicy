using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Notebook
    {
        public long Id { get; set; }
        public string Name { get; set; }
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
