using Newtonsoft.Json;
using Nova.Monitoring.Common;
using Nova.Monitoring.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public class BrightnessLogHandler:Handler
    {

        public BrightnessLogHandler(ClientDispatcher client)
            : base(client)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data">
        /// 0：手动
        /// 1：定时
        /// 2：自动
        /// 3：智能
        /// 4：策略
        /// </param>
        public override void Execute(string key, object data)
        {
            if (_client == null)
                return;
            BrightnessLog brightnessLog = new BrightnessLog();
            string brightnessLogStr = data as string;
            if(string.IsNullOrEmpty(brightnessLogStr))
                return;
            string[] logSplitArray = brightnessLogStr.Split(new char[] { '|' });//1306280000015920-00|AutoBright|true|0
            if (logSplitArray.Length != 4)
            {
                Debug.WriteLine(string.Format("亮度日志协议错误！"));
                return;
            }
            brightnessLog.Id = AppDataConfig.CurrentMAC + "+" + logSplitArray[0];
            //brightnessLog.OperationType = (DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]);
            if ((DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]) == DimmingMode.AutoBrightness)
            {
                brightnessLog.OperationType = DimmingMode.AutoBrightness;
            }
            else if ((DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]) == DimmingMode.FixedBrightness)
            {
                brightnessLog.OperationType = DimmingMode.FixedBrightness;
            }
            else if ((DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]) == DimmingMode.ManualBrightness)
            {
                brightnessLog.OperationType = DimmingMode.ManualBrightness;
            }
            else if ((DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]) == DimmingMode.SmartBrightness)
            {
                brightnessLog.OperationType = DimmingMode.SmartBrightness;
            }
            else if ((DimmingMode)Enum.Parse(typeof(DimmingMode), logSplitArray[1]) == DimmingMode.RuleBrightness)
            {
                brightnessLog.OperationType = DimmingMode.RuleBrightness;
            }
            brightnessLog.Result = logSplitArray[2] == "true" ? BrightnessLogResult.Success : BrightnessLogResult.Failure;
            brightnessLog.BrightnessValue = float.Parse(logSplitArray[3]);
            brightnessLog.Timestamp = SystemHelper.GetUtcTicksByDateTime(DateTime.Now).ToString();

            RestFulClient.Instance.Post("api/brightness/brightlog", brightnessLog, new Action<IRestResponse>((response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    //switch (response.Content)
                    //{
                    //    case "5":
                    //        break;
                    //    case "13":
                    //        break;
                    //    case "14":
                    //        break;
                    //}
                }
            }));
         
        }
    }
}
