using System;
using System.Collections.Generic;

namespace Chronicy.Data.Automation
{
    public class AutomationAction
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public Action<AutomationAction> Action { get; set; }

        public List<ITrigger> Triggers { get; }

        public AutomationAction(Action<AutomationAction> action)
        {
            Action = action;
            Triggers = new List<ITrigger>();
        }

        public AutomationAction()
        {
            Action = null;
            Triggers = new List<ITrigger>();
        }

        public void RegisterTrigger(ITrigger trigger)
        {
            trigger.SetTriggerAction<AutomationAction>((item) =>
            {
                Action?.Invoke(this);
            });
        }

        public void RemoveTrigger(ITrigger trigger)
        {
            trigger.RemoveTriggerAction<AutomationAction>();
        }
    }
}
