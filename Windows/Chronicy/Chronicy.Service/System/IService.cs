using Chronicy.Data;
using Chronicy.Data.Storage;

namespace Chronicy.Service.System
{
    public abstract class IService
    {
        public IDataSource<Notebook> DataSource { get; set; }

        public virtual void OnStart() { }
        public virtual void OnPause() { }
        public virtual void OnContinue() { }
        public virtual void OnStop() { }
    }
}
