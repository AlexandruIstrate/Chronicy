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
        #region Connection

        [OperationContract(IsOneWay = true)]
        void Connect();

        #endregion

        #region CRUD Operations

        [OperationContract(IsOneWay = false)]
        IEnumerable<Notebook> GetAll();

        [OperationContract(IsOneWay = false)]
        Notebook Get(string uuid);

        [OperationContract(IsOneWay = false)]
        void Create(Notebook notebook);

        [OperationContract(IsOneWay = false)]
        void Update(Notebook notebook);

        [OperationContract(IsOneWay = false)]
        void Delete(string uuid);

        #endregion

        #region Selected Items

        [OperationContract(IsOneWay = true)]
        void SendSelectedDataSource(DataSourceType dataSource);

        [OperationContract(IsOneWay = true)]
        void SendSelectedNotebook(Notebook notebook);

        [OperationContract(IsOneWay = true)]
        void SendSelectedStack(Stack stack);

        #endregion

        #region Information

        [OperationContract(IsOneWay = true)]
        void SendTrackingData(TrackingData data);

        [OperationContract(IsOneWay = true)]
        void SendDebugMessage(string message);

        #endregion
    }
}
