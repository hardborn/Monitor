using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
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
       
        public LedRegistationInfo() { }

        private LedRegistationInfo(LedRegistationInfo info)
        {
            sn_num = info.sn_num.Clone() as string;
            led_name = info.led_name.Clone() as string;
            led_width = info.led_width;
            led_height = info.led_height;
            Latitude = info.Latitude;
            Longitude = info.Longitude;
            card_num = info.card_num.Clone() as string;
            mac = info.mac.Clone() as string;
            UserID = info.UserID.Clone() as string;
            IsReregistering = info.IsReregistering;
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
        [JsonProperty("mac")]
        public string Mac { get; set; }
        [JsonProperty("sn")]
        public string SN { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user_id")]
        public string User { get; set; }
        [JsonProperty("is_reg")]
        public bool IsRegisted { get; set; }
    }
}
