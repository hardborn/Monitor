using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class Led : ICloneable
    {
        /// <summary>
        /// 组件列表
        /// </summary>
        public List<ComponentBase> Components
        {
            get;
            set;
        }

        public LedMonitoringConfig MonitoringConfig
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public string SerialNumber
        {
            get;
            set;
        }

        public string Mac
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public LedAcquisitionConfig AcquistionConfig
        {
            get;
            set;
        }

        public LedRegistationInfo RegistationInfo
        {
            get;
            set;
        }


        public LedAlarmConfig AlarmConfig
        {
            get;
            set;
        }


        public ServerResponseCode RegistTo(string server)
        {
            RestFulClient.Instance.Initialize(server);
            string response = RestFulClient.Instance.Post("api/index/register", RegistationInfo);
            int enumIndex;
            if (int.TryParse(response, out enumIndex))
            {
                return (ServerResponseCode)enumIndex;
            }
            else
            {
                return ServerResponseCode.ExceptionResult;
            }
        }



        public Led()
        {
            this.AcquistionConfig = new LedAcquisitionConfig();
            this.AlarmConfig = new LedAlarmConfig();
            this.Components = new List<ComponentBase>();
            this.Description = string.Empty;
            this.Height = -1;
            this.Width = -1;
            this.Mac = string.Empty;
            this.MonitoringConfig = new LedMonitoringConfig();
            this.RegistationInfo = new LedRegistationInfo();
            this.SerialNumber = string.Empty;
        }
        private Led(Led led)
        {
            this.AcquistionConfig =led.AcquistionConfig ==null?null:led.AcquistionConfig.Clone() as LedAcquisitionConfig;
            this.AlarmConfig = led.AlarmConfig == null? null: led.AlarmConfig.Clone() as LedAlarmConfig;
            this.Components =led.Components == null? null: led.Components.Select(t => (ComponentBase)t.Clone()).ToList() as List<ComponentBase>;
            this.Description =string.IsNullOrEmpty(led.Description)?string.Empty: led.Description.Clone() as string;
            this.Height = led.Height;
            this.Width = led.Width;
            this.Mac =string.IsNullOrEmpty(led.Mac)?string.Empty: led.Mac.Clone() as string;
            this.MonitoringConfig =led.MonitoringConfig==null?null: led.MonitoringConfig.Clone() as LedMonitoringConfig;
            this.RegistationInfo =led.RegistationInfo==null?null: led.RegistationInfo.Clone() as LedRegistationInfo;
            this.SerialNumber = string.IsNullOrEmpty(led.SerialNumber)?string.Empty:led.SerialNumber.Clone() as string;
        }


        public object Clone()
        {
            var led = new Led(this);
            return led;
        }
    }
}
