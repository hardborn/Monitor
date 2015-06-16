using Newtonsoft.Json;
using Nova.Monitoring.Common;
using Nova.Monitoring.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public class SpotInspectionResultHandler : Handler
    {
        public SpotInspectionResultHandler(ClientDispatcher client)
            : base(client)
        {

        }

        public override void Execute(string key, object data)
        {
            if (_client == null)
                return;
            SpotInspectionResult spotInspectionResult;
            using (var stringReader = new StringReader(data as string))
            {
                spotInspectionResult = JsonConvert.DeserializeObject<SpotInspectionResult>(data as string);
                spotInspectionResult.Id = AppDataConfig.CurrentMAC + "+" + spotInspectionResult.Id;
                spotInspectionResult.Timestamp = SystemHelper.GetUtcTicksByDateTime(DateTime.Now).ToString();
            }
            RestFulClient.Instance.Post("api/SpotCheck/index", spotInspectionResult, new Action<IRestResponse>((response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    switch (response.Content)
                    {
                        case "5":
                            break;
                        case "13":
                            break;
                        case "14":
                            break;
                    }
                }
            }));
        }
    }
}
