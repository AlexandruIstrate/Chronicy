using System;
using System.Collections.Generic;

namespace Chronicy.Excel.UI.Ribbon
{
    public class Section : ISection
    {
        public Dictionary<string, Action> Actions { get; }

        public Section()
        {
            Actions = new Dictionary<string, Action>();
        }

        public void ActivateItem(string id)
        {
            try
            {
                Actions[id].Invoke();
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Cannot find a component with the given ID", nameof(id));
            }
        }
    }
}
