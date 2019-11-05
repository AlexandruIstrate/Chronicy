using System.Threading.Tasks;

namespace Chronicy.Data.Automation
{
    public interface ITriggerAction
    {
        void Run();
        Task RunAsync();
    }
}
