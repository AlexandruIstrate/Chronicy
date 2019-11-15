using System;

namespace Chronicy.Data.Automation
{
    public interface ITrigger
    {
        void SetTriggerAction<T>(Action<ITrigger> action) where T : class;
        void RemoveTriggerAction<T>() where T : class;

        void Fire();
    }
}
