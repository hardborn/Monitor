using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public enum StateQuantityType
    {
        Voltage = 0,
        Temperature,
        Humidity,
        Brightness,
        FanSpeed,
        Smoke,
        WorkStatus,
        ConnectionStatus,
        DVIRate,
        DoorStatus,
        ComputeStatus,
        FlatCableStatus,
        SendCardPortStatus,
        EnvironmentBrightness = 13
    }
}
