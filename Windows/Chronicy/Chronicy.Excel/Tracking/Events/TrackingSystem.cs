using System;
using System.Collections.Generic;

namespace Chronicy.Excel.Tracking.Events
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
            }
        }

        public void Register<T>(PostedEventHandler eventHandler)
        {
            eventHandlers[typeof(T)] = eventHandler;
        }

        public void DiscardConsumedEvents()
        {
            recordedEvents.RemoveAll((item) => { return item.Handled; });
        }
    }
}
