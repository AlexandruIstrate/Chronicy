using System.Collections.Generic;
using System.Text;
using Chronicy.Communication;
using Chronicy.Data;
using Chronicy.Excel.Information;
using Chronicy.Information;

namespace Chronicy.Excel.Communication
{
    public class TrackedClient : IClientCallback
    {
        private IInformationContext context = new MessageBoxContext();

        public void SendAvailableNotebooks(List<Notebook> notebooks)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Notebook item in notebooks)
            {
                builder.AppendLine(item.Name);
            }

            // Debug only
            InformationDispatcher.Default.Dispatch(builder.ToString(), context);
        }

        public void SendDebugMessage(string message)
        {
            InformationDispatcher.Default.Dispatch(message, DebugLogContext.Default);
        }

        public void SendErrorMessage(string message)
        {
            InformationDispatcher.Default.Dispatch("The service encountered an error!\n" + message, context, InformationKind.Error);
        }
    }
}
