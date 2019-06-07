using System.Collections.Generic;

namespace Chronicy.Data
{
    public class Notebook
    {
        public string Name { get; set; }
        public List<Stack> Stacks { get; }

        public Notebook(string name)
        {
            Name = name;
            Stacks = new List<Stack>();
        }

        public void Add(Stack stack)
        {
            Stacks.Add(stack);
        }

        public void Remove(Stack stack)
        {
            Stacks.Remove(stack);
        }
    }
}
