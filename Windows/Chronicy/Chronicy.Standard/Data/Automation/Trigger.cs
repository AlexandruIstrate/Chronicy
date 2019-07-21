using System;
using System.Collections.Generic;

namespace Chronicy.Standard.Data.Automation
{
    public class Trigger : ITrigger
    {
        private Dictionary<Type, Action<ITrigger>> actions;

        public Trigger()
        {
            actions = new Dictionary<Type, Action<ITrigger>>();
        }

        public void RemoveTriggerAction<T>() where T : class
        {
            actions.Remove(typeof(T));
        }

        public void SetTriggerAction<T>(Action<ITrigger> action) where T : class
        {
            actions[typeof(T)] = action;
        }

        public void Fire()
        {
            foreach (Action<ITrigger> action in actions.Values)
            {
                action?.Invoke(this);
            }
        }
    }
}
