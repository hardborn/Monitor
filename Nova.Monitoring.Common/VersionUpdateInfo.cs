using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
   public class VersionUpdateInfo
    {
        [JsonProperty("VERSION")]
       public string Version { get; set; }
        [JsonProperty("MD5")]
        public string MD5 { get; set; }
        [JsonProperty("URL")]
        public string Url { get; set; }
    }
}
