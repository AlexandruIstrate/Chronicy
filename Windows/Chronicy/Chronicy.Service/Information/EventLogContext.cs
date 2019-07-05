using Chronicy.Information;
using System;
using System.Diagnostics;
using System.Text;

namespace Chronicy.Service.Information
{
    public class EventLogContext : IInformationContext
    {
        private EventLog eventLog;

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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(exception.Message);
            builder.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                builder.AppendLine("Caused by");
                builder.AppendLine(exception.InnerException.Message);
                builder.AppendLine(exception.InnerException.StackTrace);
            }

            eventLog.WriteEntry(builder.ToString(), EventLogEntryType.Error);
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
        public const string SourceName = "ChronicyService";
        public const string LogName = "ChronicyEventLog";
    }
}
