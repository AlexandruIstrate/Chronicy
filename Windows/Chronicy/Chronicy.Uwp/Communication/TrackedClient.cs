using Chronicy.Communication;
using Chronicy.Data;
using System;
using System.Collections.Generic;

namespace Chronicy.Uwp.Communication
{
    public class TrackedClient : IClientCallback
    {
        public void SendAvailableNotebooks(List<Notebook> message)
        {
            //throw new NotImplementedException();
        }

        public void SendDebugMessage(string message)
        {
            //throw new NotImplementedException();
        }

        public void SendErrorMessage(string message)
        {
            //throw new NotImplementedException();
        }
    }
}
