using System.ServiceModel;

namespace Chronicy.Communication
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnTestInfo(string info);
    }
}
