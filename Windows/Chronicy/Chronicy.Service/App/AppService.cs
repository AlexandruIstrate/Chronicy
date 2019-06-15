using Chronicy.Information;
using Chronicy.Service.Communication;
using Chronicy.Service.Information;

namespace Chronicy.Service.App
{
    public class AppService : IService
    {
        private EventLogContext logContext = new EventLogContext();

        public override void OnStart()
        {
            InitializeCommunication();
            InformationDispatcher.Default.Dispatch("OnStart", logContext);
        }

        public override void OnStop()
        {
            InformationDispatcher.Default.Dispatch("OnStop", logContext) ;
        }

        private void InitializeCommunication()
        {
            ServerConnection bootstrapper = new ServerConnection();
            bootstrapper.Start();
        }
    }
}
