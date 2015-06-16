using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.ColudSupport;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_PowerControlConfig_VM : ViewModelBase
    {
        public List<PowerControlData> PowerConfig
        {
            get
            {
                return _powerConfig;
            }
            set
            {
                _powerConfig = value;
                RaisePropertyChanged("PowerConfig");
            }
        }
        private List<PowerControlData> _powerConfig = new List<PowerControlData>();

        public UC_PowerControlConfig_VM()
        {
            CmdInitialize = new RelayCommand(OnCmdInitialize);
        }

        public RelayCommand CmdInitialize
        {
            get;
            private set;
        }

        private RelayCommand _cmdSaveConfig;
        public RelayCommand CmdSaveConfig
        {
            get
            {
                if (_cmdSaveConfig == null)
                {
                    _cmdSaveConfig = new RelayCommand(() => { OnCmdSaveConfig(); }, null);
                }
                return _cmdSaveConfig;
            }
        }
        private void OnCmdInitialize()
        {
            _powerConfig.Clear();
            if (MonitorAllConfig.Instance().PeripheralCfgDICDic == null)
            {
                return;
            }
            foreach (KeyValuePair<string, string> keyvalue in MonitorAllConfig.Instance().PeripheralCfgDICDic)
            {
                _powerConfig.Add(new PowerControlData()
                {
                    Key = CommandTextParser.GetDeJsonSerialization<FunctionCardRoadInfo>(keyvalue.Key),
                    Value = keyvalue.Value
                });
            }
        }
        public bool OnCmdSaveConfig()
        {
            SerializableDictionary<FunctionCardRoadInfo, string> funcCardPower = new SerializableDictionary<FunctionCardRoadInfo, string>();

            foreach (PowerControlData power in _powerConfig)
            {
                funcCardPower.Add(power.Key, power.Value);
            }
            return MonitorAllConfig.Instance().SaveFunctionCardPowerConfig(funcCardPower);
        }
    }

    public class PowerControlData
    {
        public FunctionCardRoadInfo Key { get; set; }
        public string Value { get; set; }
    }
}
