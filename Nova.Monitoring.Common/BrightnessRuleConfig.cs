using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class BrightnessConfig
    {
        public string DisplayUDID { get; set; }

        public bool IsSupportAutoBrightness { get; set; }
        public List<OneSmartBrightEasyConfig> OneDayConfigList { get; set; }
        public AutoBrightnessConfig AutoBrightSetting { get; set; }

        public BrightnessConfig()
        {
            OneDayConfigList = new List<OneSmartBrightEasyConfig>();
            AutoBrightSetting = new AutoBrightnessConfig();
            IsSupportAutoBrightness = false;
        }
        public BrightnessConfig(SmartLightConfigInfo smartLightConfig)
            : this()
        {
            DisplayUDID = smartLightConfig.ScreenSN;
            //AutoBrightSetting = new AutoBrightnessConfig();
            if (smartLightConfig != null && smartLightConfig.DispaySoftWareConfig != null && smartLightConfig.DispaySoftWareConfig.AutoBrightSetting != null)
            {
                AutoBrightSetting.AutoBrightMappingList = smartLightConfig.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList;
                AutoBrightSetting.OpticalFailureInfo = smartLightConfig.DispaySoftWareConfig.AutoBrightSetting.OpticalFailureInfo;
            }
            if (smartLightConfig != null && smartLightConfig.DispaySoftWareConfig != null && smartLightConfig.DispaySoftWareConfig.OneDayConfigList != null)
            {
                OneDayConfigList = smartLightConfig.DispaySoftWareConfig.OneDayConfigList;
            }
            if (smartLightConfig == null 
                || smartLightConfig.DispaySoftWareConfig == null 
                || smartLightConfig.DispaySoftWareConfig.AutoBrightSetting == null 
                || smartLightConfig.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList == null 
                || smartLightConfig.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList.Count == 0)
            {
                IsSupportAutoBrightness = false;
            }
            else
            {
                IsSupportAutoBrightness = true;
            }
        }
    }

    //public class BrightnessRuleConfig
    //{
    //    public bool IsConfigEnable { get; set; }
    //    public string StartTime { get; set; }
    //    public DimmingMode ScheduleType { get; set; }
    //}

    public class AutoBrightnessConfig
    {
        public List<DisplayAutoBrightMapping> AutoBrightMappingList { get; set; }
        public OpticalProbeFailureInfo OpticalFailureInfo { get; set; }

        public AutoBrightnessConfig()
        {
            AutoBrightMappingList = new List<DisplayAutoBrightMapping>();
            OpticalFailureInfo = new OpticalProbeFailureInfo();
        }
    }
    //public class OpticalExtendConfig
    //{
    //    public bool IsEnable { get; set; }
    //    public float BrightnessValue { get; set; }
    //}



}
