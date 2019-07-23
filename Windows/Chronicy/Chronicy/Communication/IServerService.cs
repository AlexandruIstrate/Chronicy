using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Tracking;
using System.Collections.Generic;
using System.ServiceModel;

namespace Chronicy.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientCallback))]
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void Connect();

        [OperationContract(IsOneWay = true)]
        void SendUrl(string url);

        [OperationContract(IsOneWay = true)]
        void Authenticate(string username, string password);


        [OperationContract(IsOneWay = false)]
        IEnumerable<Notebook> GetAll();

        [OperationContract(IsOneWay = false)]
        Notebook Get(int id);

        [OperationContract(IsOneWay = false)]
        void Create(Notebook notebook);

        [OperationContract(IsOneWay = false)]
        void Update(Notebook notebook);

        [OperationContract(IsOneWay = false)]
        void Delete(int id);


        [OperationContract(IsOneWay = true)]
        void SendSelectedDataSource(DataSourceType dataSource);

        [OperationContract(IsOneWay = true)]
        void SendSelectedNotebook(Notebook notebook);

        [OperationContract(IsOneWay = true)]
        void SendSelectedStack(Stack stack);


        [OperationContract(IsOneWay = true)]
        void SendTrackingData(TrackingData data);

        [OperationContract(IsOneWay = true)]
        void SendDebugMessage(string message);
    }
}
