using System;

namespace Chronicy.Tracking
{
    public abstract class ITrackable
    {
        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set { enabled = value; StateUpdated?.Invoke(value); }
        }

        public object trackedValue;
        public object TrackedValue
        {
            get => trackedValue;
            set { trackedValue = value; TrackedValueUpdated?.Invoke(value); }
        }

        public abstract Type ValueType { get; }

        public delegate void StateUpdateHandler(bool enabled);
        public delegate void ValueUpdateHandler(object value);

        public event StateUpdateHandler StateUpdated;
        public event ValueUpdateHandler ValueUpdated;
        public event ValueUpdateHandler TrackedValueUpdated;

        public void TriggerUpdate(object value)
        {
            if (Enabled)
            {
                ValueUpdated?.Invoke(value);
            }
        }
    }
}
