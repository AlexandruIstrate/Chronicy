using Chronicy.Service.App;
using System;
using Topshelf;

namespace Chronicy.Service
{
    /// <summary>
    /// Represents the running program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        public static void Main()
        {
            ConfigureTopshelf();
        }

        /// <summary>
        /// Configures the Topshelf framework.
        /// </summary>
        private static void ConfigureTopshelf()
        {
            TopshelfExitCode exitCode = HostFactory.Run(x =>                                   
            {
                x.Service<IService>(s =>                                   
                {
                    s.ConstructUsing(name => new AppService());              
                    s.WhenStarted(tc => tc.OnStart());                        
                    s.WhenStopped(tc => tc.OnStop());                         
                });

                x.StartAutomatically();
                x.RunAsLocalSystem();

                x.EnableServiceRecovery(r =>
                {
                    // Try a restart after an interval
                    r.RestartService(RestartDelayInMinutes);

                    // Only recover after a crash
                    r.OnCrashOnly();
                });

                x.SetDescription(Description);
                x.SetDisplayName(DisplayName);
                x.SetServiceName(Name);
            });                                                             

            Environment.ExitCode = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
        }

        private const string Description = "Gathers data from supported applications";
        private const string DisplayName = "Chronicy Tracking Service";
        private const string Name = "Chronicy.Service";

        private const int RestartDelayInMinutes = 5;
    }
}
