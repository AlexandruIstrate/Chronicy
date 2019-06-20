using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            }
            catch (KeyNotFoundException e)
            {
                // If we get here, it means we don't have a handler registered for this type
                // This is fine, however. That's why we can just log the exception and ignore it
                Debug.WriteLine(e.Message);
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
