using Chronicy.Information;
using System;
using System.Collections.Generic;

namespace Chronicy.Excel.Tracking
{
    public class TrackingSystem
    {
        private List<TrackingEvent> recordedEvents;
        private Dictionary<Type, PostedEventHandler> eventHandlers;

        public delegate void PostedEventHandler(TrackingEvent trackingEvent);

        public TrackingSystem()
        {
            recordedEvents = new List<TrackingEvent>();
            eventHandlers = new Dictionary<Type, PostedEventHandler>();
        }

        public void Post<T>(TrackingEvent trackingEvent)
        {
            if (trackingEvent.ValueType != typeof(T))
            {
                throw new ArgumentException("The posted type must match the event type", nameof(trackingEvent));
            }

            recordedEvents.Add(trackingEvent);

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
                InformationDispatcher.Default.Dispatch($"No registered handler for the event with type { trackingEvent.ValueType.Name }", DebugLogContext.Default, InformationKind.Warning);
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
