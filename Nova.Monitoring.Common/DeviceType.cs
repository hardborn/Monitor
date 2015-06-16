using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public enum DeviceType
    {
        Undefined = 0,
        LedScreen,
        SendCard,
        NetworkPort,
        Deconcentrator,
        ReceiverCard,
        MonitoringCard,
        Fan,
        PowerSupply,
        /// <summary>
        /// 排线
        /// </summary>
        FlatCable,
        /// <summary>
        /// 功能卡
        /// </summary>
        FunctionCard,
        Computer,
    }
}
