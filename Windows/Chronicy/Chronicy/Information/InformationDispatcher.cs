using System;
using System.Text;

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

        public void Dispatch(Exception e, IInformationContext context, ExceptionDispatch exceptionDispatch = ExceptionDispatch.Message)
        {
            StringBuilder builder = new StringBuilder();

            switch (exceptionDispatch)
            {
                case ExceptionDispatch.Message:
                    builder.Append(e.Message);
                    break;

                case ExceptionDispatch.Trace:
                    builder.Append(e.StackTrace);
                    break;

                case ExceptionDispatch.Full:
                    builder.AppendLine(e.Message);
                    builder.Append(e.StackTrace);
                    break;
            }

            Dispatch(builder.ToString(), context, InformationKind.Error);
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

    public enum ExceptionDispatch
    {
        Message, Trace, Full
    }
}
