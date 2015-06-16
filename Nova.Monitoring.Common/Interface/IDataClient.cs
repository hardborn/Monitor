using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Nova.Monitoring.Common
{
    [ServiceContract]
    [ServiceKnownType(typeof(int[]))]
    [ServiceKnownType(typeof(string[]))]
    [ServiceKnownType(typeof(double[]))]
    [ServiceKnownType(typeof(DataPoint))]
    [ServiceKnownType(typeof(List<DataPoint>))]
    public interface IDataClient
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveData(string identity, object data);
        [OperationContract(IsOneWay = true)]
        void ReceiveCompositeData(DataPoint data);
        [OperationContract(IsOneWay = true)]
        void ReceiveCommand(Command command);
    }
}
