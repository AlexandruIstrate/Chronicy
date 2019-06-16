using Chronicy.Information;

namespace Chronicy.Excel.User
{
    public interface IUserInteraction<out T>
    {
        IInformationContext InformationContext { get; }

        void Prompt();

        T Run();
    }
}
