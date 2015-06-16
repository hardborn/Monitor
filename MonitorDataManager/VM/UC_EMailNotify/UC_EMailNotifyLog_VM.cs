using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_EMailNotifyLog_VM : ViewModelBase
    {
         #region 属性
        public List<NotifyContent> NotifyContentList
        {
            get { return _notifyContentList; }
            set 
            {
                _notifyContentList = value;
                RaisePropertyChanged("NotifyContentList");
            }
        }
        private List<NotifyContent> _notifyContentList = new List<NotifyContent>();

        public string SelectedTimes
        {
            get;
            set;
        }

        public RelayCommand CmdRefresh
        {
            get;
            private set;
        }
        private RelayCommand _cmdRefresh;
        #endregion

        #region 初始化
        public UC_EMailNotifyLog_VM()
        {
            Initialize();
        }
        public void Initialize()
        {
            CmdRefresh = new RelayCommand(OnCmdRefresh);
        }
        #endregion

        #region 方法
        private void OnCmdRefresh()
        {
            NotifyContentList.Clear();
            if(MonitorAllConfig.Instance().GetSendEMailLog(SelectedTimes))
            {
                List<NotifyContent> temp=MonitorAllConfig.Instance().NotifyContentList;
                for (int i = 0; i < temp.Count; i++)
                {
                    NotifyContentList.Add(temp[i]);
                }
            }
        }

        public bool OnCmdDeleteLog()
        {
            NotifyContentList.Clear();
            return MonitorAllConfig.Instance().DeleteOneSendEMailLog(SelectedTimes);
        }
        #endregion
    }
}
