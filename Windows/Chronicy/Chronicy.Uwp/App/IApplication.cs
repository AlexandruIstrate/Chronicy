using Chronicy.Communication;
namespace Chronicy.Uwp.App
{
    public abstract class IApplication
    {
        public IServerService Service { get; set; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
    }
}
