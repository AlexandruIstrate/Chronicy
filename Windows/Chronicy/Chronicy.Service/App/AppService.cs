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

            InformationDispatcher.Default.Dispatch("OnStart", InformationKind.Info, this);
        }

        public override void OnStop()
        {
            InformationDispatcher.Default.Dispatch("OnStop", InformationKind.Info, this);
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
            ServiceBootstrapper bootstrapper = new ServiceBootstrapper();
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
}
