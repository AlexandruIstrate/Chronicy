namespace Chronicy.Information
{
    public class InformationDispatcher
    {
        // TODO: Maybe change?
        public static InformationDispatcher Default = new InformationDispatcher();

        public IInformationContext DefaultContext { get; }

        public void Dispatch(string messsage, InformationKind informationKind, IInformationContext context)
        {
            context.MessageDispatched(messsage, informationKind);
        }

        public void Dispatch(string messsage, InformationKind informationKind = InformationKind.Info)
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
