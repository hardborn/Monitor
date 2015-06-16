using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_WHControlConfig_Tem_VM : UC_WHControlConfig_VM_Base
    {
        #region 属性
        private ConditionAlgorithm _conditionAlgor = ConditionAlgorithm.AverageAlgorithm;
        public ConditionAlgorithm ConditionAlgor
        {
            get { return _conditionAlgor; }
            set
            {
                _conditionAlgor = value;
                RaisePropertyChanged("ConditionAlgor");
            }
        }

        private int _lessThan = 70;
        public int LessThan
        {
            get { return _lessThan; }
            set
            {
                _lessThan = value;
                RaisePropertyChanged("LessThan");
            }
        }
        private int _greaterThan = 50;
        public int GreaterThan
        {
            get { return _greaterThan; }
            set
            {
                _greaterThan = value;
                RaisePropertyChanged("GreaterThan");
            }
        }

        private bool _isEnableBrightCtrl = true;
        public bool IsEnableBrightCtrl
        {
            get { return _isEnableBrightCtrl; }
            set
            {
                _isEnableBrightCtrl = value;
                RaisePropertyChanged("IsEnableBrightCtrl");
            }
        }

        private bool _isControlBrightness = true;
        public bool IsControlBrightness
        {
            get { return _isControlBrightness; }
            set
            {
                _isControlBrightness = value;
                RaisePropertyChanged("IsControlBrightness");
            }
        }
        private int _brightness = 70;
        public int Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                RaisePropertyChanged("Brightness");
            }
        }

        private List<ConditionType> _conditionTypeList;
        public List<ConditionType> ConditionTypeList
        {
            get { return _conditionTypeList; }
            set
            {
                _conditionTypeList = value;
                RaisePropertyChanged("ConditionTypeList");
            }
        }
        #endregion
        #region 公共方法
        public UC_WHControlConfig_Tem_VM(string sn, Guid id, ConditionAlgorithm conditionAlgor, int lessThan, int greaterThan, bool isControlBrightness, int brightness, Dictionary<string, PowerCtrl_Type> powerCtrlDic)
            : base(sn, id, StrategyType.TemperatureStrategy, powerCtrlDic)
        {
            InitialConditionTypeList();
            _conditionAlgor = conditionAlgor;
            _lessThan = lessThan;
            _greaterThan = greaterThan;
            _isControlBrightness = isControlBrightness;
            _brightness = brightness;
        }
        public UC_WHControlConfig_Tem_VM(string sn)
            : base(sn, StrategyType.TemperatureStrategy)
        {
            InitialConditionTypeList();
        }
        private void InitialConditionTypeList()
        {
            ConditionType type;
            _conditionTypeList = new List<ConditionType>();
            type = new ConditionType();
            type.Name = ControlConfigLangTable.ConditionAlgorithmTypeTable[ConditionAlgorithm.MaxValueAlgorithm];
            type.Value = ConditionAlgorithm.MaxValueAlgorithm;
            _conditionTypeList.Add(type);
            type = new ConditionType();
            type.Name = ControlConfigLangTable.ConditionAlgorithmTypeTable[ConditionAlgorithm.AverageAlgorithm];
            type.Value = ConditionAlgorithm.AverageAlgorithm;
            _conditionTypeList.Add(type);
        }
        #endregion
        #region 私有函数
        ///// <summary>
        ///// 初始化电源管理列表
        ///// </summary>
        //private void InitialPowerList(Dictionary<string, PowerCtrl_Type> powerCtrlDic)
        //{
        //    _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
        //    if (MonitorAllConfig.Instance().PeripheralCfgDICDic != null &&
        //        MonitorAllConfig.Instance().PeripheralCfgDICDic.Count != 0)
        //    {
        //        foreach (var item in MonitorAllConfig.Instance().PeripheralCfgDICDic.Keys)
        //        {
        //            if (!powerCtrlDic.ContainsKey(item)) _powerCtrlDic.Add(item, PowerCtrl_Type.still);
        //            else _powerCtrlDic.Add(item, powerCtrlDic[item]);
        //        }
        //    }
        //}
        #endregion
        #region

        //#region 属性
        //private Dictionary<string, PowerCtrl_Type> _powerCtrlDic;
        //public Dictionary<string, PowerCtrl_Type> PowerCtrlDic
        //{
        //    get { return _powerCtrlDic; }
        //    set
        //    {
        //        _powerCtrlDic = value;
        //        RaisePropertyChanged("PowerCtrlList");
        //    }
        //}

        //private Strategy_Tem_Smoke_VM _strategyInfo;
        //public Strategy_Tem_Smoke_VM StrategyInfo
        //{
        //    get { return _strategyInfo; }
        //    set
        //    {
        //        _strategyInfo = value;
        //        RaisePropertyChanged("StrategyInfo");
        //    }
        //}
        //private List<ConditionType> _conditionTypeList;
        //public List<ConditionType> ConditionTypeList
        //{
        //    get { return _conditionTypeList; }
        //    set
        //    {
        //        _conditionTypeList = value;
        //        RaisePropertyChanged("ConditionTypeList");
        //    }
        //}
        //#endregion
        //#region 公共方法
        //public UC_WHControlConfig_Tem_VM()
        //{
        //    ConditionType type;
        //    _conditionTypeList = new List<ConditionType>();
        //    type = new ConditionType();
        //    type.Name = "平均温度";
        //    type.Value = ConditionAlgorithm.MaxValueAlgorithm;
        //    _conditionTypeList.Add(type);
        //    type = new ConditionType();
        //    type.Name = "最高温度";
        //    type.Value = ConditionAlgorithm.AverageAlgorithm;
        //    _conditionTypeList.Add(type);
        //}
        //public void Initialize(Strategy_Tem_Smoke_VM strategyInfo)
        //{
        //    _strategyInfo = strategyInfo;
        //    InitialPowerList();
        //}
        //#endregion

        ///// <summary>
        ///// 初始化电源管理列表
        ///// </summary>
        //private void InitialPowerList()
        //{
        //    _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
        //    if (MonitorAllConfig.Instance().PeripheralCfgDICDic != null &&
        //        MonitorAllConfig.Instance().PeripheralCfgDICDic.Count != 0)
        //    {
        //        foreach (var item in MonitorAllConfig.Instance().PeripheralCfgDICDic.Keys)
        //        {
        //            if (!_strategyInfo.PowerCtrlDic.ContainsKey(item)) _powerCtrlDic.Add(item, PowerCtrl_Type.still);
        //            else _powerCtrlDic.Add(item, _strategyInfo.PowerCtrlDic[item]);
        //        }
        //    }
        //}
        #endregion
    }
}
