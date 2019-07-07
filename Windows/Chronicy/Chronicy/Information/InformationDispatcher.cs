using System;
using System.Text;

namespace Chronicy.Information
{
    public class InformationDispatcher
    {
        // TODO: Maybe change?
        public static InformationDispatcher Default = new InformationDispatcher();

        public IInformationContext DefaultContext { get; } = DebugLogContext.Default;

        public void Dispatch(string messsage, IInformationContext context, InformationKind informationKind = InformationKind.Info)
        {
            context.MessageDispatched(messsage, informationKind);
        }

        public void Dispatch(string messsage, InformationKind informationKind = InformationKind.Info)
        {
            DefaultContext.MessageDispatched(messsage, informationKind);
        }

        public void Dispatch(Exception e, IInformationContext context)
        {
            context.ExceptionDispatched(e);
        }

        public void Dispatch(Exception e)
        {
            DefaultContext.ExceptionDispatched(e);
        }
    }

    public enum InformationKind
    {
        Info,
        Warning,
        Error,
        Debug
    }
}
