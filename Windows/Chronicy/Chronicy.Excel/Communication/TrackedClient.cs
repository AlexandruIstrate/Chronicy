using System;
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

            InformationDispatcher.Default.Dispatch(builder.ToString());
        }

        public void SendDebugMessage(string message)
        {
            // TODO: Dispatch this on a debug context
            InformationDispatcher.Default.Dispatch(message, context);
        }
    }
}
