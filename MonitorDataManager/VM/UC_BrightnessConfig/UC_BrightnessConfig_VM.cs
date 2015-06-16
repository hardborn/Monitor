using GalaSoft.MvvmLight;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_BrightnessConfig_VM : ViewModelBase
    {
        #region 属性
        private string _sn;
        public string SN
        {
            get { return _sn; }
        }
        private List<BrightnessConfigInfo> _brightnessConfigList = new List<BrightnessConfigInfo>();
        public List<BrightnessConfigInfo> BrightnessConfigList
        {
            get { return _brightnessConfigList; }
            set { _brightnessConfigList = value; }
        }
        private AutoBrightExtendData _autoBrightData = null;
        public AutoBrightExtendData AutoBrightData
        {
            get { return _autoBrightData; }
        }
        #endregion
        #region 公共方法
        public void Initialize(string sn, DisplaySmartBrightEasyConfigBase cfg)
        {
            _sn = sn;
            _brightnessConfigList.Clear();
            if (cfg == null) return;
            BrightnessConfigInfo brightConfig;
            _autoBrightData = cfg.AutoBrightSetting;
            if (cfg.OneDayConfigList != null)
            {
                foreach (var item in cfg.OneDayConfigList)
                {
                    brightConfig = new BrightnessConfigInfo();
                    brightConfig.Type = item.ScheduleType;
                    brightConfig.Time = item.StartTime;
                    brightConfig.IsConfigEnable = item.IsConfigEnable;
                    brightConfig.DayList = item.CustomDayCollection;
                    brightConfig.Brightness = item.BrightPercent;
                    if (brightConfig.DayList.Count == 7) brightConfig.ExecutionCycle = CycleType.everyday;
                    else if (brightConfig.DayList.FindAll(a => (a == DayOfWeek.Saturday) || a == DayOfWeek.Sunday).Count > 0) brightConfig.ExecutionCycle = CycleType.userDefined;
                    else
                    {
                        if (brightConfig.DayList.FindAll(a => (a == DayOfWeek.Monday) ||
                                                         (a == DayOfWeek.Tuesday) ||
                                                         (a == DayOfWeek.Wednesday) ||
                                                         (a == DayOfWeek.Thursday) ||
                                                         (a == DayOfWeek.Friday)
                                                         ).Count == 5)
                            brightConfig.ExecutionCycle = CycleType.workday;
                        else brightConfig.ExecutionCycle = CycleType.userDefined;
                    }
                    _brightnessConfigList.Add(brightConfig);
                }
            }
        }
        public bool IsOK()
        {
            if (_autoBrightData == null || _autoBrightData.UseLightSensorList == null || _autoBrightData.UseLightSensorList.Count == 0)
            {
                if (IsIncloudAutoConfigs()) return false;
                else return true;
            }
            else return true;
        }
        private bool IsIncloudAutoConfigs()
        {
            foreach (var item in _brightnessConfigList)
            {
                if (item.Type == SmartBrightAdjustType.AutoBright) return true;
            }
            return false;
        }
        #endregion
    }
}
