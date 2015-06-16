using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class Device
    {
        /// <summary>
        /// 设备标识
        /// </summary>
        public string Id
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType Type
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 设备状态量列表
        /// </summary>
        public System.Collections.Generic.List<Nova.Monitoring.Common.MonitoringParameter> ParameterList
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
