using Nova.LCT.GigabitSystem.HWPointDetect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class LedMonitoringConfig : ICloneable
    {
        public string SN { get; set; }
        public MonitoringCardConfig MonitoringCardConfig { get; set; }

        public LedMonitoringConfig() { }
        private LedMonitoringConfig(LedMonitoringConfig config)
        {
            SN = string.IsNullOrEmpty(config.SN) ? string.Empty : config.SN.Clone() as string;

            MonitoringCardConfig = config.MonitoringCardConfig == null ? null : config.MonitoringCardConfig.Clone() as MonitoringCardConfig;
        }

        public object Clone()
        {
            LedMonitoringConfig config = new LedMonitoringConfig(this);
            return config;
        }
    }

    public class MonitoringCardConfig : IMonitoringConfig, ICloneable
    {

        public bool MonitoringEnable
        {
            get;
            set;
        }

        public List<ParameterMonitoringConfig> ParameterConfigTable { get; set; }


        public MonitoringCardConfig() { }
        private MonitoringCardConfig(MonitoringCardConfig config)
        {
            MonitoringEnable = config.MonitoringEnable;
            ParameterConfigTable = config.ParameterConfigTable == null ? null : config.ParameterConfigTable.Select(item => (ParameterMonitoringConfig)item.Clone()).ToList();
        }

        public object Clone()
        {
            MonitoringCardConfig config = new MonitoringCardConfig(this);
            return config;
        }
    }

    public class ParameterMonitoringConfig : IMonitoringConfig, ICloneable
    {
        private int _reservedConfig = -1;
        /// <summary>
        /// 状态类型
        /// </summary>
        public StateQuantityType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 监测使能
        /// </summary>
        public bool MonitoringEnable
        {
            get;
            set;
        }

        public ParameterConfigMode ConfigMode { get; set; }
        public int GeneralExtendedConfig { get; set; }
        public List<ParameterExtendedConfig> ExtendedConfig { get; set; }

        public int ReservedConfig
        {
            get { return _reservedConfig; }
            set { _reservedConfig = value; }
        }

        public ParameterMonitoringConfig() { }
        private ParameterMonitoringConfig(ParameterMonitoringConfig config)
        {
            ConfigMode = config.ConfigMode;
            ExtendedConfig = config.ExtendedConfig == null ? null : config.ExtendedConfig.Select(item => (ParameterExtendedConfig)item.Clone()).ToList();
            MonitoringEnable = config.MonitoringEnable;
            GeneralExtendedConfig = config.GeneralExtendedConfig;
            Type = config.Type;
            ReservedConfig = config.ReservedConfig;
        }

        public object Clone()
        {
            ParameterMonitoringConfig config = new ParameterMonitoringConfig(this);
            return config;
        }
    }

    public class ParameterExtendedConfig : ICloneable
    {
        public string ReceiveCardId { get; set; }
        public int ParameterCount { get; set; }

        public ParameterExtendedConfig() { }
        private ParameterExtendedConfig(ParameterExtendedConfig config)
        {
            ReceiveCardId = config.ReceiveCardId == null ? string.Empty : config.ReceiveCardId.Clone() as string;
            ParameterCount = config.ParameterCount;
        }

        public object Clone()
        {
            ParameterExtendedConfig config = new ParameterExtendedConfig(this);
            return config;
        }
    }
    public enum ParameterConfigMode
    {
        NoneExtendedMode,
        GeneralExtendedMode,
        AdvanceExtendedMode
    }
}
