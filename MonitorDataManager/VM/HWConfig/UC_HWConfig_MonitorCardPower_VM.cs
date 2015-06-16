using GalaSoft.MvvmLight;
using Nova.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_HWConfig_MonitorCardPower_VM : ViewModelBase
    {
        public UC_HWConfig_MonitorCardPower_VM()
        {
            _powerCount = 3;
            _allPowerOfCabinetSame = true;
        }

        private bool _isRefreshPower;
        public bool IsRefreshPower
        {
            get { return _isRefreshPower; }
            set
            {
                _isRefreshPower = value;
                RaisePropertyChanged("IsRefreshPower");
            }
        }
        private bool _allPowerOfCabinetSame;
        public bool AllPowerOfCabinetSame
        {
            get { return _allPowerOfCabinetSame; }
            set
            {
                _allPowerOfCabinetSame = value;
                RaisePropertyChanged("AllPowerOfCabinetSame");
            }
        }
        private bool _allPowerOfCabinetDif;
        public bool AllPowerOfCabinetDif
        {
            get { return _allPowerOfCabinetDif; }
            set
            {
                _allPowerOfCabinetDif = value;
                RaisePropertyChanged("AllPowerOfCabinetDif");
            }
        }
        private int _powerCount;
        public int PowerCount
        {
            get { return _powerCount; }
            set
            {
                _powerCount = value;
                RaisePropertyChanged("PowerCount");
            }
        }

        public SerializableDictionary<string, byte> AllPowerCountDif { get; set; }
    }
}
