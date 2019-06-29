using System;

namespace Chronicy.Information
{
    public class InformationDispatcher
    {
        // TODO: Maybe change?
        public static InformationDispatcher Default = new InformationDispatcher();

        public IInformationContext DefaultContext { get; }

        public void Dispatch(string messsage, IInformationContext context, InformationKind informationKind = InformationKind.Info)
        {
            context.MessageDispatched(messsage, informationKind);
        }

        public void Dispatch(string messsage, InformationKind informationKind = InformationKind.Info)
        {
            Dispatch(messsage, DefaultContext, informationKind);
        }

        public void Dispatch(Exception e, IInformationContext context)
        {
            Dispatch(e.Message, context, InformationKind.Error);
        }

        public void Dispatch(Exception e)
        {
            Dispatch(e.Message, DefaultContext, InformationKind.Error);
        }
    }

    public enum InformationKind
    {
        Info,
        Warning,
        Error
    }
}
