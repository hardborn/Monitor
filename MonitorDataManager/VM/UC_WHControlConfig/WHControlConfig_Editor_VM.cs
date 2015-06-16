using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class WHControlConfig_Editor_VM : ViewModelBase
    {
        private string _selectedScreenSN;
        public string SelectedScreenSN
        {
            get { return _selectedScreenSN; }
            set
            {
                _selectedScreenSN = value;
                RaisePropertyChanged("SelectedScreenSN");
            }
        }
        private ControlType _selectedCtrlType;
        public ControlType SelectedCtrlType
        {
            get { return _selectedCtrlType; }
            set
            {
                _selectedCtrlType = value;
                RaisePropertyChanged("SelectedCtrlType");
            }
        }
        private List<ControlType> _ctrlTypeList;
        public List<ControlType> CtrlTypeList
        {
            get { return _ctrlTypeList; }
            set
            {
                _ctrlTypeList = value;
                RaisePropertyChanged("CtrlTypeList");
            }
        }
        private UC_WHControlConfig_VM_Base _strategy;
        public UC_WHControlConfig_VM_Base Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        private List<UC_WHControlConfig_Smoke_VM> _smokeStrategyList = new List<UC_WHControlConfig_Smoke_VM>();
        public List<UC_WHControlConfig_Smoke_VM> SmokeStrategyList
        {
            get { return _smokeStrategyList; }
            set
            {
                _smokeStrategyList = value;
                RaisePropertyChanged("SmokeStrategyList");
            }
        }
        private List<UC_WHControlConfig_Tem_VM> _temStrategyList = new List<UC_WHControlConfig_Tem_VM>();
        public List<UC_WHControlConfig_Tem_VM> TemStrategyList
        {
            get { return _temStrategyList; }
            set
            {
                _temStrategyList = value;
                RaisePropertyChanged("TemStrategyList");
            }
        }
        #region 公共方法
        public WHControlConfig_Editor_VM()
        {
            _selectedScreenSN = string.Empty;
            ControlType type;
            _ctrlTypeList = new List<ControlType>();
            type = new ControlType();
            type.Name = ControlConfigLangTable.StrategyTypeTable[StrategyType.SmokeStrategy];
            type.Value = StrategyType.SmokeStrategy;
            _ctrlTypeList.Add(type);
            type = new ControlType();
            type.Name = ControlConfigLangTable.StrategyTypeTable[StrategyType.TemperatureStrategy];
            type.Value = StrategyType.TemperatureStrategy;
            _ctrlTypeList.Add(type);
            _selectedCtrlType = new ControlType();
            _selectedCtrlType.Value = StrategyType.TemperatureStrategy;
            _selectedCtrlType.Name = ControlConfigLangTable.StrategyTypeTable[StrategyType.TemperatureStrategy];
        }
        public ControlConfigSaveRes IsTemOk(UC_WHControlConfig_Tem_VM tem_VM)
        {
            if (_temStrategyList == null) return ControlConfigSaveRes.ok;
            if (tem_VM == null) return ControlConfigSaveRes.tem_objIsNull;
            bool isAllInvalid = true;
            foreach (var item in tem_VM.PowerCtrlDic.Values)
            {
                if (item != PowerCtrl_Type.still) isAllInvalid = false;
            }
            if (isAllInvalid && !tem_VM.IsControlBrightness) return ControlConfigSaveRes.tem_CtrlCfgIsInvalid;
            if (tem_VM.GreaterThan + 5 > tem_VM.LessThan) return ControlConfigSaveRes.tem_ConditionError;
            List<UC_WHControlConfig_Tem_VM> tem = _temStrategyList.FindAll(a => (a.LessThan + 5 > tem_VM.GreaterThan &&
              a.GreaterThan - 5 < tem_VM.GreaterThan) ||
              (a.LessThan + 5 > tem_VM.LessThan &&
              a.GreaterThan - 5 < tem_VM.LessThan) ||
              (a.GreaterThan - 5 > tem_VM.GreaterThan &&
              a.LessThan + 5 < tem_VM.LessThan));
            if (tem.Count != 0) return ControlConfigSaveRes.tem_CtrlCfgIsExist;
            return ControlConfigSaveRes.ok;
        }
        public ControlConfigSaveRes IsSmokeOk(UC_WHControlConfig_Smoke_VM smoke_VM)
        {
            if (_smokeStrategyList == null) return ControlConfigSaveRes.ok;
            if (smoke_VM == null) return ControlConfigSaveRes.smoke_objIsNull;
            bool isAllInvalid = true;
            foreach (var item in smoke_VM.PowerCtrlDic)
            {
                if (item.Value == PowerCtrl_Type.still) continue;
                else isAllInvalid = false;
                for (int i = 0; i < _smokeStrategyList.Count; i++)
                {
                    if (IsSmokeConflict(item.Key, _smokeStrategyList[i].PowerCtrlDic)) return ControlConfigSaveRes.smoke_CtrlCfgIsExist;
                }
            }
            if (isAllInvalid) return ControlConfigSaveRes.smoke_CtrlCfgIsInvalid;
            return ControlConfigSaveRes.ok;
        }
        #endregion
        #region 私有函数
        private bool IsSmokeConflict(string key, Dictionary<string, PowerCtrl_Type> smokeDic)
        {
            List<UC_WHControlConfig_Smoke_VM> smokeList = new List<UC_WHControlConfig_Smoke_VM>();
            foreach (var item in smokeDic)
            {
                if (item.Key == key && item.Value != PowerCtrl_Type.still) return true;
            }
            return false;
        }
        #endregion
    }
}
