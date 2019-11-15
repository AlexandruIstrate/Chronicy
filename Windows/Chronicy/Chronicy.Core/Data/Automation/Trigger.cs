using System;
using System.Collections.Generic;

namespace Chronicy.Data.Automation
{
    public class Trigger : ITrigger
    {
        private readonly Dictionary<Type, Action<ITrigger>> actions;

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
            actions[typeof(T)] = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Fire()
        {
            foreach (Action<ITrigger> action in actions.Values)
            {
                action.Invoke(this);
            }
        }
    }
}
