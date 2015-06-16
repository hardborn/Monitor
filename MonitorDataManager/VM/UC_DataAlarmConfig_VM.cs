using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_DataAlarmConfig_VM : ViewModelBase
    {
        public string SN
        {
            get
            {
                return _sn;
            }
            set
            {
                _sn = value;
                RaisePropertyChanged("SN");
            }
        }
        private string _sn = string.Empty;

        private bool _isUI = true;

        private bool _isEnableTempAlarm = false;

        public bool IsEnableTempAlarm
        {
            get { return _isEnableTempAlarm; }
            set
            {
                _isEnableTempAlarm = value;
                RaisePropertyChanged("IsEnableTempAlarm");
            }
        }


        public string StrTempFlag
        {
            get
            {
                return _strTempFlag;
            }
            set
            {
                _strTempFlag = value;
                if (_isUI && _tempType == TemperatureType.Celsius)
                {
                    return;
                }
                if (_strTempFlag == "℃")
                {
                    TempType = TemperatureType.Celsius;
                    TemperatureThreshold = (int)MonitorAllConfig.Instance().GetTempValueChanged(_tempType, TemperatureThreshold);
                    MaxTempValue = 79;
                    MinTempValue = -19;
                }
                else
                {
                    TempType = TemperatureType.Fahrenheit;
                    TemperatureThreshold = (int)MonitorAllConfig.Instance().GetTempValueChanged(_tempType, TemperatureThreshold);
                    MaxTempValue = 174;
                    MinTempValue = -2;
                }
                RaisePropertyChanged("StrTempFlag");
            }
        }
        private string _strTempFlag = "℃";

        /// <summary>
        /// 摄氏 Celsius = 0,华氏:Fahrenheit
        /// </summary>
        public TemperatureType TempType
        {
            get
            {
                return _tempType;
            }
            set
            {
                _tempType = value;
            }
        }

        private TemperatureType _tempType = TemperatureType.Celsius;

        public int TemperatureThreshold
        {
            get
            {
                return _temperatureThreshold;
            }
            set
            {
                if (_temperatureThreshold != value)
                {
                    _temperatureThreshold = value;
                    RaisePropertyChanged("TemperatureThreshold");
                }
            }
        }
        private int _temperatureThreshold = 60;


        public int MaxTempValue
        {
            get
            {
                return _maxTempValue;
            }
            set
            {
                _maxTempValue = value;
                RaisePropertyChanged("MaxTempValue");
            }
        }
        private int _maxTempValue = 79;
        public int MinTempValue
        {
            get
            {
                return _minTempValue;
            }
            set
            {
                _minTempValue = value;
                RaisePropertyChanged("MinTempValue");
            }
        }

        private int _minTempValue = -19;

        private bool _isEnableVoltageAlarm = false;

        public bool IsEnableVoltageAlarm
        {
            get { return _isEnableVoltageAlarm; }
            set
            {
                _isEnableVoltageAlarm = value;
                _isEnableVoltageErrorAlarm = value;
                RaisePropertyChanged("IsEnableVoltageAlarm");
                RaisePropertyChanged("IsEnableVoltageErrorAlarm");
            }
        }

        public float VoltageThreshold
        {
            get
            {
                return _voltageThreshold;
            }
            set
            {
                _voltageThreshold = value;
                RaisePropertyChanged("VoltageThreshold");
            }
        }
        private float _voltageThreshold = 4;

        private bool _isEnableVoltageErrorAlarm = false;

        public bool IsEnableVoltageErrorAlarm
        {
            get { return _isEnableVoltageErrorAlarm; }
            set
            {
                _isEnableVoltageAlarm = value;
                _isEnableVoltageErrorAlarm = value;
                RaisePropertyChanged("IsEnableVoltageAlarm");
                RaisePropertyChanged("IsEnableVoltageErrorAlarm");
            }
        }

        public float VoltageErrorThreshold
        {
            get
            {
                return _voltageErrorThreshold;
            }
            set
            {
                _voltageErrorThreshold = value;
                RaisePropertyChanged("VoltageErrorThreshold");
            }
        }
        private float _voltageErrorThreshold = 3.5f;

        public float VoltageThresholdMax { get; set; }

        private bool _isEnableHumidityAlarm = false;

        public bool IsEnableHumidityAlarm
        {
            get { return _isEnableHumidityAlarm; }
            set
            {
                _isEnableHumidityAlarm = value;
                RaisePropertyChanged("IsEnableHumidityAlarm");
            }
        }

        public int HumidityThreshold
        {
            get
            {
                return _humidityThreshold;
            }
            set
            {
                _humidityThreshold = value;
                RaisePropertyChanged("HumidityThreshold");
            }
        }
        private int _humidityThreshold = 60;

        private bool _isEnableFanSpeedAlarm = false;

        public bool IsEnableFanSpeedAlarm
        {
            get { return _isEnableFanSpeedAlarm; }
            set
            {
                _isEnableFanSpeedAlarm = value;
                RaisePropertyChanged("IsEnableFanSpeedAlarm");
            }
        }

        public int FanSpeedThreshold
        {
            get
            {
                return _fanSpeedThreshold;
            }
            set
            {
                _fanSpeedThreshold = value;
                RaisePropertyChanged("FanSpeedThreshold");
            }
        }
        private int _fanSpeedThreshold = 1000;

        public UC_DataAlarmConfig_VM()
        {
            CmdInitialize = new RelayCommand<string>(OnCmdInitialize);
        }
        public RelayCommand<string> CmdInitialize
        {
            get;
            private set;
        }

        private RelayCommand _cmdSaveAlarmConfig;
        public RelayCommand CmdSaveAlarmConfig
        {
            get
            {
                if (_cmdSaveAlarmConfig == null)
                {
                    _cmdSaveAlarmConfig = new RelayCommand(() => { OnCmdSaveAlarmConfig(); }, null);
                }
                return _cmdSaveAlarmConfig;
            }
        }

        private void OnCmdInitialize(string sn)
        {
            _sn = sn;
            LedAlarmConfig ledAlarm = null;
            if (MonitorAllConfig.Instance().LedAlarmConfigs == null)
            {
                return;
            }

            foreach (var item in MonitorAllConfig.Instance().LedAlarmConfigs)
            {
                if (_sn == MonitorAllConfig.Instance().ALLScreenName)
                {
                    ledAlarm = item;
                    break;
                }
                else if (_sn == item.SN)
                {
                    ledAlarm = item;
                    break;
                }
            }
            if (ledAlarm != null)
            {
                _isUI = true;
                InitialAlarmConfg(ledAlarm);
                _isUI = false;
            }
        }

        public bool OnCmdSaveAlarmConfig()
        {
            LedAlarmConfig ledAlarmCfg = GetAlarmConfg();
            //TODO:将系统配置入库 MonitorAllConfig.Instance().u(_sn, ledAlarmCfg);
            MonitorAllConfig.Instance().UserConfigInfo.TemperatureUnit = _tempType;
            if (!MonitorAllConfig.Instance().SaveUserConfig())
            {
                return false;
            }
            return MonitorAllConfig.Instance().UpdateLedAlarmConfig(_sn, ledAlarmCfg);
        }

        private void InitialAlarmConfg(LedAlarmConfig ledAlarm)
        {
            if (ledAlarm.ParameterAlarmConfigList == null || ledAlarm.ParameterAlarmConfigList.Count == 0)
            {
                return;
            }
            foreach (ParameterAlarmConfig param in ledAlarm.ParameterAlarmConfigList)
            {
                if (param != null)
                {
                    switch (param.ParameterType)
                    {
                        case StateQuantityType.Temperature:
                            IsEnableTempAlarm = !param.Disable;
                            TemperatureThreshold = (int)param.HighThreshold;
                            break;
                        case StateQuantityType.Humidity:
                            IsEnableHumidityAlarm = !param.Disable;
                            HumidityThreshold = (int)param.HighThreshold;
                            break;
                        case StateQuantityType.Voltage:
                            if (param.Level == AlarmLevel.Warning)
                            {
                                IsEnableVoltageAlarm = !param.Disable; 
                                VoltageThreshold = (float)param.LowThreshold;
                                VoltageThresholdMax = (float)param.HighThreshold;
                            }
                            else if (param.Level == AlarmLevel.Malfunction)
                            {
                                IsEnableVoltageErrorAlarm = !param.Disable;
                                VoltageErrorThreshold = (float)param.HighThreshold;
                            }
                            break;
                        case StateQuantityType.FanSpeed:
                            IsEnableFanSpeedAlarm = !param.Disable;
                            FanSpeedThreshold = (int)param.LowThreshold;
                            break;
                    }
                }
            }

            if (MonitorAllConfig.Instance().UserConfigInfo != null)
            {
                if (_tempType == TemperatureType.Celsius)
                {
                    StrTempFlag = "℃";
                }
                else
                {
                    StrTempFlag = "℉";
                }
            }
        }
        private LedAlarmConfig GetAlarmConfg()
        {
            LedAlarmConfig ledAlarm = new LedAlarmConfig();
            ledAlarm.SN = SN;
            ledAlarm.ParameterAlarmConfigList = new List<ParameterAlarmConfig>();

            ParameterAlarmConfig param = new ParameterAlarmConfig();
            param.Disable = !IsEnableTempAlarm;
            param.ParameterType = StateQuantityType.Temperature;
            if (_tempType == TemperatureType.Fahrenheit)
            {
                param.HighThreshold = (int)MonitorAllConfig.Instance().GetTempValueChanged(TemperatureType.Celsius, TemperatureThreshold);
            }
            else
            {
                param.HighThreshold = TemperatureThreshold;
            }
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.Disable = !IsEnableHumidityAlarm;
            param.ParameterType = StateQuantityType.Humidity;
            param.HighThreshold = HumidityThreshold;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.Disable = !IsEnableVoltageAlarm;
            param.ParameterType = StateQuantityType.Voltage;
            param.LowThreshold = VoltageThreshold;
            if (param.LowThreshold >= param.HighThreshold)
            {
                param.HighThreshold = VoltageThreshold + 0.1;
            }
            if (param.HighThreshold > 12)
            {
                param.HighThreshold = 12;
            }
            param.Level = AlarmLevel.Warning;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.Disable = !IsEnableVoltageErrorAlarm;
            param.ParameterType = StateQuantityType.Voltage;
            param.HighThreshold = VoltageErrorThreshold;
            param.LowThreshold = 0;
            param.Level = AlarmLevel.Malfunction;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            param.Disable = !IsEnableFanSpeedAlarm;
            param.ParameterType = StateQuantityType.FanSpeed;
            param.LowThreshold = FanSpeedThreshold;
            param.HighThreshold = 0;
            ledAlarm.ParameterAlarmConfigList.Add((ParameterAlarmConfig)param.Clone());
            return ledAlarm;
        }
    }
}
