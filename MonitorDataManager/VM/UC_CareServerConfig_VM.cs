using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.MonitorDataManager
{
    public class UC_CareServerConfig_VM : ViewModelBase
    {

        private string _userId = "";
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                RaisePropertyChanged("UserId");
            }
        }

        #region 数据属性

        private List<DataOneRegistationInfo> _uc_CareRegisters = new List<DataOneRegistationInfo>();
        public List<DataOneRegistationInfo> Uc_CareRegisters
        {
            get
            {
                return _uc_CareRegisters;
            }
            set
            {
                _uc_CareRegisters = value;
            }
        }
        #endregion
        #region 私有字段
        private object _ledRegistationMetux = new object();
        #endregion
        #region 命令
        public RelayCommand CmdInitialize
        {
            get;
            private set;
        }

        public RelayCommand<bool> CmdSaveTo
        {
            get;
            private set;
        }

        public RelayCommand CmdGetRegist
        {
            get;
            private set;
        }

        #endregion

        #region 构造函数
        public UC_CareServerConfig_VM()
        {
            if (!this.IsInDesignMode)
            {
                CmdInitialize = new RelayCommand(Initialize);
                CmdGetRegist = new RelayCommand(OnCmdGetRegist);
                CmdSaveTo = new RelayCommand<bool>(OnCmdSaveTo);
                Uc_CareRegisters = new List<DataOneRegistationInfo>();
            }
        }

        private void OnCmdGetRegist()
        {
            MonitorAllConfig.Instance().LedBasicToUIScreen();
        }
        #endregion

        private void Initialize()
        {
            lock (_ledRegistationMetux)
            {
                Uc_CareRegisters.Clear();
                foreach (LedRegistationInfoResponse ledRegistation in MonitorAllConfig.Instance().LedRegistationUiList)
                {
                    if (!string.IsNullOrEmpty(ledRegistation.User))
                    {
                        UserId = ledRegistation.User;
                    }
                    SetCareRegisterInfo(ledRegistation.SN);
                }
            }
        }

        private void OnCmdSaveTo(bool isReregister)
        {
            List<LedRegistationInfo> list = new List<LedRegistationInfo>();
            ServerResponseCode strResult = ServerResponseCode.ScreenAlreadyExists;
            foreach (DataOneRegistationInfo oneInfo in Uc_CareRegisters)
            {
                if (string.IsNullOrEmpty(oneInfo.Led_name))
                {
                    strResult = ServerResponseCode.SnEmpty;
                    list.Clear();
                    break;
                }
                list.Add(GetLedRegistationInfo(oneInfo));
            }
            if (list.Count > 0)
            {
                if (isReregister)
                {
                    foreach (LedRegistationInfo regInfo in list)
                    {
                        regInfo.IsReregistering = true;
                    }
                }
                strResult = MonitorAllConfig.Instance().SaveResgiterTo(UserId, list, list[0].IsReregistering);

                if (strResult == ServerResponseCode.ScreenRegisteredSuccessfully)
                {
                    foreach (LedRegistationInfo regInfo in list)
                    {
                        DataOneRegistationInfo oneInfo = Uc_CareRegisters.Find(a => a.Sn == regInfo.sn_num);
                        LedRegistationInfoResponse ledReg = MonitorAllConfig.Instance().LedRegistationUiList.Find(a => a.SN == regInfo.sn_num);
                        ledReg.IsRegisted = true;
                        ledReg.Name = oneInfo.Led_name;
                        ledReg.User = UserId;
                        ledReg.Latitude = oneInfo.Latitude;
                        ledReg.Longitude = oneInfo.Longitude;

                        oneInfo.IsRegister = true;
                        oneInfo.UserId = UserId;
                    }
                    MonitorAllConfig.Instance().LedRegistationInfoEventMethod(false);
                }
            }

            Messenger.Default.Send<string>(strResult.ToString(), MsgToken.MSG_NOTIFYDIALOG_OK);
        }

        private LedRegistationInfo GetLedRegistationInfo(DataOneRegistationInfo oneInfo)
        {
            LedRegistationInfo ledregister = new LedRegistationInfo();
            ledregister.sn_num = oneInfo.Sn;
            ledregister.UserID = UserId;
            ledregister.Latitude = oneInfo.Latitude;
            ledregister.led_height = oneInfo.Height;
            ledregister.led_width = oneInfo.Width;
            ledregister.led_name = oneInfo.Led_name;
            ledregister.Longitude = oneInfo.Longitude;
            ledregister.mac = oneInfo.Mac;
            ledregister.card_num = oneInfo.Card_NumSave;
            ledregister.ControlSystem = ControlSystemType.LED_Nova_M3;
            return ledregister;
        }

        private DataOneRegistationInfo GetRegInfo(string sn)
        {
            DataOneRegistationInfo oneInfo = Uc_CareRegisters.Find(
                  delegate(DataOneRegistationInfo tmp)
                  {
                      return tmp.Sn == sn;
                  });
            return oneInfo;
        }

        private void SetCareRegisterInfo(string sn)
        {
            DataOneRegistationInfo uc_one = new DataOneRegistationInfo();
            LedBasicInfo led = MonitorAllConfig.Instance().LedInfoList.Find(a => a.Sn == sn);
            if (led == null)
            {
                return;
            }
            uc_one.Sn = led.Sn;
            uc_one.Width = led.Width;
            uc_one.Height = led.Height;
            uc_one.Mac = AppDataConfig.CurrentMAC;
            string cardNum = "";
            string cardNumSave = "";
            int receiverCount = 0;
            LedMonitoringConfig ledMonitor = MonitorAllConfig.Instance().LedMonitorConfigs.Find(a => a.SN == led.Sn);
            foreach (PartInfo part in led.PartInfos)
            {
                switch (part.Type)
                {
                    case DeviceType.SendCard:
                        cardNum += string.Format("{0}:{1} ", part.Type.ToString(), part.Amount);
                        cardNumSave += string.Format("+{0}", part.Amount.ToString());
                        break;
                    case DeviceType.ReceiverCard:
                        cardNum += string.Format("{0}:{1} ", part.Type.ToString(), part.Amount);
                        cardNumSave += string.Format("+{0}", part.Amount.ToString());
                        receiverCount = part.Amount;
                        break;
                    case DeviceType.MonitoringCard:
                        if (ledMonitor != null && ledMonitor.MonitoringCardConfig != null
                            && ledMonitor.MonitoringCardConfig.MonitoringEnable)
                        {
                            cardNum += string.Format("{0}:{1} ", part.Type.ToString(), receiverCount);
                            cardNumSave += string.Format("+{0}", receiverCount.ToString());
                        }
                        else
                        {
                            cardNum += string.Format("{0}:{1} ", part.Type.ToString(), 0);
                            cardNumSave += 0.ToString();
                        }
                        break;
                }
            }
            uc_one.Card_Num = cardNum;
            uc_one.Card_NumSave = cardNumSave.Substring(1);

            var ledRegs = from tmp in MonitorAllConfig.Instance().LedRegistationUiList where tmp.SN == sn select tmp;
            if (ledRegs == null)
            {
                return;
            }
            foreach (LedRegistationInfoResponse ledReg in ledRegs)
            {
                uc_one.IsRegister = ledReg.IsRegisted;
                uc_one.Led_name = ledReg.Name;
                uc_one.UserId = ledReg.User;
                uc_one.Latitude = ledReg.Latitude;
                uc_one.Longitude = ledReg.Longitude;
            }
            Uc_CareRegisters.Add(uc_one);
        }
    }
}