using Chronicy.Communication;
using Chronicy.Excel.Communication;
using System.Diagnostics;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExtension
    {
        private IServerService service;

        public override void OnStart()
        {
            Debug.WriteLine("Extension OnStart");

            ClientBootstrapper bootstrapper = new ClientBootstrapper();
            service = bootstrapper.Start(new TrackedClient());
        }

        public override void OnShutdown()
        {
            Debug.WriteLine("Extension OnShutdown");
        }
    }
}
