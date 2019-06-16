namespace Chronicy.Tracking
{
    public abstract class ITrackable<T>
    {
        public bool Enabled { get; set; }

        public delegate void ValueUpdateHandler(T value);
        public event ValueUpdateHandler ValueUpdated;

        public void TriggerUpdate(T value)
        {
            if (Enabled)
            {
                ValueUpdated?.Invoke(value);
            }
        }
    }
}
