using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Resource.Language;
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
    public partial class Frm_RegistrationManager : Frm_CommonBase
    {
        private Hashtable _languageTable;
        private List<LedRegistationInfo> _screenInfoList = new List<LedRegistationInfo>();
        private bool _careServiceStatus;
        private delegate void LedRegistationInfoHandle(bool e);


        public Frm_RegistrationManager()
        {
            InitializeComponent();
            this.Load += Frm_RegistrationManager_Load;
            MonitorAllConfig.Instance().LedRegistationInfoEvent += LedRegistationInfoCompletedEvent;
            MonitorAllConfig.Instance().CareServiceConnectionStatusChangedEvent += Frm_MonitorConfigManager_CareServiceStatusChanged;
            InitializeRegistationData();
            _topmostTimer = new System.Threading.Timer(ThreadSetTopMostCallback);
        }
        private void ThreadSetTopMostCallback(object state)
        {
            EnableTopMost(false);
        }
        #region 单例
        private System.Threading.Timer _topmostTimer = null;
        private static Frm_RegistrationManager _instance = null;
        public static Frm_RegistrationManager Instance(bool isOpen)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new Frm_RegistrationManager();
                _instance.Show();
            }
            if (isOpen)
            {
                _instance.BringToFront();
                if (_instance.WindowState == FormWindowState.Minimized)
                {
                    _instance.WindowState = FormWindowState.Normal;
                }
            }
            return _instance;
        }
        delegate void EnableTopMostDel(bool bEnable);
        public void EnableTopMost(bool bEnable)
        {
            if (this.InvokeRequired)
            {
                while (!this.IsHandleCreated)
                {
                    if (this == null || this.IsDisposed)
                    {
                        return;
                    }
                }
                EnableTopMostDel cs = new EnableTopMostDel(EnableTopMost);
                this.Invoke(cs, new object[] { bEnable });
                return;
            }

            if (_instance != null && !_instance.IsDisposed)
            {
                _instance.TopMost = bEnable;
                if (bEnable)
                {
                    _topmostTimer.Change(300, 20000);
                }
                else
                {
                    _topmostTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                }
            }
        }

        #endregion
        void Frm_RegistrationManager_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
        }

        private void Frm_MonitorConfigManager_CareServiceStatusChanged(bool obj)
        {
            
        }

        public void ApplyUILanguageTable(string langType)
        {
            CommonUI.SetLanguage(langType);
            ApplyUILanguageTable();
        }

        public void ApplyUILanguageTable()
        {
           
            MultiLanguageUtils.UpdateLanguage(CommonUI.ScreenRegistrationLangPath, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.ScreenRegistrationLangPath, "Frm_RegistrationManager", out _languageTable);

            if (_languageTable == null || _languageTable.Count == 0)
                return;

            foreach (DataGridViewColumn columnItem in screenRegistationInfoDataGridView.Columns)
            {
                //if (columnItem.CellType == typeof(DataGridViewButtonCell))
                //{
                //    string strText = CommonUI.GetCustomMessage(_languageTable, (columnItem.Name + "_Text").ToLower(), string.Empty);
                //    (columnItem as DataGridViewButtonColumn).Text = strText;
                //}
                string str = CommonUI.GetCustomMessage(_languageTable, columnItem.Name, string.Empty);
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                columnItem.HeaderText = str;
            }
            //foreach (DataGridViewColumn columnItem in screenRegistationInfoDataGridView.Columns)
            //{
            //    if (columnItem.CellType == typeof(DataGridViewButtonCell))
            //    {
            //        string strText = CommonUI.GetCustomMessage(_languageTable, (columnItem.Name + "_Text").ToLower(), string.Empty);
            //        (columnItem as DataGridViewButtonColumn).Text = strText;
            //    }
            //    string str = CommonUI.GetCustomMessage(_languageTable, columnItem.Name.ToLower(), string.Empty);
            //    if (string.IsNullOrEmpty(str))
            //    {
            //        continue;
            //    }
            //    columnItem.HeaderText = str;
            //}
        }

        private void InitializeRegistationData()
        {
            screenRegistationInfoDataGridView.Rows.Clear();
            _screenInfoList.Clear();

            if (!MonitorAllConfig.Instance().CareServiceConnectionStatus)
            {
                if (MonitorAllConfig.Instance().LedRegistationUiList == null || MonitorAllConfig.Instance().LedRegistationUiList.Count == 0)
                {
                    foreach (var screenInfo in MonitorAllConfig.Instance().LedInfoList)
                    {
                        var screenName = screenInfo.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (screenInfo.LedIndexOfCom + 1);
                        object[] row = { screenName, screenInfo.Height, screenInfo.Width, Properties.Resources.OffLine };
                        screenRegistationInfoDataGridView.Rows.Add(row);
                        _screenInfoList.Add(new LedRegistationInfo()
                        {
                            card_num = GetCardNum(screenInfo.PartInfos),
                            mac = AppDataConfig.CurrentMAC,
                            sn_num = screenInfo.Sn,
                            led_height = screenInfo.Height,
                            led_width = screenInfo.Width,
                            IsReregistering = false,
                            ControlSystem = ControlSystemType.LED_Nova_M3,
                            Latitude = 0d,
                            Longitude = 0d,
                            UserID = string.Empty,
                            led_name = screenName
                        });
                    }
                }
                else
                {
                    foreach (var screenInfo in MonitorAllConfig.Instance().LedInfoList)
                    {
                        var screenName = screenInfo.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (screenInfo.LedIndexOfCom + 1);
                        var screenRegistInfo = MonitorAllConfig.Instance().LedRegistationUiList.FirstOrDefault(s => !string.IsNullOrEmpty(s.User) && s.SN == screenInfo.Sn);
                        if (screenRegistInfo != null)
                        {
                            object[] row = { screenRegistInfo.Name, screenInfo.Height, screenInfo.Width, Properties.Resources.OffLine };
                            screenRegistationInfoDataGridView.Rows.Add(row);
                            _screenInfoList.Add(new LedRegistationInfo()
                            {
                                card_num = GetCardNum(screenInfo.PartInfos),
                                mac = AppDataConfig.CurrentMAC,
                                sn_num = screenInfo.Sn,
                                led_height = screenInfo.Height,
                                led_width = screenInfo.Width,
                                IsReregistering = false,
                                ControlSystem = ControlSystemType.LED_Nova_M3,
                                Latitude = screenRegistInfo.Latitude,
                                Longitude = screenRegistInfo.Longitude,
                                UserID = screenRegistInfo.User,
                                led_name = screenRegistInfo.Name
                            });
                            userNameLabel.Text = screenRegistInfo.User;
                        }
                        else
                        {
                            object[] row = { screenName, screenInfo.Height, screenInfo.Width, Properties.Resources.OffLine };
                            screenRegistationInfoDataGridView.Rows.Add(row);
                            _screenInfoList.Add(new LedRegistationInfo()
                            {
                                card_num = GetCardNum(screenInfo.PartInfos),
                                mac = AppDataConfig.CurrentMAC,
                                sn_num = screenInfo.Sn,
                                led_height = screenInfo.Height,
                                led_width = screenInfo.Width,
                                IsReregistering = false,
                                ControlSystem = ControlSystemType.LED_Nova_M3,
                                Latitude = 0d,
                                Longitude = 0d,
                                UserID = string.Empty,
                                led_name = screenName
                            });
                        }
                    }
                }
            }
            else
            {
                if (MonitorAllConfig.Instance().LedRegistationUiList == null || MonitorAllConfig.Instance().LedRegistationUiList.Count == 0)
                {
                    foreach (var screenInfo in MonitorAllConfig.Instance().LedInfoList)
                    {
                        var screenName = screenInfo.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (screenInfo.LedIndexOfCom + 1);

                        object[] row = { screenName, screenInfo.Height, screenInfo.Width, Properties.Resources.NoRegister };
                        screenRegistationInfoDataGridView.Rows.Add(row);
                        _screenInfoList.Add(new LedRegistationInfo()
                        {
                            card_num = GetCardNum(screenInfo.PartInfos),
                            mac = AppDataConfig.CurrentMAC,
                            sn_num = screenInfo.Sn,
                            led_height = screenInfo.Height,
                            led_width = screenInfo.Width,
                            IsReregistering = false,
                            ControlSystem = ControlSystemType.LED_Nova_M3,
                            Latitude = 0d,
                            Longitude = 0d,
                            UserID = string.Empty,
                            led_name = screenName
                        });
                    }
                }
                else
                {
                    foreach (var screenInfo in MonitorAllConfig.Instance().LedInfoList)
                    {
                        var screenName = screenInfo.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (screenInfo.LedIndexOfCom + 1);

                        var screenRegistInfo = MonitorAllConfig.Instance().LedRegistationUiList.FirstOrDefault(s => !string.IsNullOrEmpty(s.User) && s.SN == screenInfo.Sn);
                        if (screenRegistInfo != null)
                        {
                            object[] row = { screenRegistInfo.Name, screenInfo.Height, screenInfo.Width, Properties.Resources.OnLine };
                            screenRegistationInfoDataGridView.Rows.Add(row);
                            _screenInfoList.Add(new LedRegistationInfo()
                            {
                                card_num = GetCardNum(screenInfo.PartInfos),
                                mac = AppDataConfig.CurrentMAC,
                                sn_num = screenInfo.Sn,
                                led_height = screenInfo.Height,
                                led_width = screenInfo.Width,
                                IsReregistering = false,
                                ControlSystem = ControlSystemType.LED_Nova_M3,
                                Latitude = screenRegistInfo.Latitude,
                                Longitude = screenRegistInfo.Longitude,
                                UserID = screenRegistInfo.User,
                                led_name = screenRegistInfo.Name
                            });
                            userNameLabel.Text = screenRegistInfo.User;
                        }
                        else
                        {
                            object[] row = { screenName, screenInfo.Height, screenInfo.Width, Properties.Resources.NoRegister };
                            screenRegistationInfoDataGridView.Rows.Add(row);
                            _screenInfoList.Add(new LedRegistationInfo()
                            {
                                card_num = GetCardNum(screenInfo.PartInfos),
                                mac = AppDataConfig.CurrentMAC,
                                sn_num = screenInfo.Sn,
                                led_height = screenInfo.Height,
                                led_width = screenInfo.Width,
                                IsReregistering = false,
                                ControlSystem = ControlSystemType.LED_Nova_M3,
                                Latitude = 0d,
                                Longitude = 0d,
                                UserID = string.Empty,
                                led_name = screenName
                            });
                        }
                    }
                }
            }
        }

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_CareServerConfig_String", out _hashTable);
            //dataGridViewBaseEx_Care.Columns[0].HeaderText = CommonUI.GetCustomMessage(_hashTable, "ScreenSN", "屏ID");
            //dataGridViewBaseEx_Care.Columns[1].HeaderText = CommonUI.GetCustomMessage(_hashTable, "SnAlia", "屏别名");
            //dataGridViewBaseEx_Care.Columns[2].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Longitude", "经度");
            //dataGridViewBaseEx_Care.Columns[3].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Latitude", "纬度");
            //dataGridViewBaseEx_Care.Columns[4].HeaderText = CommonUI.GetCustomMessage(_hashTable, "MapName", "地图");
            //dataGridViewBaseEx_Care.Columns[5].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Width", "宽度");
            //dataGridViewBaseEx_Care.Columns[6].HeaderText = CommonUI.GetCustomMessage(_hashTable, "Height", "高度");
            //dataGridViewBaseEx_Care.Columns[7].HeaderText = CommonUI.GetCustomMessage(_hashTable, "CareStatus", "注册状态");
            //dataGridViewBaseEx_Care.Columns[8].HeaderText = CommonUI.GetCustomMessage(_hashTable, "ScreenMsg", "屏信息");
        }

        private void LedRegistationInfoCompletedEvent(bool e)
        {
            if (!this.InvokeRequired)
            {
                if (e)
                {
                    InitializeRegistationData();
                }
                CloseProcessForm();
            }
            else
            {
                while (!this.IsHandleCreated)
                {
                    if (this == null || this.IsDisposed)
                    {
                        return;
                    }
                }
                LedRegistationInfoHandle led = new LedRegistationInfoHandle(LedRegistationInfoCompletedEvent);
                this.Invoke(led, new object[] { e });
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {

            MonitorAllConfig.Instance().LedBasicToUIScreen();
            ShowProcessForm(CommonUI.GetCustomMessage(_languageTable, "CareReadData", "注册数据正在获取中，请等待..."), true);
        }

        private void modifyRegistrationButton_Click(object sender, EventArgs e)
        {
            Frm_RegisterScreen registerScreenView = new Frm_RegisterScreen(_screenInfoList);
            registerScreenView.Owner = this;
            if (registerScreenView.ShowDialog() == DialogResult.OK)
            {

            }
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

        private void linkLabel_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.novaicare.com/");
        }

        private void Frm_RegistrationManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            MonitorAllConfig.Instance().LedRegistationInfoEvent -= LedRegistationInfoCompletedEvent;
            MonitorAllConfig.Instance().CareServiceConnectionStatusChangedEvent -= Frm_MonitorConfigManager_CareServiceStatusChanged;
            _topmostTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            _topmostTimer.Dispose();
            if (_instance != null && !_instance.IsDisposed)
            {
                _instance.Dispose();
                _instance = null;
            }
        }
    }

    //public class ScreenSummaryInfo
    //{
    //    public string SN { get; set; }
    //    public string Name { get; set; }
    //    public string Account { get; set; }
    //    public bool IsRegistered { get; set; }
    //}
}
