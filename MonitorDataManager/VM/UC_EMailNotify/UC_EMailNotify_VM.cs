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
    public class UC_EMailNotify_VM : ViewModelBase
    {
        #region 属性
        public EMailNotifyConfig NotifyConfig
        {
            get { return _notifyConfig; }
            set 
            {
                _notifyConfig = value;
                RaisePropertyChanged("NotifyConfig");
            }
        }
        private EMailNotifyConfig _notifyConfig = new EMailNotifyConfig();
        #endregion

        #region 初始化
        public UC_EMailNotify_VM()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (MonitorAllConfig.Instance().NotifyConfig!=null)
            {
                _notifyConfig = (EMailNotifyConfig)MonitorAllConfig.Instance().NotifyConfig.Clone();
            }
        }
        #endregion

        #region 方法
        public bool OnCmdSaveTo()
        {
            return MonitorAllConfig.Instance().SaveEMailNotifyConfig((EMailNotifyConfig)NotifyConfig.Clone());
        }
        #endregion
    }
}
