using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_CycleConfig_VM : ViewModelBase
    {
        private bool _isCycle = false;
        public bool IsCycleMonitor 
        {
            get
            {
                return _isCycle;
            }
            set
            {
                _isCycle = value;
                RaisePropertyChanged("IsCycleMonitor");
            }
        }

        private int _retryRead = 1;
        public int RetryReadTimes
        {
            get
            {
                return _retryRead;
            }
            set
            {
                _retryRead = value;
                RaisePropertyChanged("RetryReadTimes");
            }
        }
        private int _period = 60;
        public int MonitorPeriod
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
                RaisePropertyChanged("MonitorPeriod");
            }
        }

        public UC_CycleConfig_VM()
        {
            if (!this.IsInDesignMode)
            {
                CmdInitialize = new RelayCommand(OnCmdInitialize);
                CmdRefreshTime = new RelayCommand(OnCmdRefreshTime);
            }
        }
        public RelayCommand CmdInitialize
        {
            get;
            private set;
        }

        private RelayCommand _cmdSaveTo;
        public RelayCommand CmdSaveTo
        {
            get
            {
                if (_cmdSaveTo == null)
                {
                    _cmdSaveTo = new RelayCommand(() => { OnCmdSaveTo(); }, null);
                }
                return _cmdSaveTo;
            }
        }
        public RelayCommand CmdRefreshTime
        {
            get;
            private set;
        }

        private void OnCmdInitialize()
        {
            if (MonitorAllConfig.Instance().AcquisitionConfig == null)
            {
                MonitorAllConfig.Instance().AcquisitionConfig = new LedAcquisitionConfig();
            }
            IsCycleMonitor = MonitorAllConfig.Instance().AcquisitionConfig.IsAutoRefresh;
            RetryReadTimes = MonitorAllConfig.Instance().AcquisitionConfig.RetryCount;
            MonitorPeriod = MonitorAllConfig.Instance().AcquisitionConfig.DataPeriod / 1000;
        }
        public bool OnCmdSaveTo()
        {
            LedAcquisitionConfig acquisitionConfig=new LedAcquisitionConfig();
            acquisitionConfig.IsAutoRefresh = IsCycleMonitor;
            acquisitionConfig.RetryCount = RetryReadTimes;
            acquisitionConfig.DataPeriod = MonitorPeriod *1000;

            return MonitorAllConfig.Instance().UpdateLedAcquisitionConfig(acquisitionConfig);
        }

        private void OnCmdRefreshTime()
        {
            MonitorAllConfig.Instance().RefreshMonitoringData();
        }
    }
}
