namespace Chronicy.Information
{
    public interface IInformationContext
    {
        void MessageDispatched(string message, InformationKind informationKind);
    }
}
