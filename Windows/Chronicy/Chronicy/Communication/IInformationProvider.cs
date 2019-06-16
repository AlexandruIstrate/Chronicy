using Chronicy.Information;

namespace Chronicy.Communication
{
    public interface IInformationProvider
    {
        IInformationContext InformationContext { get; }
    }
}
