using System.Collections.Generic;

namespace Chronicy.Information
{
    public class AgregateContext : IInformationContext
    {
        public List<IInformationContext> Contexts { get; }

        public AgregateContext(List<IInformationContext> contexts = null)
        {
            Contexts = contexts ?? new List<IInformationContext>();
        }

        public void MessageDispatched(string message, InformationKind informationKind)
        {
            foreach (IInformationContext context in Contexts)
            {
                InformationDispatcher.Default.Dispatch(message, context, informationKind);
            }
        }

        public static AgregateContext Of(params IInformationContext[] contexts)
        {
            return new AgregateContext(new List<IInformationContext>(contexts));
        }
    }
}
