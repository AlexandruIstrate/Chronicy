using System.Threading.Tasks;

namespace Chronicy.Data.Actions
{
    public interface ITriggerAction
    {
        void Run();
        Task RunAsync();
    }
}
