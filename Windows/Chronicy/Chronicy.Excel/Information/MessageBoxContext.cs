using Chronicy.Information;
using System;
using System.Windows.Forms;

namespace Chronicy.Excel.Information
{
    public class MessageBoxContext : IInformationContext
    {
        public void MessageDispatched(string message, InformationKind informationKind)
        {
            MessageBox.Show(message, informationKind.ToString(), MessageBoxButtons.OK, InformationKindToMessageBoxIcon(informationKind));
        }

        public void ExceptionDispatched(Exception exception)
        {
            MessageBox.Show(exception.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private MessageBoxIcon InformationKindToMessageBoxIcon(InformationKind informationKind)
        {
            switch (informationKind)
            {
                case InformationKind.Info:
                    return MessageBoxIcon.Information;
                case InformationKind.Warning:
                    return MessageBoxIcon.Warning;
                case InformationKind.Error:
                    return MessageBoxIcon.Error;
                case InformationKind.Debug:
                    return MessageBoxIcon.Information;
            }

            return MessageBoxIcon.Information;
        }
    }
}
