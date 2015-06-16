using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace Nova.Monitoring.Common
{
    [ServiceContract(Name = "WebDataService", Namespace = "http://service.monitoring.com/web", SessionMode = SessionMode.NotAllowed)]
    public interface IWebDataService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetData?id={identity}")]
        DataPoint GetData(string identity);

        [OperationContract]
        DataPoint[] GetDataArray(string[] identity);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllData", ResponseFormat = WebMessageFormat.Json)]
        DataPoint[] GetAllData();

        [OperationContract]
        [WebGet(UriTemplate = "View?mode={mode}")]
        Stream ViewData(string mode);

        [OperationContract(IsOneWay = true)]
        void SendCommand(Command command);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(UriTemplate = "SendData?id={identity}")]
        void SendData(string identity, object data);
    }
}
