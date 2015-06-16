using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_HWConfigOpticalProbe_VM : ViewModelBase
    {
        private string _sn = string.Empty;
        public string Sn
        {
            get
            {
                return _sn;
            }
            set
            {
                _sn = value;
                RaisePropertyChanged("Sn");
            }
        }

        public List<CheckUseablePeripheral> CheckUseable
        {
            get
            {
                return _checkUseable;
            }
            set
            {
                _checkUseable = value;
                RaisePropertyChanged("CheckUseable");
            }
        }
        private List<CheckUseablePeripheral> _checkUseable = new List<CheckUseablePeripheral>();

        public UC_HWConfigOpticalProbe_VM()
        {
            CmdInitialize = new RelayCommand<string>(OnCmdInitialize);
        }

        public RelayCommand<string> CmdInitialize
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

        private void OnCmdInitialize(string sn)
        {
            _sn = sn;
            _checkUseable.Clear();
            if(MonitorAllConfig.Instance().AllOpticalProbeInfo==null 
                || MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList==null)
            {
                return;
            }
            foreach(UseablePeripheral useable in MonitorAllConfig.Instance().AllOpticalProbeInfo.UseablePeripheralList)
            {
                CheckUseablePeripheral checkuseable=new CheckUseablePeripheral();
                checkuseable.UseablePer = useable;
                if (sn == MonitorAllConfig.Instance().ALLScreenName)
                {
                    checkuseable.IsChecked = false;
                }
                else
                {
                    OpticalProbeConfig optical = MonitorAllConfig.Instance().LedOpticalProbeConfigs.Find(a => a.SN == sn);
                    checkuseable.IsChecked = false;
                    if (optical == null || optical.ConfigInfo == null || optical.ConfigInfo.Count == 0)
                    {
                        checkuseable.IsChecked = false;
                    }
                    else
                    {
                        if (optical.ConfigInfo.FindIndex(a => a.Equals(useable)) >= 0)
                        {
                            checkuseable.IsChecked = true;
                        }
                    }
                }
                _checkUseable.Add(checkuseable);
            }
        }
        public bool OnCmdSaveConfig()
        {
            OpticalProbeConfig optical = new OpticalProbeConfig();
            optical.SN = _sn;
            optical.ConfigInfo=new List<UseablePeripheral>();
            foreach (CheckUseablePeripheral checkusea in _checkUseable)
            {
                if (checkusea.IsChecked)
                {
                    optical.ConfigInfo.Add(checkusea.UseablePer);
                }
            }
            return MonitorAllConfig.Instance().SaveOpticalProbeInfoCofig(_sn, optical);
        }
    }

    public class CheckUseablePeripheral:ICloneable
    {
        public bool IsChecked { get; set; }
        public UseablePeripheral UseablePer { get; set; }

        public object Clone()
        {
            CheckUseablePeripheral location = new CheckUseablePeripheral();
            if (this.CopyTo(location))
            {
                return location;
            }
            return null;
        }
        public bool CopyTo(object obj)
        {
            if (!(obj is CheckUseablePeripheral))
            {
                return false;
            }
            CheckUseablePeripheral location = obj as CheckUseablePeripheral;
            location.IsChecked = this.IsChecked;
            location.UseablePer = (UseablePeripheral)this.UseablePer.Clone();
            return true;
        }
    }
}
