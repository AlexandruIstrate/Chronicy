using Chronicy.Information;
using Chronicy.Service.Communication;
using Chronicy.Service.Information;

namespace Chronicy.Service.System
{
    public class AppService : IService
    {
        private EventLogContext logContext;

        public AppService()
        {
            logContext = new EventLogContext();
        }

        public override void OnStart()
        {
            InformationDispatcher.Default.Dispatch("OnStart", logContext);
            InitializeCommunication();
        }

        public override void OnStop()
        {
            InformationDispatcher.Default.Dispatch("OnStop", logContext);
        }

        private void InitializeCommunication()
        {
            ServerConnection bootstrapper = new ServerConnection();
            bootstrapper.Start();
        }
    }
}
