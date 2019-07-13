using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronicy.Data.Actions
{
    public class ActionManager
    {
        public List<ITriggerAction> Actions { get; }

        public ActionManager()
        {
            Actions = new List<ITriggerAction>();
        }
    }
}
