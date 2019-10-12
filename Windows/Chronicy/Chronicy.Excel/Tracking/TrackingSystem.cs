using Chronicy.Information;
using System;
using System.Collections.Generic;

namespace Chronicy.Excel.Tracking
{
    /// <summary>
    /// Handles posting and listening for events.
    /// </summary>
    public class TrackingSystem
    {
        private List<TrackingEvent> recordedEvents;
        private Dictionary<Type, PostedEventHandler> eventHandlers;

        public delegate void PostedEventHandler(TrackingEvent trackingEvent);

        public bool Enabled { get; set; }

        public TrackingSystem()
        {
            recordedEvents = new List<TrackingEvent>();
            eventHandlers = new Dictionary<Type, PostedEventHandler>();
            Enabled = true;
        }

        public void Post<T>(TrackingEvent trackingEvent)
        {
            if (trackingEvent.ValueType != typeof(T))
            {
                throw new ArgumentException("The posted type must match the event type", nameof(trackingEvent));
            }

            recordedEvents.Add(trackingEvent);

            if (!Enabled)
            {
                return;
            }

            try
            {
                PostedEventHandler handler = eventHandlers[typeof(T)];
                handler.Invoke(trackingEvent);
                trackingEvent.Handle();
            }
            catch (KeyNotFoundException)
            {
                // If we get here, it means we don't have a handler registered for this type.
                // This is fine, however.
                InformationDispatcher.Default.Dispatch($"No registered handler for the event with type { trackingEvent.ValueType.Name }", DebugLogContext.Current, InformationKind.Warning);
            }
        }

        public void Register<T>(PostedEventHandler eventHandler)
        {
            eventHandlers[typeof(T)] = eventHandler;
        }

        public void DiscardConsumedEvents()
        {
            recordedEvents.RemoveAll((item) => item.Handled);
        }
    }
}
