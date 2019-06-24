using Chronicy.Data.Storage;

namespace Chronicy.Service.App
{
    public abstract class IService
    {
        public IDataSource DataSource { get; set; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
    }
}
