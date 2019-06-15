using NLog;
using NLog.Config;
using NLog.Targets;

namespace Chronicy.Information
{
    // TODO: Singleton pattern is bad. Try using factories in the future.
    public class DebugLogContext : IInformationContext
    {
        public static DebugLogContext Default { get; } = new DebugLogContext();

        private DebugLogContext()
        {
            InitializeNLog();
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            LogManager.GetCurrentClassLogger().Log(InformationKindToLogLevel(informationKind), message);
        }

        private void InitializeNLog()
        {
            LoggingConfiguration config = new LoggingConfiguration();

            ConsoleTarget logConsole = new ConsoleTarget("ChronicyLog");
            FileTarget logFile = new FileTarget("ChronicyLog") { FileName = "Chronicy.log" };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);

            LogManager.Configuration = config;
        }

        private LogLevel InformationKindToLogLevel(InformationKind informationKind)
        {
            switch (informationKind)
            {
                case InformationKind.Info:
                    return LogLevel.Info;
                case InformationKind.Warning:
                    return LogLevel.Warn;
                case InformationKind.Error:
                    return LogLevel.Error;
            }

            return LogLevel.Info;
        }
    }
}
