using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class MonitoringParameter : StateObject, IMonitoringConfig, IAlarm
    {
        public bool MonitoringEnable
        {
            get;
            set;
        }

        public double HighThreshold
        {
            get;
            set;
        }

        public double LowThreshold
        {
            get;
            set;
        }

        public string PositionInfo { get; set; }

        public MonitoringParameter() { }

        private MonitoringParameter(MonitoringParameter parameter)
        {
            this.HighThreshold = parameter.HighThreshold;
            this.LowThreshold = parameter.LowThreshold;
            this.MonitoringEnable = parameter.MonitoringEnable;
            this.PositionInfo = parameter.PositionInfo.Clone() as string;
            this.Type = parameter.Type;
            this.Value = parameter.Value;
        }

        public object Clone()
        {
            var parameter = new MonitoringParameter(this);
            return parameter;
        }

        public bool VerifyAlarm(double currentValue)
        {
            throw new NotImplementedException();
        }
    }
}
