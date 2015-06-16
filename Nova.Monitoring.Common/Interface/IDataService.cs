using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Nova.Monitoring.Common
{
    [ServiceContract(Name = "DataService", Namespace = "http://service.monitoring.com/data", SessionMode = SessionMode.Required, CallbackContract = typeof(IDataClient))]//, CallbackContract = typeof(IDataClient)
    [ServiceKnownType(typeof(int[]))]
    [ServiceKnownType(typeof(string[]))]
    [ServiceKnownType(typeof(double[]))]
    [ServiceKnownType(typeof(DataPoint))]
    [ServiceKnownType(typeof(List<DataPoint>))]
    public interface IDataService
    {
        [OperationContract(IsOneWay = true, IsTerminating = false)]
        void Register(string[] keys);
        [OperationContract(IsOneWay = true)]
        void Login(string name);

        [OperationContract(IsOneWay = true, IsTerminating = false)]
        void Logout();

        [OperationContract(IsOneWay = true)]
        void SendCommand(Command command);

        [OperationContract(IsOneWay = true, IsTerminating = false)]
        void SendData(string key, object data);

        [OperationContract(IsOneWay = true, IsTerminating = false)]
        void SendCompositeData(DataPoint dp);

        [OperationContract(IsOneWay = false)]
        bool Hello();

    }


    [ServiceContract]
    public interface IPingService
    {
        [OperationContract]
        bool Ping();
    }
}
