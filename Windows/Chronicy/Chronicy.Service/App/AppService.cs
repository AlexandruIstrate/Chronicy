using Chronicy.Information;
using Chronicy.Service.Communication;
using Chronicy.Service.Information;

namespace Chronicy.Service.App
{
    public class AppService : IService
    {
        private EventLogContext logContext;

        public AppService()
        {
            logContext = EventLogContext.Current;
        }

        public override void OnStart()
        {
            InformationDispatcher.Default.Dispatch("Service started", logContext);
            InitializeCommunication();
        }

        public override void OnStop()
        {
            InformationDispatcher.Default.Dispatch("Service stopped", logContext);
        }

        private void InitializeCommunication()
        {
            ServerConnection bootstrapper = new ServerConnection();
            bootstrapper.Start();
        }
    }
}
