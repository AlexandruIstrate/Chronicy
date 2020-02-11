using Chronicy.Service.App;
using System;
using Topshelf;

namespace Chronicy.Service
{
    class Program
    {
        static void Main()
        {
            StartTopshelf();
        }

        private static void StartTopshelf()
        {
            TopshelfExitCode rc = HostFactory.Run(x =>                                   
            {
                x.Service<IService>(s =>                                   
                {
                    s.ConstructUsing(name => new AppService());              
                    s.WhenStarted(tc => tc.OnStart());                        
                    s.WhenStopped(tc => tc.OnStop());                         
                });
                x.RunAsLocalSystem();                                       

                x.SetDescription("Gathers data from supported applications");                   
                x.SetDisplayName("Chronicy Tracking Service");                                  
                x.SetServiceName("Chronicy.Service");                                  
            });                                                             

            int exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
