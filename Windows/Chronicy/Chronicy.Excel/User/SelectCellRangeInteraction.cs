using Chronicy.Excel.Information;
using Chronicy.Information;
using Microsoft.Office.Interop.Excel;
using System;

namespace Chronicy.Excel.User
{
    public class SelectCellRangeInteraction : IUserInteraction<Range>
    {
        private StatusBarContext statusBarContext = new StatusBarContext();
        public IInformationContext InformationContext => statusBarContext;

        public void Prompt()
        {
            throw new NotImplementedException();
        }

        public Range Run()
        {
            return Globals.ThisAddIn.Application.ActiveCell;
        }
    }
}
