using System.Collections.Generic;

namespace Chronicy.Data.Automation
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
