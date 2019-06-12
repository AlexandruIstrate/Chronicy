using System.ServiceModel;

namespace Chronicy.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientCallback))]
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void Connect();
    }
}
