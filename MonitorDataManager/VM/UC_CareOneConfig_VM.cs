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
    public class UC_CareOneConfig_VM:ViewModelBase
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
                string[] str = _sn.Split('-');
                Sn10 = str[0] + "-" + (Convert.ToInt32(str[1], 16) + 1).ToString("00");
                RaisePropertyChanged("Sn");
            }
        }

        private string _sn10 = string.Empty;
        public string Sn10
        {
            get
            {
                return _sn10;
            }
            set
            {
                _sn10 = value;
                RaisePropertyChanged("Sn10");
            }
        }

        private int _width = 0;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                RaisePropertyChanged("Width");
            }
        }
        private int _height = 0;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                RaisePropertyChanged("Height");
            }
        }

        private bool _isRegister = false;
        public bool IsRegister
        {
            get
            {
                return _isRegister;
            }
            set
            {
                _isRegister = value;
                RaisePropertyChanged("IsRegister");
            }
        }
        private string _card_Num = string.Empty;
        public string Card_Num
        {
            get
            {
                return _card_Num;
            }
            set
            {
                _card_Num = value;
                RaisePropertyChanged("Card_Num");
            }
        }

        private string _led_name = string.Empty;
        public string Led_name
        {
            get
            {
                if (string.IsNullOrEmpty(_led_name))
                {
                    return string.Empty;
                }
                return _led_name.Trim(' ');
            }
            set
            {
                _led_name = value;
                RaisePropertyChanged("Led_name");
            }
        }
        private double _latitude = 0;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }
        private double _longitude = 0;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                RaisePropertyChanged("Longitude");
            }
        }

        private DataOneRegistationInfo _dataInfo=null;

        public UC_CareOneConfig_VM()
        {
            if (!this.IsInDesignMode)
            {
                CmdInitialize = new RelayCommand<DataOneRegistationInfo>(Initialize);
                Messenger.Default.Register<NotificationMessageAction<DataOneRegistationInfo>>(this, MsgToken.MSG_CareOneConfig_SAVEDATA, OnMSG_CareOneConfig_SAVEDATA);
            }
        }

        private void OnMSG_CareOneConfig_SAVEDATA(NotificationMessageAction<DataOneRegistationInfo> action)
        {
            _dataInfo.Led_name = Led_name;
            _dataInfo.Latitude = Latitude;
            _dataInfo.Longitude = Longitude;
            action.Execute(_dataInfo);
        }

        private void Initialize(DataOneRegistationInfo dataInfo)
        {
            _dataInfo = dataInfo;
            Sn = dataInfo.Sn;
            Width = dataInfo.Width;
            Height = dataInfo.Height;
            Card_Num = dataInfo.Card_Num;
            IsRegister = dataInfo.IsRegister;
            Led_name = dataInfo.Led_name;
        }

        public RelayCommand<DataOneRegistationInfo> CmdInitialize
        {
            get;
            private set;
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister<NotificationMessageAction<DataOneRegistationInfo>>(this, MsgToken.MSG_CareOneConfig_SAVEDATA, OnMSG_CareOneConfig_SAVEDATA);
            base.Cleanup();
        }
    }
}
