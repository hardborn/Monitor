using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public interface IMonitoringConfig
    {
        /// <summary>
        /// 参数监控使能
        /// </summary>
        bool MonitoringEnable
        {
            get;
            set;
        }
    }
}
