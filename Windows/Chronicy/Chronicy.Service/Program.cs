using Chronicy.Information;
using Chronicy.Service.Information;
using System;
using System.ServiceProcess;

namespace Chronicy.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            try
            {
                Run();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e.Message, new EventLogContext(), InformationKind.Error);
            }
        }

        private static void Run()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
