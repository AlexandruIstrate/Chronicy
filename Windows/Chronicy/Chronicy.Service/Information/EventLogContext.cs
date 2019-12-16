using Chronicy.Information;
using System;
using System.Diagnostics;

namespace Chronicy.Service.Information
{
    public class EventLogContext : IInformationContext
    {
        private readonly EventLog eventLog;

        public static EventLogContext Current { get; } = new EventLogContext();

        public EventLogContext()
        {
            eventLog = new EventLog();

            if (!EventLog.SourceExists(EventLogConstants.SourceName))
            {
                EventLog.CreateEventSource(EventLogConstants.SourceName, EventLogConstants.LogName);
            }

            eventLog.Source = EventLogConstants.SourceName;
            eventLog.Log = EventLogConstants.LogName;
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            eventLog.WriteEntry(message, InformationKindToEventLogEntry(informationKind));
        }

        public void ExceptionDispatched(Exception exception)
        {
            eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
        }

        private EventLogEntryType InformationKindToEventLogEntry(InformationKind information)
        {
            switch (information)
            {
                case InformationKind.Info:
                    return EventLogEntryType.Information;
                case InformationKind.Warning:
                    return EventLogEntryType.Warning;
                case InformationKind.Error:
                    return EventLogEntryType.Error;
            }

            return EventLogEntryType.Information;
        }
    }

    internal static class EventLogConstants
    {
        public const string SourceName = "Chronicy.Service";
        public const string LogName = "Application";
    }
}
