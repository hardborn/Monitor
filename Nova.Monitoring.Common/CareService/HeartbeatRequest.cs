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

        [JsonProperty("SyncInfos")]
        public List<SyncSummary> SyncInfos { get; set; }
    }

    public class HeartbeatResponse
    {
        public string sn { get; set; }
        public int result { get; set; }
        public List<SyncSummaryResponse> SyncInfos { get; set; }       
        public List<Command> CommandList { get; set; }
    }

    public class CheckSystemUpdateRequest
    {
         [JsonProperty("mac")]
        public string mac { get; set; }

        [JsonProperty("SystemVersion")]
        public string SystemVersion { get; set; }
    }


    public class SyncSummaryResponse
    {
        public SyncFlag IsSync { get; set; }
        public SyncType Type { get; set; }
    }

    public class SyncSummary
    {
        public string SyncMark { get; set; }
        public SyncType Type { get; set; }
    }

    public enum SyncType
    {
        AcquisitionConfig = 1,
        AlarmConfig,
        PeripheralsConfig,
        StrategyConfig,
        PeriodicInspectionConfig,
        BrightnessRuleConfig
    }

    public enum SyncFlag
    {
        Synchronized = 0,
        TerminalNotSynchronized,
        CareNotSynchronized,
    }
}

