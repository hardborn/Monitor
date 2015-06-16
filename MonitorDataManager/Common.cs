using GalaSoft.MvvmLight;
using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public enum PowerCtrl_Type
    {
        open,
        close,
        still
    }
    public class ScreenInfoList : List<LedBasicInfo>
    { }
    public class ConditionType : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private ConditionAlgorithm _value;
        public ConditionAlgorithm Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }
    }
    public class ControlType : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private StrategyType _value;
        public StrategyType Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }
    }
    public enum ControlConfigSaveRes
    {
        ok,
        tem_CtrlCfgIsExist,
        tem_CtrlCfgIsInvalid,
        tem_objIsNull,
        tem_ConditionError,
        smoke_CtrlCfgIsExist,
        smoke_CtrlCfgIsInvalid,
        smoke_objIsNull,
    }
    public enum Mode
    {
        modify,
        add,
        delete
    }
    //public enum BrightenssConfigType
    //{
    //    fasten,
    //    automate
    //}
    public enum CycleType
    {
        everyday,
        userDefined,
        workday
    }
    public struct BrightnessStruct
    {
        private CycleType _cType;
        public CycleType CType
        {
            get { return _cType; }
            set { _cType = value; }
        }
        private List<DayOfWeek> _dayOfWeekList;
        public List<DayOfWeek> DayOfWeekList
        {
            get { return _dayOfWeekList; }
            set { _dayOfWeekList = value; }
        }
        public override string ToString()
        {
            string str = string.Empty;
            if (_cType == CycleType.userDefined)
            {
                foreach (var item in _dayOfWeekList)
                {
                    if (str != string.Empty) str += ",";
                    if (BrightnessLangTable.DayTable != null)
                    {
                        str += BrightnessLangTable.DayTable[item];
                    }
                    else
                    {
                        switch (item)
                        {
                            case DayOfWeek.Friday:
                                str += "周五";
                                break;
                            case DayOfWeek.Monday:
                                str += "周一";
                                break;
                            case DayOfWeek.Saturday:
                                str += "周六";
                                break;
                            case DayOfWeek.Sunday:
                                str += "周日";
                                break;
                            case DayOfWeek.Thursday:
                                str += "周四";
                                break;
                            case DayOfWeek.Tuesday:
                                str += "周二";
                                break;
                            case DayOfWeek.Wednesday:
                                str += "周三";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else if (_cType == CycleType.everyday)
            {
                if (BrightnessLangTable.CycleTypeTable != null)
                {
                    str += BrightnessLangTable.CycleTypeTable[_cType];
                }
                else str = "每天";
            }
            else if (_cType == CycleType.workday)
                if (BrightnessLangTable.CycleTypeTable != null)
                {
                    str += BrightnessLangTable.CycleTypeTable[_cType];
                }
                else str = "周一到周五";
            return str;
        }
    }
    public class BrightnessConfigInfo : ICloneable
    {
        private bool _isConfigEnable;
        public bool IsConfigEnable
        {
            get { return _isConfigEnable; }
            set { _isConfigEnable = value; }
        }

        private DateTime _time = new DateTime(2014, 1, 1, 0, 0, 0);
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string TypeStr
        {
            get
            {
                if (_type == SmartBrightAdjustType.FixBright)
                {
                    if (BrightnessLangTable.SmartBrightTypeTable != null)
                        return BrightnessLangTable.SmartBrightTypeTable[_type];
                    else return "固定亮度";
                }
                else
                {
                    if (BrightnessLangTable.SmartBrightTypeTable != null)
                        return BrightnessLangTable.SmartBrightTypeTable[_type];
                    else return "自动亮度";
                }
            }
        }

        private SmartBrightAdjustType _type = SmartBrightAdjustType.FixBright;
        public SmartBrightAdjustType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private CycleType _executionCycle = CycleType.everyday;
        public CycleType ExecutionCycle
        {
            get { return _executionCycle; }
            set
            {
                _executionCycle = value;
                _brightStruct.CType = _executionCycle;
            }
        }

        public string DayListStr
        {
            get
            {
                _brightStruct.DayOfWeekList = _dayList;
                return _brightStruct.ToString();
            }
        }

        private float _brightness = 0;
        public float Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        public string BrightnessDisplay
        {
            get
            {
                if (_type == SmartBrightAdjustType.FixBright) return _brightness.ToString();
                else return "--";
            }
        }

        private List<DayOfWeek> _dayList = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
        public List<DayOfWeek> DayList
        {
            get { return _dayList; }
            set
            {
                _dayList = value;
            }
        }

        public string EditingOperation { get { return "编辑"; } }
        public string DelationOperation { get { return "删除"; } }

        private BrightnessStruct _brightStruct = new BrightnessStruct();
        public object Clone()
        {
            BrightnessConfigInfo info = new BrightnessConfigInfo();
            info.IsConfigEnable = _isConfigEnable;
            info.Time = _time;
            info.Type = _type;
            info.ExecutionCycle = _executionCycle;
            info.DayList = _dayList;
            info.Brightness = _brightness;
            return info;
        }
    }
    public class BrightnessLangTable
    {
        public static Dictionary<SmartBrightAdjustType, string> SmartBrightTypeTable { get; set; }
        public static Dictionary<DayOfWeek, string> DayTable { get; set; }
        public static Dictionary<CycleType, string> CycleTypeTable { get; set; }
    }
    public class ControlConfigLangTable
    {
        public static Dictionary<StrategyType, string> StrategyTypeTable { get; set; }
        public static Dictionary<ConditionAlgorithm, string> ConditionAlgorithmTypeTable { get; set; }
    }
    public enum BrightnessConfigType
    {
        software,
        hardware
    }
}
