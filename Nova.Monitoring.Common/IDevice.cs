using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public interface IDevice
    {
        /// <summary>
        /// 设备标识
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        Nova.Monitoring.Common.DeviceType Type
        {
            get;
            set;
        }
    }
}
