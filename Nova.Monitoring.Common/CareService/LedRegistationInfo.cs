using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{

    public class LedRegistationRequest
    {
        public string User { get; set; }
        public string Mac { get; set; }

        public bool IsReregistering { get; set; }
        public List<LedRegistationInfo> RegistationInfos { get; set; }
    }

    public class LedRegistationResponse
    {
        public int Result { get; set; }
        public int IsReregistering { get; set; }
    }
    public class LedRegistationInfo
    {
        public string sn_num { get; set; }
        public string led_name { get; set; }
        public double led_width { get; set; }
        public double led_height { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string card_num { get; set; }
        public string mac { get; set; }
        public string UserID { get; set; }
        public bool IsReregistering { get; set; }
        public ControlSystemType ControlSystem { get; set; }
       
        public LedRegistationInfo() { }

        private LedRegistationInfo(LedRegistationInfo info)
        {
            sn_num =string.IsNullOrEmpty(info.sn_num)?string.Empty: info.sn_num.Clone() as string;
            led_name = string.IsNullOrEmpty(info.led_name) ? string.Empty : info.led_name.Clone() as string;
            led_width = info.led_width;
            led_height = info.led_height;
            Latitude = info.Latitude;
            Longitude = info.Longitude;
            card_num = string.IsNullOrEmpty(info.card_num) ? string.Empty : info.card_num.Clone() as string;
            mac = string.IsNullOrEmpty(info.mac) ? string.Empty : info.mac.Clone() as string;
            UserID = string.IsNullOrEmpty(info.UserID) ? string.Empty : info.UserID.Clone() as string;
            IsReregistering = info.IsReregistering;
            ControlSystem = info.ControlSystem;
        }

        public object Clone()
        {
            LedRegistationInfo info = new LedRegistationInfo(this);
            return info;
        }
    }

    public class LedRegistationInfoRequest
    {
        [JsonProperty("mac")]
        public string mac { get; set; }
        [JsonProperty("sn")]
        public string sn { get; set; }
    }


    public class LedRegistationInfoResponse
    {
        public string Mac { get; set; }
        public string SN { get; set; }
        public string Name { get; set; }        
        public string User { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsRegisted { get; set; }
    }

    public enum ControlSystemType
    {
        LED_Nova_M3 = 1,
        LED_Nova_Pluto
    }
}
