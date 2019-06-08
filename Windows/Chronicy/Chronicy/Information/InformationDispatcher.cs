namespace Chronicy.Information
{
    public class InformationDispatcher
    {
        public IInformationContext DefaultContext { get; }

        public void Dispatch(string messsage, InformationKind informationKind, IInformationContext context)
        {
            context.MessageDispatched(messsage, informationKind);
        }

        public void Dispatch(string messsage, InformationKind informationKind)
        {
            Dispatch(messsage, informationKind, DefaultContext);
        }
    }

    public enum InformationKind
    {
        Info,
        Warning,
        Error
    }
}
