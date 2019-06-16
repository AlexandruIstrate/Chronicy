using Chronicy.Excel.Information;
using Chronicy.Information;
using Microsoft.Office.Tools.Excel;
using System;

namespace Chronicy.Excel.User
{
    public class SelectSheetInteraction : IUserInteraction<Worksheet>
    {
        private StatusBarContext statusBarContext = new StatusBarContext();
        public IInformationContext InformationContext => statusBarContext;

        public void Prompt()
        {
            throw new NotImplementedException();
        }

        public Worksheet Run()
        {
            throw new NotImplementedException();
        }
    }
}
