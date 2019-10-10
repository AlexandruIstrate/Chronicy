using Chronicy.Tracking;
using System;
using System.Collections.Generic;

namespace Chronicy.Excel.Tracking
{
    /// <summary>
    /// Handles the registration of tracking objects.
    /// </summary>
    public class ExcelTracker
    {
        public List<ITrackable> Trackers { get; }

        public ExcelTracker()
        {
            Trackers = new List<ITrackable>();
        }

        public void Register<T>(ITrackable trackable)
        {
            if (trackable.ValueType != typeof(T))
            {
                throw new ArgumentException("The ValueType property of the argument must be the same as the generic type", nameof(trackable));
            }

            Trackers.Add(trackable);
        }

        public ITrackable Get<T>()
        {
            foreach (ITrackable trackable in Trackers)
            {
                if (trackable.ValueType == typeof(T))
                {
                    return trackable;
                }
            }

            return null;
        }

        public ITrackable GetOrRegister<T>(ITrackable trackable)
        {
            ITrackable result = Get<T>();

            if (result == null)
            {
                Register<T>(trackable);
                result = trackable;
            }

            return result;
        }

        public bool HasTrackable<T>()
        {
            foreach (ITrackable trackable in Trackers)
            {
                if (trackable.ValueType == typeof(T))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
