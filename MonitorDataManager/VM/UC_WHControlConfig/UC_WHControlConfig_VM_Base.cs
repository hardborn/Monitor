using GalaSoft.MvvmLight;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_WHControlConfig_VM_Base : ViewModelBase
    {
        #region 属性
        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }
        private string _aliaName;
        public string AliaName
        {
            get { return _aliaName; }
            set
            {
                _aliaName = value;
                RaisePropertyChanged("AliaName");
            }
        }

        private string _sn;
        public string SN
        {
            get { return _sn; }
            set
            {
                _sn = value;
                RaisePropertyChanged("SN");
            }
        }

        private StrategyType _stratType;
        public StrategyType StratlType
        {
            get { return _stratType; }
        }

        public string StrategyTypeStr
        { get { return ControlConfigLangTable.StrategyTypeTable[_stratType]; } }

        private Dictionary<string, PowerCtrl_Type> _powerCtrlDic;
        public Dictionary<string, PowerCtrl_Type> PowerCtrlDic
        {
            get { return _powerCtrlDic; }
            set { _powerCtrlDic = value; }
        }

        public string Condition
        {
            get
            {
                string str = string.Empty;
                if (_stratType == StrategyType.SmokeStrategy)
                {
                    str += ControlConfigLangTable.StrategyTypeTable[_stratType] + ":";
                    UC_WHControlConfig_Smoke_VM vm = (UC_WHControlConfig_Smoke_VM)this;
                    str += vm.GreaterThan;
                }
                else if (_stratType == StrategyType.TemperatureStrategy)
                {
                    UC_WHControlConfig_Tem_VM vm = (UC_WHControlConfig_Tem_VM)this;
                    str += ControlConfigLangTable.StrategyTypeTable[_stratType] + "(" + ControlConfigLangTable.ConditionAlgorithmTypeTable[vm.ConditionAlgor] + ")" + ":";
                    str += vm.GreaterThan + "-";
                    str += vm.LessThan + " ℃";
                }
                return str;
            }
        }
        #endregion
        public UC_WHControlConfig_VM_Base(string sn, Guid id, StrategyType stratType, Dictionary<string, PowerCtrl_Type> powerCtrlDic)
        {
            _sn = sn;
            List<LedBasicInfo> ledInfoList = MonitorAllConfig.Instance().LedInfoList.FindAll(a => a.Sn == _sn);
            if (ledInfoList.Count != 0)
                _aliaName = ledInfoList[0].AliaName;
            _id = id;
            _stratType = stratType;
            InitialPowerList(powerCtrlDic);
        }
        public UC_WHControlConfig_VM_Base(string sn, StrategyType stratType)
        {
            _sn = sn;
            List<LedBasicInfo> ledInfoList = MonitorAllConfig.Instance().LedInfoList.FindAll(a => a.Sn == _sn);
            if (ledInfoList.Count != 0)
                _aliaName = ledInfoList[0].AliaName;
            _id = Guid.NewGuid();
            _stratType = stratType;
            InitialPowerList(new Dictionary<string, PowerCtrl_Type>());
        }
        #region 私有函数
        private void InitialPowerList(Dictionary<string, PowerCtrl_Type> powerCtrlDic)
        {
            _powerCtrlDic = new Dictionary<string, PowerCtrl_Type>();
            if (MonitorAllConfig.Instance().PeripheralCfgDICDic != null &&
                MonitorAllConfig.Instance().PeripheralCfgDICDic.Count != 0)
            {
                foreach (string item in MonitorAllConfig.Instance().PeripheralCfgDICDic.Keys)
                {
                    if (!powerCtrlDic.Keys.Contains(item)) _powerCtrlDic.Add(item, PowerCtrl_Type.still);
                    else _powerCtrlDic.Add(item, powerCtrlDic[item]);
                }
            }
        }
        #endregion
    }
}
