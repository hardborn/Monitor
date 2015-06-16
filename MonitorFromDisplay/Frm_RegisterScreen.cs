using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.UI.MonitorFromDisplay.UC_Config;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class Frm_RegisterScreen : Frm_CommonBase
    {

        private Hashtable _languageTable;
        private string _account;
        private List<LedRegistationInfo> _screenInfoList = new List<LedRegistationInfo>();
        private List<LedRegistationInfo> _requestScreenList = new List<LedRegistationInfo>();

        public Frm_RegisterScreen()
        {
            InitializeComponent();
            this.Load += Frm_RegisterScreen_Load;
            this.FormClosed += Frm_RegisterScreen_FormClosed;
        }

        void Frm_RegisterScreen_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
        }

        public Frm_RegisterScreen(List<LedRegistationInfo> screenInfoList)
            : this()
        {
            _screenInfoList = screenInfoList;
            Messenger.Default.Register<string>(this, MsgToken.MSG_NOTIFYDIALOG_OK, OnCareServerSaveFinish);
            InitializeScreenList(_screenInfoList);
        }

        void Frm_RegisterScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_NOTIFYDIALOG_OK, OnCareServerSaveFinish);
        }

        private void ApplyUILanguageTable()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.ScreenRegistrationLangPath,"Frm_RegisterScreen", this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.ScreenRegistrationLangPath, "Frm_RegisterScreen", out _languageTable);

            if (_languageTable == null || _languageTable.Count == 0)
                return;

        }



        private void InitializeScreenList(List<LedRegistationInfo> screenSummaryInfoList)
        {
            dynamicFlowLayoutPanel.Controls.Clear();
            foreach (var item in screenSummaryInfoList)
            {
                var screenView = new UC_ScreenSummary(item);
                dynamicFlowLayoutPanel.Controls.Add(screenView);
            }
            var registeredScreen = screenSummaryInfoList.FirstOrDefault(s => !string.IsNullOrEmpty(s.UserID));
            if (registeredScreen == null)
            {
                this.userNameTextBox.Text = string.Empty;
            }
            else
            {
                this.userNameTextBox.Text = registeredScreen.UserID;
            }
        }


        private void registerButton_Click(object sender, EventArgs e)
        {
            VerifyRegisterationInformation();

            foreach (var screenItem in MonitorAllConfig.Instance().LedInfoList)
            {
                var resultItem = _screenInfoList.FirstOrDefault(s => s.sn_num == screenItem.Sn);
                if (resultItem == null)
                {
                    _requestScreenList.Add(new LedRegistationInfo()
                    {
                        card_num = GetCardNum(screenItem.PartInfos),
                        mac = AppDataConfig.CurrentMAC,
                        sn_num = screenItem.Sn,
                        led_height = screenItem.Height,
                        led_width = screenItem.Width,
                        IsReregistering = false,
                        ControlSystem = ControlSystemType.LED_Nova_M3,
                        Latitude = 0d,
                        Longitude = 0d,
                        UserID = string.Empty,
                        led_name = string.Empty
                    });
                }
                else
                {
                    _requestScreenList.Add(new LedRegistationInfo()
                    {
                        card_num = GetCardNum(screenItem.PartInfos),
                        mac = AppDataConfig.CurrentMAC,
                        sn_num = screenItem.Sn,
                        led_height = screenItem.Height,
                        led_width = screenItem.Width,
                        IsReregistering = false,
                        ControlSystem = ControlSystemType.LED_Nova_M3,
                        Latitude = resultItem.Latitude,
                        Longitude = resultItem.Longitude,
                        UserID = resultItem.UserID,
                        led_name = resultItem.led_name
                    });
                }
            }
            Action action = new Action(() =>
            {
                RegisterScreens(_account, _requestScreenList, false);
            });
            action.BeginInvoke(null, null);
            string strText = CommonUI.GetCustomMessage(_languageTable, "Waiting_information", "注册中，请等待...");
            ShowProcessForm(strText, true);

        }

        private bool VerifyRegisterationInformation()
        {
            _account = this.userNameTextBox.Text;
            foreach (var item in dynamicFlowLayoutPanel.Controls)
            {
                var screenItem = item as UC_ScreenSummary;
                if (screenItem != null)
                {
                    var registerationItem = _screenInfoList.FirstOrDefault(s => s.sn_num == screenItem.Id);
                    if (registerationItem != null)
                    {
                        registerationItem.led_name = screenItem.Name;
                    }
                }
            }
            return true;
        }

        private void RegisterScreens(string account, List<LedRegistationInfo> screenList, bool isReregistering)
        {
            var responseResult = MonitorAllConfig.Instance().SaveResgiterTo(account, screenList, isReregistering);
            Messenger.Default.Send<string>(responseResult.ToString(), MsgToken.MSG_NOTIFYDIALOG_OK);
        }


        private void OnCareServerSaveFinish(string result)
        {
            string tmpType = string.Empty;
            switch (result)
            {
                case "ScreenAlreadyExists":
                    tmpType = CommonUI.GetCustomMessage(_languageTable, "ScreenAlreadyExists", "屏体已注册!");
                    break;
                case "AccountNotExist":
                    tmpType = CommonUI.GetCustomMessage(_languageTable, "AccountNotExist", "账户不存在!");
                    break;
                case "ScreenRegisteredSuccessfully":
                    tmpType = CommonUI.GetCustomMessage(_languageTable, "ScreenRegisteredSuccessfully", "注册成功!");
                    MonitorAllConfig.Instance().LedBasicToUIScreen();
                    break;
                case "SnEmpty":
                    tmpType = CommonUI.GetCustomMessage(_languageTable, "SnEmpty", "不允许存在屏体别名为空!");
                    break;
                case "ScreenReregister":
                    if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_languageTable, "ScreenReregister", "免则声明：注册用户变更，是否继续?"), "", MessageBoxButtons.OKCancel, MessageBoxIconType.Question)
                        == DialogResult.OK)
                    {
                        Action action = new Action(() =>
                        {
                            //_requestScreenList.ForEach(s => s.IsReregistering = true);
                            RegisterScreens(_account, _requestScreenList, true);
                        });
                        this.BeginInvoke(action, null);
                        return;
                    }
                    else
                    {
                        CloseProcessForm();
                    }
                    return;
                default:
                    tmpType = CommonUI.GetCustomMessage(_languageTable, "CareRegFailed", "注册失败!");
                    break;
            }

            ShowCustomMessageBox(tmpType, "", MessageBoxButtons.OK, MessageBoxIconType.Alert);
            CloseProcessForm();
        }
        private static string GetCardNum(List<PartInfo> partsInfoList)
        {
            int senderCardNum = 0;
            int reciverCardNum = 0;
            int monitingCardNum = 0;
            foreach (var item in partsInfoList)
            {
                if (item.Type == DeviceType.ReceiverCard)
                {
                    reciverCardNum = item.Amount;
                }
                if (item.Type == DeviceType.SendCard)
                {
                    senderCardNum = item.Amount;
                }
                if (item.Type == DeviceType.MonitoringCard)
                {
                    monitingCardNum = item.Amount;
                }
            }
            return senderCardNum + "+" + reciverCardNum + "+" + monitingCardNum;
        }

        //private LedRegistationInfo GetLedRegistationInfo(ScreenSummaryInfo oneInfo)
        //{
        //    LedRegistationInfo ledregister = new LedRegistationInfo();
        //    ledregister.sn_num = oneInfo.Sn;
        //    ledregister.UserID = UserId;
        //    ledregister.Latitude = oneInfo.Latitude;
        //    ledregister.led_height = oneInfo.Height;
        //    ledregister.led_width = oneInfo.Width;
        //    ledregister.led_name = oneInfo.Led_name;
        //    ledregister.Longitude = oneInfo.Longitude;
        //    ledregister.mac = oneInfo.Mac;
        //    ledregister.card_num = oneInfo.Card_NumSave;
        //    ledregister.ControlSystem = ControlSystemType.LED_Nova_M3;
        //    return ledregister;
        //}
    }
}
