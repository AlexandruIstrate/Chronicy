using Chronicy.Information;
using System;
using System.Diagnostics;

namespace Chronicy.Service.Information
{
    public class EventLogContext : IInformationContext
    {
        public static EventLogContext Current { get; } = new EventLogContext();

        public EventLogContext()
        {
            eventLog = new EventLog();

            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }

            eventLog.Source = SourceName;
            eventLog.Log = LogName;
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

        public const string SourceName = "Chronicy.Service";
        public const string LogName = "Application";

        private readonly EventLog eventLog;
    }
}
