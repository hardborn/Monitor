using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class LedAcquisitionConfig:ICloneable
    {
        /// <summary>
        /// 数据周期
        /// </summary>
        public int DataPeriod
        {
            get;
            set;
        }

        public bool IsAutoRefresh
        {
            get;
            set;
        }

        public int RetryCount
        {
            get;
            set;
        }

        public LedAcquisitionConfig()
        {

        }

        private LedAcquisitionConfig(LedAcquisitionConfig config)
        {
            this.DataPeriod = config.DataPeriod;
            this.IsAutoRefresh = config.IsAutoRefresh;
            this.RetryCount = config.RetryCount;
        }

        public object Clone()
        {
            var config = new LedAcquisitionConfig(this);
            return config;
        }
    }
}
