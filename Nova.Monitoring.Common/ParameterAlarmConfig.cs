using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class ParameterAlarmConfig : ICloneable
    {
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool Disable { get; set; }
        /// <summary>
        /// 告警最小阈值
        /// </summary>
        public double LowThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 告警最大阈值
        /// </summary>
        public double HighThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 告警参数类型
        /// </summary>
        public StateQuantityType ParameterType
        {
            get;
            set;
        }

        public AlarmLevel Level { get; set; }

        public ParameterAlarmConfig()
        {
            Disable = true;
        }
        private ParameterAlarmConfig(ParameterAlarmConfig config)
        {
            this.LowThreshold = config.LowThreshold;
            this.HighThreshold = config.HighThreshold;
            this.ParameterType = config.ParameterType;
            this.Level = config.Level;
            this.Disable = config.Disable;
        }

        public object Clone()
        {
            ParameterAlarmConfig config = new ParameterAlarmConfig(this);
            return config;
        }
    }

    public enum AlarmLevel
    {
        Warning = 0,
        Malfunction
    }
}
