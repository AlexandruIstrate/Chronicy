using Chronicy.Auth;

namespace Chronicy.Service.System
{
    public abstract class IService
    {
        public IUserSystem UserSystem { get; }

        public virtual void OnStart() { }
        public virtual void OnPause() { }
        public virtual void OnContinue() { }
        public virtual void OnStop() { }
    }
}
