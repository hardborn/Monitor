using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class HeartbeatRequest
    {
        [JsonProperty("Identifier")]
        public string Identifier { get; set; }
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("SystemVersion")]
        public string SystemVersion { get; set; }
    }
}
