using GalaSoft.MvvmLight;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_HWConfig_CabinetFanInfo_VM : ViewModelBase
    {
        public UC_HWConfig_CabinetFanInfo_VM()
        {
            _fanSpeed = 1;
            _fanCount = 4;
            AllFanOfCabinetSame = true;
        }

        private bool _isRefreshFan = false;
        public bool IsRefreshFan
        {
            get { return _isRefreshFan; }
            set
            {
                _isRefreshFan = value;
                RaisePropertyChanged("IsRefreshFan");
            }
        }
        private int _fanSpeed;
        public int FanSpeed
        {
            get { return _fanSpeed; }
            set
            {
                _fanSpeed = value;
                RaisePropertyChanged("FanSpeed");
            }
        }
        private int _fanCount;
        public int FanCount
        {
            get { return _fanCount; }
            set
            {
                _fanCount = value;
                RaisePropertyChanged("FanCount");
            }
        }
        private bool _allFanOfCabinetSame;
        public bool AllFanOfCabinetSame
        {
            get { return _allFanOfCabinetSame; }
            set
            {
                _allFanOfCabinetSame = value;
                RaisePropertyChanged("AllFanOfCabinetSame");
            }
        }
        private bool _allFanOfCabinetDif;
        public bool AllFanOfCabinetDif
        {
            get { return _allFanOfCabinetDif; }
            set
            {
                _allFanOfCabinetDif = value;
                RaisePropertyChanged("AllFanOfCabinetDif");
            }
        }

        public SerializableDictionary<string, byte> AllFanCountDif { get; set; }
    }
}
