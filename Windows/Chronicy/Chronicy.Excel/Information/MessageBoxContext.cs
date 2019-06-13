using Chronicy.Information;
using System.Windows.Forms;

namespace Chronicy.Excel.Information
{
    public class MessageBoxContext : IInformationContext
    {
        public void MessageDispatched(string message, InformationKind informationKind)
        {
            MessageBox.Show(message, informationKind.ToString(), MessageBoxButtons.OK, InformationKindToMessageBoxIcon(informationKind));
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
                default:
                    return MessageBoxIcon.Information;
            }
        }
    }
}
