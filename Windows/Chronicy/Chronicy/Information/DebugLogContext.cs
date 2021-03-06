﻿using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Chronicy.Information
{
    /// <summary>
    /// Represents a log information context.
    /// </summary>
    public class DebugLogContext : IInformationContext
    {
        public static DebugLogContext Current { get; } = new DebugLogContext();

        private DebugLogContext()
        {
            InitializeNLog();
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            LogManager.GetCurrentClassLogger().Log(InformationKindToLogLevel(informationKind), message);
        }

        public void ExceptionDispatched(Exception exception)
        {
            LogManager.GetCurrentClassLogger().Fatal(exception);
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
                case InformationKind.Debug:
                    return LogLevel.Debug;
            }

            return LogLevel.Info;
        }
    }
}
