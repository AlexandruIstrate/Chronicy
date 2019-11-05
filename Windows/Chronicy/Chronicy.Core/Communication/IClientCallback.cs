using Chronicy.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace Chronicy.Communication
{
    /// <summary>
    /// Represents operations that can be called back on the client.
    /// </summary>
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
