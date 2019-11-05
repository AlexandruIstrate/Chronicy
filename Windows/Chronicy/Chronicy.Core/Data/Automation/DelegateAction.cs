using System;
using System.Threading.Tasks;

namespace Chronicy.Data.Automation
{
    public class DelegateAction : ITriggerAction
    {
        public Action<DelegateAction> TriggerAction { get; set; }

        public DelegateAction(Action<DelegateAction> action)
        {
            TriggerAction = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Run()
        {
            if (TriggerAction == null)
            {
                throw new InvalidOperationException($"{ nameof(TriggerAction) } must not be null");
            }

            TriggerAction(this);
        }

        public Task RunAsync()
        {
            return Task.Run(() => Run());
        }
    }
}
