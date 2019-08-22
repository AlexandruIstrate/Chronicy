using Chronicy.Communication;
using Chronicy.Uwp.Communication;

namespace Chronicy.Uwp.App
{
    public class Application : IApplication
    {
        public override void OnStart()
        {
            ClientConnection connection = new ClientConnection();
            Service = connection.Connect(new TrackedClient());
        }
    }
}
