using System;

namespace Chronicy.Excel.Tracking
{
    /// <summary>
    /// Contains data about a tracking update.
    /// </summary>
    public class TrackingEvent
    {
        public Type ValueType { get; set; }
        public object Value { get; }

        public DateTime TriggerDate { get; }

        public bool Handled { get; private set; }

        private TrackingEvent(object value, Type type)
        {
            Value = value;
            ValueType = type;
            TriggerDate = DateTime.Now;
        }

        public void Handle()
        {
            Handled = true;
        }

        public static TrackingEvent Create<T>(T value)
        {
            return new TrackingEvent(value, typeof(T));
        }
    }
}
