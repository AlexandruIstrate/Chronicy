using Chronicy.Information;
using Chronicy.Service.Communication;
using System.Diagnostics;

namespace Chronicy.Service.App
{
    public class AppService : IService, IInformationContext
    {
        private EventLog eventLog;

        public override void OnStart()
        {
            InitializeEventLog();
            InitializeCommunication();

            InformationDispatcher.Default.Dispatch("OnStart", this);
        }

        public override void OnStop()
        {
            InformationDispatcher.Default.Dispatch("OnStop", this);
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            eventLog.WriteEntry(message, InformationKindToEventLogEntry(informationKind));
        }

        private void InitializeEventLog()
        {
            eventLog = new EventLog();

            if (!EventLog.SourceExists(EventLogConstants.SourceName))
            {
                EventLog.CreateEventSource(EventLogConstants.SourceName, EventLogConstants.LogName);
            }

            eventLog.Source = EventLogConstants.SourceName;
            eventLog.Log = EventLogConstants.LogName;
        }

        private void InitializeCommunication()
        {
            ServerConnection bootstrapper = new ServerConnection();
            bootstrapper.Start();
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
                default:
                    return EventLogEntryType.Information;
            }
        }

    }

    internal static class EventLogConstants
    {
        public const string SourceName = "ChronicyService";
        public const string LogName = "ChronicyEventLog";
    }
}
