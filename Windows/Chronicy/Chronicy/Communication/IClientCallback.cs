using System.Collections.Generic;
using System.ServiceModel;

namespace Chronicy.Communication
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendAvailableNotebooks(List<string> notebooks);
    }
}
