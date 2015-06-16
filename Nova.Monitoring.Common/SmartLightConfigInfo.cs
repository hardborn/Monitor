using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    [Serializable]
    public class SmartLightConfigInfo:ICopy,ICloneable
    {
        public SmartLightConfigInfo()
        {
            HwExecTypeValue = BrightnessHWExecType.SoftWareControl;
        }
        public string ScreenSN { get; set; }
        public BrightnessHWExecType HwExecTypeValue { get; set; }
        public DisplaySmartBrightEasyConfigBase DisplayHardcareConfig { get; set; }        
        public DisplaySmartBrightEasyConfig DispaySoftWareConfig { get; set; }

        public object Clone()
        {
            SmartLightConfigInfo data = new SmartLightConfigInfo();
            if (!this.CopyTo(data))
            {
                return null;
            }
            return data;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is SmartLightConfigInfo))
            {
                return false;
            }
            SmartLightConfigInfo config = obj as SmartLightConfigInfo;
            config.DisplayHardcareConfig = new DisplaySmartBrightEasyConfigBase();
            this.DisplayHardcareConfig.CopyTo(config.DisplayHardcareConfig);
            config.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
            this.DispaySoftWareConfig.CopyTo(config.DispaySoftWareConfig);
            config.HwExecTypeValue = this.HwExecTypeValue;
            config.ScreenSN = this.ScreenSN;
            return true;
        }
    }
}
