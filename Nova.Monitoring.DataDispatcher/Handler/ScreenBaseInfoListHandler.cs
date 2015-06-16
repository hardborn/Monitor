using Newtonsoft.Json;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public class ScreenBaseInfoListHandler : Handler
    {
        public ScreenBaseInfoListHandler(ClientDispatcher client)
            : base(client)
        {
        }

        public override void Execute(string key, object data)
        {
            if (_client == null)
                return;

            List<LedBasicInfo> obj;
            using (var stringReader = new StringReader(data as string))
            {
                obj = JsonConvert.DeserializeObject<List<LedBasicInfo>>(data as string);
            }
            GetLedList().Clear();
            foreach (var basicInfo in obj)
            {
                var led = new Led()
                {
                    SerialNumber = basicInfo.Sn,
                    Height = basicInfo.Height,
                    Width = basicInfo.Width
                };
                GetLedList().Add(led);
                var updateConfigRequest = new
                {
                    mac = AppDataConfig.CurrentMAC,
                    sn_num = basicInfo.Sn,
                    led_height = basicInfo.Height,
                    led_width = basicInfo.Width,
                    card_num = GetCardNum(basicInfo.PartInfos),
                    IsSupportPointDetect = basicInfo.IsSupportPointDetect,
                    IsSupportAutoBrightness = true,
                    PointCount = basicInfo.PointCount
                };
                RestFulClient.Instance.Post("api/index/updateConf", updateConfigRequest, null);
            }
            _client.NotifyLedBaseInfoUpdated(obj);
        }

        private List<Led> GetLedList()
        {
            return _client.LedList;
        }


        private static string GetCardNum(List<PartInfo> partsInfoList)
        {
            int senderCardNum = 0;
            int reciverCardNum = 0;
            int monitingCardNum = 0;
            foreach (var item in partsInfoList)
            {
                if (item.Type == DeviceType.ReceiverCard)
                {
                    reciverCardNum = item.Amount;
                }
                if (item.Type == DeviceType.SendCard)
                {
                    senderCardNum = item.Amount;
                }
                if (item.Type == DeviceType.MonitoringCard)
                {
                    monitingCardNum = item.Amount;
                }
            }
            return senderCardNum + "+" + reciverCardNum + "+" + monitingCardNum;
        }
    }
}
