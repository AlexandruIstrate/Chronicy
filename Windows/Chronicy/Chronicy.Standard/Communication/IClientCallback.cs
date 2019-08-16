#if NET472

using Chronicy.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace Chronicy.Communication
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendAvailableNotebooks(List<Notebook> message);

        [OperationContract(IsOneWay = true)]
        void SendDebugMessage(string message);

        [OperationContract(IsOneWay = true)]
        void SendErrorMessage(string message);
    }
}

#endif