using Chronicy.Data;
using System.ServiceModel;

namespace Chronicy.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientCallback))]
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void Connect();

        [OperationContract(IsOneWay = true)]
        void SendSelectedNotebook(Notebook notebook);

        [OperationContract(IsOneWay = true)]
        void SendSelectedStack(Stack stack);

        [OperationContract(IsOneWay = true)]
        void SendDebugMessage(string message);
    }
}
