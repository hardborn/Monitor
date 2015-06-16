using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public interface IAlarm
    {
        /// <summary>
        /// 告警最大阈值
        /// </summary>
        double HighThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// 告警最小阈值
        /// </summary>
        double LowThreshold
        {
            get;
            set;
        }

        bool VerifyAlarm(double currentValue);
    }
}
