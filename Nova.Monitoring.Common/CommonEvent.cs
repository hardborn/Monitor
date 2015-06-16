using Nova.LCT.GigabitSystem.HWConfigAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class BrightnessValueRefreshEventArgs : EventArgs
    {
        public string SN { get; set; }

        public string BrightnessValue { get; set; }

        public BrightnessValueRefreshEventArgs(string sn, string brightnessValue)
        {
            SN = sn;
            BrightnessValue = brightnessValue;
        }
    }
    public class BrightnessConfigChangedEventArgs : EventArgs
    {
        public string SN { get; set; }

        public SmartLightConfigInfo SmartLightConfig { get; set; }

        public BrightnessConfigChangedEventArgs(string sn, SmartLightConfigInfo brightnessValue)
        {
            SN = sn;
            SmartLightConfig = brightnessValue;
        }
    }
}
