using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class LedAlarmConfig : ICloneable
    {
        public string SN { get; set; }
                    
        public List<ParameterAlarmConfig> ParameterAlarmConfigList { get; set; }


        public LedAlarmConfig()
        {
        }
        private LedAlarmConfig(LedAlarmConfig config)
        {
            this.SN = string.IsNullOrEmpty(config.SN) ? null : config.SN;
            this.ParameterAlarmConfigList = config.ParameterAlarmConfigList == null ? null : config.ParameterAlarmConfigList.Select(T => (ParameterAlarmConfig)T.Clone()).ToList();
        }

        public object Clone()
        {
            LedAlarmConfig config = new LedAlarmConfig(this);
            return config;
        }
    }
}
