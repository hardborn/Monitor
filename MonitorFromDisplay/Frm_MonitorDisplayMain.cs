using Nova.Control;
using Nova.Control.Button;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.UI;
using Nova.Monitoring.Common;
using Nova.Monitoring.HardwareMonitorInterface;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.UI.ScannerStatusDisplay;
using Nova.Monitoring.UI.SenderStatusDisplay;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using Nova.Xml.Serialization;
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
    public partial class Frm_MonitorDisplayMain : Frm_CommonBase
    {
        #region 字段

        Frm_MonitorDisplayMain_VM _vm = null;
        UC_SenderStatus _uc_SenderStatus = null;
        List<MarsScreenMonitorDataViewer> _screenMonitorInfoList = new List<MarsScreenMonitorDataViewer>();
        private MonitorConfigData _monitorConfigData = null;
        Dictionary<string, MonitorConfigData> _monitorConfigDataList = new Dictionary<string, MonitorConfigData>();
        private Hashtable _hashTable = null;
        private Hashtable _hashTableStaus = null;
        private bool _isScreenChanged = false;
        private bool _isManualRefresh = false;
        private System.Threading.Timer _topmostTimer = null;
        private List<PopButton> _buttonList;
        private static object _lockScreenChanged = new object();
        private List<LedBasicInfo> _leds = null;
        private SerializableDictionary<string, List<ILEDDisplayInfo>> _allCommDic = null;

        #endregion

        #region 构造函数
        private Frm_MonitorDisplayMain()
        {
            InitializeComponent();
            MonitorAllConfig.Instance().MonitorDataChangedEvent += Frm_MonitorDisplayMain_MonitorDataChangedEvent;
            MonitorAllConfig.Instance().MonitorUIStatusChangedEvent += Frm_MonitorDisplayMain_MonitorUIStatusChangedEvent;
            MonitorAllConfig.Instance().LedScreenChangedEvent += Frm_MonitorDisplayMain_LedScreenChangedEvent;
            MonitorAllConfig.Instance().LedRegistationInfoEvent += Frm_MonitorDisplayMain_LedRegistationInfoEvent;
            MonitorAllConfig.Instance().CareServiceConnectionStatusChangedEvent += Frm_MonitorDisplayMain_CareServiceConnectionStatusChangedEvent;
            MonitorAllConfig.Instance().LedMonitoringConfigChangedEvent += Frm_MonitorDisplayMain_LedMonitoringConfigChangedEvent;
            _vm = new Frm_MonitorDisplayMain_VM();
            uC_MonitorDataListDW.DataGridClickEvent += uC_MonitorDataListDW_DataGridClickEvent;
            _topmostTimer = new System.Threading.Timer(ThreadSetTopMostCallback);
        }

        private static object _lockInstance = new object();
        private static Frm_MonitorDisplayMain _instance = null;
        public static bool IsOpen = false;
        public static Frm_MonitorDisplayMain Instance(bool isOpen)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                lock (_lockInstance)
                {
                    if (_instance == null || _instance.IsDisposed)
                    {
                        _instance = new Frm_MonitorDisplayMain();
                        IsOpen = true;
                        _instance.Show();
                    }
                }
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

        private void ThreadSetTopMostCallback(object state)
        {
            EnableTopMost(false);
        }
        #endregion

        #region 数据事件推送
        /// <summary>
        /// 注册数据更新
        /// </summary>
        /// <param name="obj"></param>
        void Frm_MonitorDisplayMain_LedRegistationInfoEvent(bool obj)
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
                this.Invoke(new MethodInvoker(delegate { Frm_MonitorDisplayMain_LedRegistationInfoEvent(obj); }), null);
                return;
            }

            if (obj == false)
            {
                return;
            }
            lock (_lockScreenChanged)
            {
                List<LedRegistationInfoResponse> registerList = MonitorAllConfig.Instance().LedRegistationUiList;
                foreach (MarsScreenMonitorDataViewer marsScreen in _screenMonitorInfoList)
                {
                    LedRegistationInfoResponse register = registerList.Find(a => a.SN.Replace("-", "") == marsScreen.ScreenSN);
                    if (register != null && !string.IsNullOrEmpty(register.Name))
                    {
                        if (tabControl_MonitorShow.TabPages.ContainsKey(register.SN) && !string.IsNullOrEmpty(register.Name))
                        {
                            tabControl_MonitorShow.TabPages[register.SN].Text = register.Name;
                        }

                        if(_allCommDic.ContainsKey(register.SN))
                        {
                            marsScreen.SetScreenMessage(marsScreen.ScreenSN, register.Name, _allCommDic[register.SN]);
                        }
                    }
                }
            }
        }
        delegate void LedMonitoringConfigChangedEventDel(object sender, EventArgs e);
        /// <summary>
        /// 硬件配置变更事件通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frm_MonitorDisplayMain_LedMonitoringConfigChangedEvent(object sender, EventArgs e)
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
                LedMonitoringConfigChangedEventDel cs = new LedMonitoringConfigChangedEventDel(Frm_MonitorDisplayMain_LedMonitoringConfigChangedEvent);
                this.Invoke(cs, new object[] { sender, e });
            }
            else
            {
                Frm_MonitorDisplayMain_MonitorDataChangedEvent(null, null);
            }
        }

        delegate void CareServiceConnectionStatusChangedHandler(bool status);
        /// <summary>
        /// 注册状态通知
        /// </summary>
        /// <param name="status"></param>
        void Frm_MonitorDisplayMain_CareServiceConnectionStatusChangedEvent(bool status)
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
                CareServiceConnectionStatusChangedHandler cs = new CareServiceConnectionStatusChangedHandler(Frm_MonitorDisplayMain_CareServiceConnectionStatusChangedEvent);
                this.Invoke(cs, new object[] { status });
            }
            else
            {
                if (status)
                {
                    toolStripStatusLabel_Status.Text =
                    CommonUI.GetCustomMessage(_hashTableStaus, "CareLineStatusOK", "Care状态:在线");
                }
                else
                {
                    toolStripStatusLabel_Status.Text =
                    CommonUI.GetCustomMessage(_hashTableStaus, "CareLineStatusNG", "Care状态:离线");
                }
            }
        }

        delegate void MonitorUIStatusChangedHandler(string msg);
        /// <summary>
        /// 状态栏通知
        /// </summary>
        /// <param name="e"></param>
        void Frm_MonitorDisplayMain_MonitorUIStatusChangedEvent(string e)
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
                MonitorUIStatusChangedHandler cs = new MonitorUIStatusChangedHandler(Frm_MonitorDisplayMain_MonitorUIStatusChangedEvent);
                this.Invoke(cs, new object[] { e });
            }
            else
            {
                string[] str = e.Split('|');
                if (str.Length >= 2)
                {
                    string temp = string.Empty;
                    for (int i = 0; i < str.Length; i++)
                    {
                        temp += CommonUI.GetCustomMessage(_hashTableStaus, str[i], str[i]);
                    }
                    toolStripStatusLabel_AutoStatus.Text = temp;
                }
                else
                {
                    toolStripStatusLabel_AutoStatus.Text = CommonUI.GetCustomMessage(_hashTableStaus, e, "");
                }
            }
        }

        delegate void LedScreenChangedEventDel(object sender, EventArgs e);
        /// <summary>
        /// 屏体变更通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frm_MonitorDisplayMain_LedScreenChangedEvent(object sender, EventArgs e)
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
                LedScreenChangedEventDel cs = new LedScreenChangedEventDel(Frm_MonitorDisplayMain_LedScreenChangedEvent);
                this.Invoke(cs, new object[] { sender, e });
            }
            else
            {
                MonitorAllConfig.Instance().WriteLogToFile("UI：LedScreenChangedEvent");
                if (_isScreenChanged == false)
                {
                    _isScreenChanged = true;
                }
                else
                {
                    return;
                }

                lock (_lockScreenChanged)
                {
                    _leds = MonitorAllConfig.Instance().LedInfoList;
                    _allCommDic = MonitorAllConfig.Instance().AllCommPortLedDisplayDic;
                    if (MonitorAllConfig.Instance().IsGetLedInfo)
                    {
                        tableLayoutPanel_MonitorData.Enabled = true;
                        if (_leds == null || _leds.Count == 0)
                        {
                            panel.Enabled = false;
                            foreach (MarsScreenMonitorDataViewer mars in _screenMonitorInfoList)
                            {
                                mars.Dispose();
                            }
                            _screenMonitorInfoList.Clear();
                            tabControl_MonitorShow.TabPages.Clear();
                            _vm.MonitorDataFlags.Clear();
                            uC_MonitorDataListDW.UpdateCurErrInfo(_vm.MonitorDataFlags);
                            StatusClick(0);
                        }
                        else
                        {
                            string tabPageName = string.Empty;
                            if (tabControl_MonitorShow.SelectedTab != null)
                            {
                                tabPageName = tabControl_MonitorShow.SelectedTab.Name;
                                tabControl_MonitorShow.TabPages.Clear();
                                foreach (MarsScreenMonitorDataViewer mars in _screenMonitorInfoList)
                                {
                                    mars.Dispose();
                                }
                                _screenMonitorInfoList.Clear();
                            }
                            _vm.MonitorDataFlags.Clear();
                            uC_MonitorDataListDW.UpdateCurErrInfo(_vm.MonitorDataFlags);
                            //int lastPhysicalDisplayCnt = tabControl_MonitorShow.TabCount;
                            //int curPhysicalDisplayCnt = leds.Count;
                            //int nDiffCnt = curPhysicalDisplayCnt - lastPhysicalDisplayCnt;
                            //if (nDiffCnt < 0)
                            //{
                            //    for (int i = lastPhysicalDisplayCnt - 1; i >= curPhysicalDisplayCnt; i--)
                            //    {
                            //        tabControl_MonitorShow.TabPages.RemoveAt(i);
                            //        _screenMonitorInfoList.RemoveAt(i);
                            //        _vm.MonitorDataFlags.RemoveAt(i);
                            //    }
                            //    UpdateScreenInfos(curPhysicalDisplayCnt, leds, allCommDic);
                            //}
                            //else if (nDiffCnt > 0)
                            //{
                            //    UpdateScreenInfos(lastPhysicalDisplayCnt, leds, allCommDic);
                            //    string displayName = string.Empty;
                            //    int screenIndex = 0;
                            //    for (int i = 0; i < nDiffCnt; i++)
                            //    {
                            //        screenIndex = i + lastPhysicalDisplayCnt;
                            //        if (string.IsNullOrEmpty(leds[screenIndex].AliaName))
                            //        {
                            //            displayName = leds[screenIndex].Commport + "-" + MonitorAllConfig.Instance().ScreenName +
                            //                (Convert.ToInt32(leds[screenIndex].Sn.Split('-')[1]) + 1);
                            //        }
                            //        else
                            //        {
                            //            displayName = leds[screenIndex].AliaName;
                            //        }
                            //        InitializeScanner(leds[screenIndex].Sn, displayName, allCommDic[leds[screenIndex].Sn]);
                            //    }
                            //}
                            //else
                            //{
                            //    UpdateScreenInfos(lastPhysicalDisplayCnt, leds, allCommDic);
                            //}

                            foreach (var led in _leds)
                            {
                                string displayName = string.Empty;
                                if (string.IsNullOrEmpty(led.AliaName))
                                {
                                    displayName = led.Commport + "-" + MonitorAllConfig.Instance().ScreenName +
                                        (Convert.ToInt32(led.Sn.Split('-')[1]) + 1);
                                }
                                else
                                {
                                    displayName = led.AliaName;
                                }
                                InitializeScanner(led.Sn, displayName, MonitorAllConfig.Instance().AllCommPortLedDisplayDic[led.Sn]);
                            }
                            if (tabControl_MonitorShow.SelectedIndex >= 0)
                            {
                                if (tabControl_MonitorShow.SelectedTab.Name != _vm.SN)
                                {
                                    if (tabControl_MonitorShow.SelectedTab.Name != tabPageName &&
                                        tabControl_MonitorShow.TabPages.ContainsKey(tabPageName))
                                    {
                                        tabControl_MonitorShow.SelectTab(tabPageName);
                                    }
                                    else
                                    {
                                        _vm.SN = tabControl_MonitorShow.SelectedTab.Name;
                                        _vm.SN10 = tabControl_MonitorShow.SelectedTab.Text;
                                        _vm.SelectColIndex = 2;
                                        SetBtnEnable(_vm.SN);
                                        SetBtnStatus(popButton_Scanner);
                                    }
                                }
                            }

                            if (_vm.MonitorDataFlags.Count > 0)
                            {
                                _vm.SN = string.Empty;
                                uC_MonitorDataListDW.UpdateCurErrInfo(_vm.MonitorDataFlags);
                                tabControl_MonitorShow_SelectedIndexChanged(null, null);
                            }
                        }
                    }
                }
                _isScreenChanged = false;
            }
        }

        /// <summary>
        /// 单击圆圈的事件
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="colIndex"></param>
        void uC_MonitorDataListDW_DataGridClickEvent(string sn, int colIndex)
        {
            if (colIndex == 0)
            {
                return;
            }
            if (_vm.SN != sn)
            {
                _vm.SN = sn;
                MonitorDataFlag flag = _vm.MonitorDataFlags.Find(a => a.SN == sn);
                if (flag == null)
                {
                    return;
                }
                else
                {
                    _vm.SN10 = flag.SNName;
                }
                if (tabControl_MonitorShow.TabPages.ContainsKey(_vm.SN))
                {
                    tabControl_MonitorShow.SelectTab(_vm.SN);
                }
            }
            if (_vm.SelectColIndex != colIndex)
            {
                _vm.SelectColIndex = colIndex;
            }
            StatusClick(1);
        }

        delegate void MonitorDataChangedHandlerDel(object sender, EventArgs e);
        /// <summary>
        /// 监控数据的更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frm_MonitorDisplayMain_MonitorDataChangedEvent(object sender, EventArgs e)
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
                MonitorDataChangedHandlerDel cs = new MonitorDataChangedHandlerDel(Frm_MonitorDisplayMain_MonitorDataChangedEvent);
                this.Invoke(cs, new object[] { sender, e });
            }
            else
            {
                _stopwatch.Stop();
                MonitorAllConfig.Instance().WriteLogToFile("Montior Main ReceiveData Timer:" + _stopwatch.ElapsedMilliseconds);
                _stopwatch.Start();
                SetDataResult((result) =>
                {
                    if (result)
                    {
                        _vm.IsRefreshDetails = true;
                        if (MonitorAllConfig.Instance().LedInfoList == null ||
                            MonitorAllConfig.Instance().LedInfoList.Count == 0)
                        {
                            _vm.MonitorDataFlags.Clear();
                        }
                        uC_MonitorDataListDW.UpdateCurErrInfo(_vm.MonitorDataFlags);
                        StatusClick(0);
                    }
                    else
                    {
                        if (_isManualRefresh)
                        {
                            _isManualRefresh = false;
                        }
                        if (!_vm.IsGetData)
                        {
                            _vm.MonitorDataFlags.Clear();
                            uC_MonitorDataListDW.UpdateCurErrInfo(_vm.MonitorDataFlags);
                            _uc_SenderStatus.Visible = false;
                            tabControl_MonitorShow.Visible = false;
                        }
                    }
                    _stopwatch.Stop();
                    MonitorAllConfig.Instance().WriteLogToFile("Montior Main Process Timer:" + _stopwatch.ElapsedMilliseconds);
                    CloseProcessForm();
                });
            }
        }
        #endregion

        #region 控件事件
        private void Frm_MonitorDisplayMain_Load(object sender, EventArgs e)
        {
            _buttonList = new List<PopButton>();
            _buttonList.Add(popButton_Sender);
            _buttonList.Add(popButton_Scanner);
            _buttonList.Add(popButton_tem);
            _buttonList.Add(popButton_Monitor);
            _buttonList.Add(popButton_Hum);
            _buttonList.Add(popButton_Smoke);
            _buttonList.Add(popButton_Fan);
            _buttonList.Add(popButton_Power);
            _buttonList.Add(popButton_Cabinet);
            _buttonList.Add(popButton_Cabinet_Door);

            InitializeSender();
            _monitorConfigData = new MonitorConfigData();
            _leds = MonitorAllConfig.Instance().LedInfoList;
            _allCommDic = MonitorAllConfig.Instance().AllCommPortLedDisplayDic;
            if (_leds != null && _leds.Count != 0)
            {
                foreach (var led in _leds)
                {
                    string displayName = string.Empty;
                    if (string.IsNullOrEmpty(led.AliaName))
                    {
                        displayName = led.Commport + "-" + MonitorAllConfig.Instance().ScreenName +
                            (Convert.ToInt32(led.Sn.Split('-')[1]) + 1);
                    }
                    else
                    {
                        displayName = led.AliaName;
                    }
                    InitializeScanner(led.Sn, displayName, _allCommDic[led.Sn]);
                }
                Frm_MonitorDisplayMain_MonitorDataChangedEvent(null, null);
            }
        }
        private void crystalButton_Config_Click(object sender, EventArgs e)
        {
            Frm_MonitorConfigManager frm_monitorConfig = new Frm_MonitorConfigManager();
            frm_monitorConfig.ShowDialog();
        }
        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
        private void crystalButton_MonitorRefresh_Click(object sender, EventArgs e)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            MonitorAllConfig.Instance().RefreshMonitoringData();
            _isManualRefresh = true;
            //ShowProcessForm(CommonUI.GetCustomMessage("ManualRefreshTitle", "重新获取监控数据,请稍候..."), true);
        }
        private void Frm_MonitorDisplayMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_uc_SenderStatus != null)
            {
                _uc_SenderStatus.DisposeAllInfo();
            }
            MonitorAllConfig.Instance().MonitorDataChangedEvent -= Frm_MonitorDisplayMain_MonitorDataChangedEvent;
            MonitorAllConfig.Instance().MonitorUIStatusChangedEvent -= Frm_MonitorDisplayMain_MonitorUIStatusChangedEvent;
            MonitorAllConfig.Instance().LedScreenChangedEvent -= Frm_MonitorDisplayMain_LedScreenChangedEvent;
            MonitorAllConfig.Instance().LedRegistationInfoEvent -= Frm_MonitorDisplayMain_LedRegistationInfoEvent;
            MonitorAllConfig.Instance().CareServiceConnectionStatusChangedEvent -= Frm_MonitorDisplayMain_CareServiceConnectionStatusChangedEvent;
            MonitorAllConfig.Instance().LedMonitoringConfigChangedEvent -= Frm_MonitorDisplayMain_LedMonitoringConfigChangedEvent;
            uC_MonitorDataListDW.DataGridClickEvent -= uC_MonitorDataListDW_DataGridClickEvent;
            _topmostTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            _topmostTimer.Dispose();
            IsOpen = false;
            if (_screenMonitorInfoList != null)
            {
                foreach (MarsScreenMonitorDataViewer mars in _screenMonitorInfoList)
                {
                    mars.Dispose();
                }
                _screenMonitorInfoList.Clear();
            }
            _instance.Dispose();
        }

        private void ResourceDispose()
        {
            _instance = null;
            GC.Collect();
        }

        private void crystalButton_Regist_Click(object sender, EventArgs e)
        {
            OpenRegist();
        }
        private void popButton_Sender_Click(object sender, EventArgs e)
        {
            PopButton pBtn = (PopButton)sender;
            if (pBtn.Name == popButton_Sender.Name)
            {
                _vm.SelectColIndex = 1;
            }
            else if (pBtn.Name == popButton_Scanner.Name)
            {
                _vm.SelectColIndex = 2;
            }
            else if (pBtn.Name == popButton_tem.Name)
            {
                _vm.SelectColIndex = 3;
            }
            else if (pBtn.Name == popButton_Monitor.Name)
            {
                _vm.SelectColIndex = 4;
            }
            else if (pBtn.Name == popButton_Hum.Name)
            {
                _vm.SelectColIndex = 5;
            }
            else if (pBtn.Name == popButton_Smoke.Name)
            {
                _vm.SelectColIndex = 6;
            }
            else if (pBtn.Name == popButton_Fan.Name)
            {
                _vm.SelectColIndex = 7;
            }
            else if (pBtn.Name == popButton_Power.Name)
            {
                _vm.SelectColIndex = 8;
            }
            else if (pBtn.Name == popButton_Cabinet.Name)
            {
                _vm.SelectColIndex = 9;
            }
            else if (pBtn.Name == popButton_Cabinet_Door.Name)
            {
                _vm.SelectColIndex = 10;
            }
            if (tabControl_MonitorShow.TabCount > 0)
            {
                int index = MonitorAllConfig.Instance().LedInfoList.FindIndex(a => a.Sn == _vm.SN);
                uC_MonitorDataListDW.SetSelectedCell(index + 1, _vm.SelectColIndex);
                uC_MonitorDataListDW_DataGridClickEvent(_vm.SN, _vm.SelectColIndex);
            }
        }
        private void popButton_Fan_MouseHover(object sender, EventArgs e)
        {
            this.BringToFront();
            PopButton pBtn = (PopButton)sender;
            if (pBtn.Name == popButton_Sender.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Sender, CommonUI.GetCustomMessage("ColSenderTip", "发送卡"));
            }
            else if (pBtn.Name == popButton_Scanner.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Scanner, CommonUI.GetCustomMessage("ColSBStatusTip", "接收卡状态"));
            }
            else if (pBtn.Name == popButton_tem.Name)
            {
                toolTip_Notice.SetToolTip(popButton_tem, CommonUI.GetCustomMessage("ColTemperatureTip", "温度"));
            }
            else if (pBtn.Name == popButton_Monitor.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Monitor, CommonUI.GetCustomMessage("ColMControlTip", "监控卡状态"));
            }
            else if (pBtn.Name == popButton_Hum.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Hum, CommonUI.GetCustomMessage("ColHumidityTip", "湿度"));
            }
            else if (pBtn.Name == popButton_Smoke.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Smoke, CommonUI.GetCustomMessage("ColSmokeWarnTip", "烟雾"));
            }
            else if (pBtn.Name == popButton_Fan.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Fan, CommonUI.GetCustomMessage("ColFanSpeedTip", "风扇"));
            }
            else if (pBtn.Name == popButton_Power.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Power, CommonUI.GetCustomMessage("ColValtageTip", "电源"));
            }
            else if (pBtn.Name == popButton_Cabinet.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Cabinet, CommonUI.GetCustomMessage("ColRowLineStatusTip", "箱体状态"));
            }
            else if (pBtn.Name == popButton_Cabinet_Door.Name)
            {
                toolTip_Notice.SetToolTip(popButton_Cabinet_Door, CommonUI.GetCustomMessage("ColGeneralStatusTip", "箱门状态"));
            }
        }
        private void popButton_Fan_MouseLeave(object sender, EventArgs e)
        {

        }
        private void popButton_Sender_MouseEnter(object sender, EventArgs e)
        {
            popButton_Fan_MouseHover(sender, e);
            return;
        }
        private void tabControl_MonitorShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_MonitorShow.TabCount > 0)
            {
                if (tabControl_MonitorShow.SelectedIndex == -1 || tabControl_MonitorShow.SelectedTab.Name == _vm.SN)
                {
                    return;
                }
                _vm.SN = tabControl_MonitorShow.SelectedTab.Name;
                if (_vm.SelectColIndex >= 3)
                {
                    if (!IsSelectedIndexEnable()) _vm.SelectColIndex = 2;
                }
                uC_MonitorDataListDW_DataGridClickEvent(_vm.SN, _vm.SelectColIndex);
                uC_MonitorDataListDW.SetSelectedCell(tabControl_MonitorShow.SelectedIndex + 1, _vm.SelectColIndex);
            }
        }
        #endregion

        #region 对外方法
        /// <summary>
        /// 语言
        /// </summary>
        /// <param name="langType"></param>
        /// <param name="proxyLangName"></param>
        /// <returns></returns>
        public bool UpdateLanguage(string langType, string proxyLangName)
        {
            CommonUI.SetLanguage(langType);
            UpdateLanguage();
            return true;
        }
        #endregion

        #region 内部方法

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "Frm_MonitorStatusDisplayInfo", out _hashTable);
            CommonUI.HashTable = _hashTable;
            StaticValue.DisplayTypeStr[0] = CommonUI.GetCustomMessage("ColScreenTip", "屏名称");
            StaticValue.DisplayTypeStr[1] = CommonUI.GetCustomMessage("ColSenderTip", "发送卡");
            StaticValue.DisplayTypeStr[2] = CommonUI.GetCustomMessage("ColSBStatusTip", "接收卡状态");
            StaticValue.DisplayTypeStr[3] = CommonUI.GetCustomMessage("ColTemperatureTip", "温度");
            StaticValue.DisplayTypeStr[4] = CommonUI.GetCustomMessage("ColMControlTip", "监控卡状态");
            StaticValue.DisplayTypeStr[5] = CommonUI.GetCustomMessage("ColHumidityTip", "湿度");
            StaticValue.DisplayTypeStr[6] = CommonUI.GetCustomMessage("ColSmokeWarnTip", "烟雾");
            StaticValue.DisplayTypeStr[7] = CommonUI.GetCustomMessage("ColFanSpeedTip", "风扇");
            StaticValue.DisplayTypeStr[8] = CommonUI.GetCustomMessage("ColValtageTip", "电源");
            StaticValue.DisplayTypeStr[9] = CommonUI.GetCustomMessage("ColRowLineStatusTip", "箱体状态");
            StaticValue.DisplayTypeStr[10] = CommonUI.GetCustomMessage("ColGeneralStatusTip", "箱门状态");
            StaticValue.DisplayTypeStr[11] = CommonUI.GetCustomMessage("ColCareStatusTip", "Care状态");
            StaticValue.ColConTextTip = CommonUI.GetCustomMessage("ColConTextTip", "单击查看详情");
            uC_MonitorDataListDW.UpdateLanguage(CommonUI.GetCustomMessage("ColScreenTip", "屏名称"));
            MonitorAllConfig.Instance().ALLScreenName = CommonUI.GetCustomMessage("AllScreen", "所有屏");
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "Frm_MonitorStatusDisplayInfo_Status", out _hashTableStaus);

            if (_uc_SenderStatus != null)
            {
                _uc_SenderStatus.UpdateLanguage(_hashTable);
            }
            foreach (TabPage tp in tabControl_MonitorShow.TabPages)
            {
                LedBasicInfo led = MonitorAllConfig.Instance().LedInfoList.Find(a => a.Sn == tp.Name);
                if (string.IsNullOrEmpty(led.AliaName))
                {
                    tp.Text = led.Commport + "-" + MonitorAllConfig.Instance().ScreenName + (led.LedIndexOfCom + 1);
                }
            }
            foreach (var led in _screenMonitorInfoList)
            {
                led.UpdateLanguage(_hashTable);
            }
            if (!MonitorAllConfig.Instance().IsGetLedInfo)
            {
                this.Text = "MonitorSite" +
                    " " + MonitorAllConfig.Instance().MonitorDisplayVersion +
                    CommonUI.GetCustomMessage("Frm_MonitorDisplayMainTextNoData", "(获取屏体数据中...)");
                tableLayoutPanel_MonitorData.Enabled = false;
            }
            else
            {
                this.Text = "MonitorSite" +
                        " " + MonitorAllConfig.Instance().MonitorDisplayVersion;
                Frm_MonitorDisplayMain_CareServiceConnectionStatusChangedEvent(
                    MonitorAllConfig.Instance().CareServiceConnectionStatus);
            }
        }

        #region 屏体变更方法

        private void UpdateScreenInfos(int screenCount, List<LedBasicInfo> leds, SerializableDictionary<string, List<ILEDDisplayInfo>> allCommDic)
        {
            string displayName = string.Empty;
            for (int i = 0; i < screenCount; i++)
            {
                if (string.IsNullOrEmpty(leds[i].AliaName))
                {
                    displayName = leds[i].Commport + "-" + MonitorAllConfig.Instance().ScreenName + (Convert.ToInt32(leds[i].Sn.Split('-')[1]) + 1);
                }
                else
                {
                    displayName = leds[i].AliaName;
                }
                if (tabControl_MonitorShow.TabPages.ContainsKey(leds[i].Sn))
                {
                    tabControl_MonitorShow.TabPages[leds[i].Sn].Name = leds[i].Sn + ".";
                }
                tabControl_MonitorShow.TabPages[i].Name = leds[i].Sn;
                tabControl_MonitorShow.TabPages[i].Text = displayName;

                MonitorDataFlag monitorFlag = _vm.MonitorDataFlags.Find(a => a.SN == leds[i].Sn);
                if (monitorFlag != null)
                {
                    _vm.MonitorDataFlags[i] = monitorFlag;
                    _vm.MonitorDataFlags[i].SN = leds[i].Sn;
                    _vm.MonitorDataFlags[i].SNName = displayName;
                }
                else if (i < _vm.MonitorDataFlags.Count)
                {
                    _vm.MonitorDataFlags[i].SN = leds[i].Sn;
                    _vm.MonitorDataFlags[i].SNName = displayName;
                }

                _screenMonitorInfoList[i].SetScreenMessage(leds[i].Sn.Replace("-", ""), displayName,
                    allCommDic[leds[i].Sn]);
                Application.DoEvents();
            }
        }

        public void InitializeSender()
        {
            _uc_SenderStatus = new UC_SenderStatus();
            _uc_SenderStatus.Parent = panel_MonitorShow;
            _uc_SenderStatus.Dock = DockStyle.Fill;
            _uc_SenderStatus.BorderStyle = BorderStyle.FixedSingle;
            _uc_SenderStatus.CustomToolTipFont = this.Font;
            _uc_SenderStatus.CommPortFont = this.Font;
            _uc_SenderStatus.SenderNumFont = this.Font;
            //_uc_SenderStatus.UpdateLanguage(CommonUI.HashTable);
        }
        public void InitializeScanner(string sn, string displayName, List<ILEDDisplayInfo> ledInfo)
        {
            if (tabControl_MonitorShow.TabPages.ContainsKey(sn))
            {
                return;
            }
            MarsScreenMonitorDataViewer oneScreenMonitorInfo = new MarsScreenMonitorDataViewer();
            tabControl_MonitorShow.TabPages.Add(displayName);
            tabControl_MonitorShow.TabPages[tabControl_MonitorShow.TabPages.Count - 1].Name = sn;
            oneScreenMonitorInfo.Parent = tabControl_MonitorShow.TabPages[tabControl_MonitorShow.TabPages.Count - 1];
            oneScreenMonitorInfo.Dock = DockStyle.Fill;
            oneScreenMonitorInfo.CtrlOfFormFont = this.Font;
            oneScreenMonitorInfo.CustomToolTipFont = this.Font;
            oneScreenMonitorInfo.SimpleOrStandardScreenFont = this.Font;
            oneScreenMonitorInfo.ComplexScreenFont = this.Font;
            _screenMonitorInfoList.Add(oneScreenMonitorInfo);
            tabControl_MonitorShow.TabPages[sn].Text = displayName;
            oneScreenMonitorInfo.SetScreenMessage(sn.Replace("-", ""), displayName, ledInfo);
            //_vm.MonitorDataFlags.Add(new MonitorDataFlag()
            //{
            //    SN = sn,
            //    SNName = displayName
            //});
            Application.DoEvents();
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            oneScreenMonitorInfo.UpdateLanguage(_hashTable);
        }
        #endregion

        #region 控件样式变化
        private void SetBtnStatus(PopButton pButton)
        {
            if (_buttonList == null)
            {
                return;
            }
            foreach (var btn in _buttonList)
            {
                if (btn.Name == pButton.Name)
                {
                    btn.ButtonBorderStyle = ButtonBorderStyleType.Groove;
                }
                else
                {
                    btn.ButtonBorderStyle = ButtonBorderStyleType.Ridge;
                }
            }
        }
        private void SetBtnEnable(string sn)
        {
            panel.Enabled = true;
            var cfgInfo = _vm.MonitorDataFlags.Find(a => a.SN == sn);
            if (cfgInfo == null)
            {
                popButton_Sender.Enabled = false;
                popButton_Scanner.Enabled = false;
                cfgInfo = new MonitorDataFlag();
                cfgInfo.IsTemperatureValid = DeviceWorkStatus.UnAvailable;
            }
            else
            {
                popButton_Sender.Enabled = true;
                popButton_Scanner.Enabled = true;
            }
            if (cfgInfo.IsMCStatusValid == DeviceWorkStatus.UnAvailable)
                popButton_Monitor.Enabled = false;
            else popButton_Monitor.Enabled = true;
            if (cfgInfo.IsFanValid == DeviceWorkStatus.UnAvailable)
                popButton_Fan.Enabled = false;
            else popButton_Fan.Enabled = true;
            if (cfgInfo.IsRowLineValid == DeviceWorkStatus.UnAvailable)
                popButton_Cabinet.Enabled = false;
            else popButton_Cabinet.Enabled = true;
            if (cfgInfo.IsHumidityValid == DeviceWorkStatus.UnAvailable)
                popButton_Hum.Enabled = false;
            else popButton_Hum.Enabled = true;
            if (cfgInfo.IsPowerValid == DeviceWorkStatus.UnAvailable)
                popButton_Power.Enabled = false;
            else popButton_Power.Enabled = true;
            if (cfgInfo.IsTemperatureValid == DeviceWorkStatus.UnAvailable)
                popButton_tem.Enabled = false;
            else popButton_tem.Enabled = true;
            if (cfgInfo.IsSmokeValid == DeviceWorkStatus.UnAvailable)
                popButton_Smoke.Enabled = false;
            else popButton_Smoke.Enabled = true;
            if (cfgInfo.IsGeneralStatusValid == DeviceWorkStatus.UnAvailable)
                popButton_Cabinet_Door.Enabled = false;
            else popButton_Cabinet_Door.Enabled = true;
        }
        private bool IsSelectedIndexEnable()
        {
            var cfgInfo = _vm.MonitorDataFlags.Find(a => a.SN == _vm.SN);
            if (cfgInfo == null) return true;
            if (_vm.SelectColIndex == 0 || _vm.SelectColIndex == 11) return false;
            if (_vm.SelectColIndex == 3)
            {
                if (cfgInfo.IsTemperatureValid == DeviceWorkStatus.UnAvailable) return false;
                else return true;
            }
            if (_vm.SelectColIndex == 4)
            {
                if (cfgInfo.IsMCStatusValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 5)
            {
                if (cfgInfo.IsHumidityValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 6)
            {
                if (cfgInfo.IsSmokeValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 7)
            {
                if (cfgInfo.IsFanValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 8)
            {
                if (cfgInfo.IsPowerValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 9)
            {
                if (cfgInfo.IsRowLineValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            if (_vm.SelectColIndex == 10)
            {
                if (cfgInfo.IsGeneralStatusValid == DeviceWorkStatus.UnAvailable)
                    return false;
                else return true;
            }
            return true;
        }

        private MonitorDisplayType GetMonitorType(int index)
        {
            return (MonitorDisplayType)Enum.Parse(typeof(MonitorDisplayType), (index - 2).ToString());
        }
        private void OpenRegist()
        {
            if (!this.InvokeRequired)
            {
                MonitorFromDisplay.Frm_RegistrationManager.Instance(true).EnableTopMost(true);
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).ApplyUILanguageTable();
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).UpdateFont(CommonUI.SoftwareFont);
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).Activate();
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate { OpenRegist(); }));
            }
        }

        #endregion

        #region 数据来了更新界面

        private void SetDataResult(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.OnCmdInitialize();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                while (!this.IsHandleCreated)
                {
                    if (this == null || this.IsDisposed)
                    {
                        return;
                    }
                }
                this.Invoke(callBackAction, new object[] { result });
            }, null);
        }
        private MonitorConfigData SetMonitorConfigData(string sn)
        {
            MonitorConfigData monitorConfigData = new MonitorConfigData();
            LedAlarmConfig ledAlarm = MonitorAllConfig.Instance().LedAlarmConfigs.Find(a => a.SN == sn);
            if (ledAlarm==null || ledAlarm.ParameterAlarmConfigList == null || ledAlarm.ParameterAlarmConfigList.Count == 0)
            {
                return monitorConfigData;
            }
            foreach (ParameterAlarmConfig param in ledAlarm.ParameterAlarmConfigList)
            {
                if (param != null)
                {
                    switch (param.ParameterType)
                    {
                        case StateQuantityType.Temperature:
                            monitorConfigData.TempAlarmThreshold = (float)param.HighThreshold;
                            break;
                        case StateQuantityType.Humidity:
                            monitorConfigData.HumiAlarmThreshold = (float)param.HighThreshold;
                            break;
                        case StateQuantityType.Voltage:
                            if (param.Level == AlarmLevel.Warning)
                            {
                                monitorConfigData.PowerAlarmValue = (float)param.LowThreshold;
                            }
                            else if (param.Level == AlarmLevel.Malfunction)
                            {
                                monitorConfigData.PowerFaultValue = (float)param.HighThreshold;
                            }
                            break;
                        case StateQuantityType.FanSpeed:
                            monitorConfigData.FanSpeed = (float)param.LowThreshold;
                            break;
                    }
                }
            }

            LedMonitoringConfig ledMonitorCfg = MonitorAllConfig.Instance().LedMonitorConfigs.Find(a => a.SN == sn);
            if (ledMonitorCfg == null || ledMonitorCfg.MonitoringCardConfig == null || ledMonitorCfg.SN == null)
            {
                return monitorConfigData;
            }
            if (ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable == null || ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Count == 0)
            {
                return monitorConfigData;
            }
            ParameterMonitoringConfig cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.FanSpeed));
            monitorConfigData.MCFanInfo = new ScannerStatusDisplay.ScanBdMonitoredParamUpdateInfo();
            if (cfg != null && cfg.MonitoringEnable)
            {
                if (cfg.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                {
                    monitorConfigData.MCFanInfo.CountType =
                        Nova.Monitoring.UI.ScannerStatusDisplay.ScanBdMonitoredParamCountType.SameForEachScanBd;
                    monitorConfigData.MCFanInfo.AlarmThreshold = (int)monitorConfigData.FanSpeed;
                    monitorConfigData.MCFanInfo.SameCount = (byte)cfg.GeneralExtendedConfig;
                }
                else if (cfg.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                {
                    monitorConfigData.MCFanInfo.CountType =
                        Nova.Monitoring.UI.ScannerStatusDisplay.ScanBdMonitoredParamCountType.DifferentForEachScanBd;
                    monitorConfigData.MCFanInfo.AlarmThreshold = (int)monitorConfigData.FanSpeed;
                    monitorConfigData.MCFanInfo.CountDicOfScanBd = new SerializableDictionary<string, byte>();
                    foreach (ParameterExtendedConfig param in cfg.ExtendedConfig)
                    {
                        monitorConfigData.MCFanInfo.CountDicOfScanBd.Add(param.ReceiveCardId, (byte)param.ParameterCount);
                    }
                }
            }
            cfg = ledMonitorCfg.MonitoringCardConfig.ParameterConfigTable.Find(a => a.Type.Equals(StateQuantityType.Voltage));
            monitorConfigData.MCPowerInfo = new ScannerStatusDisplay.ScanBdMonitoredPowerInfo();
            if (cfg != null)
            {
                if (cfg != null && cfg.MonitoringEnable)
                {
                    if (cfg.ConfigMode == ParameterConfigMode.GeneralExtendedMode)
                    {
                        monitorConfigData.MCPowerInfo.CountType =
                            Nova.Monitoring.UI.ScannerStatusDisplay.ScanBdMonitoredParamCountType.SameForEachScanBd;
                        monitorConfigData.MCPowerInfo.SameCount = (byte)cfg.GeneralExtendedConfig;
                        monitorConfigData.MCPowerInfo.AlarmThreshold = monitorConfigData.PowerAlarmValue;
                        monitorConfigData.MCPowerInfo.FaultThreshold = monitorConfigData.PowerFaultValue;
                    }
                    else if (cfg.ConfigMode == ParameterConfigMode.AdvanceExtendedMode)
                    {
                        monitorConfigData.MCPowerInfo.CountType =
                            Nova.Monitoring.UI.ScannerStatusDisplay.ScanBdMonitoredParamCountType.DifferentForEachScanBd;
                        monitorConfigData.MCPowerInfo.AlarmThreshold = monitorConfigData.PowerAlarmValue;
                        monitorConfigData.MCPowerInfo.FaultThreshold = monitorConfigData.PowerFaultValue;
                        monitorConfigData.MCPowerInfo.CountDicOfScanBd = new SerializableDictionary<string, byte>();
                        foreach (ParameterExtendedConfig param in cfg.ExtendedConfig)
                        {
                            monitorConfigData.MCPowerInfo.CountDicOfScanBd.Add(param.ReceiveCardId, (byte)param.ParameterCount);
                        }
                    }
                }
            }
            return monitorConfigData;
        }
        delegate void StatusClickHandler(int isClick);
        private void StatusClick(int isClick)
        {
            if (this.InvokeRequired)
            {
                StatusClickHandler cs = new StatusClickHandler(StatusClick);
                this.Invoke(cs, new object[] { isClick });
                return;
            }
            SetBtnEnable(_vm.SN);
            switch (_vm.SelectColIndex)
            {
                case 0:
                    break;
                case 1:
                    SetBtnStatus(popButton_Sender);
                    _uc_SenderStatus.Visible = true;
                    tabControl_MonitorShow.Visible = false;
                    if (string.IsNullOrEmpty(_vm.SN) || _vm.ScreenMonitorData == null
                        || !_vm.ScreenMonitorData.MonitorResInfDic.ContainsKey(_vm.SN))
                    {
                        _uc_SenderStatus.Visible = false;
                        return;
                    }
                    _uc_SenderStatus.UpdateSenderStatus(
                        _vm.ScreenMonitorData.CurAllSenderStatusDic,
                        _vm.ScreenMonitorData.CurAllSenderDVIDic,
                        _vm.ScreenMonitorData.RedundantStateType,
                        _vm.ScreenMonitorData.CommPortData,
                        _vm.ScreenMonitorData.TempRedundancyDict);
                    break;
                case 11:
                    if (isClick == 1)
                    {
                        OpenRegist();
                    }
                    break;
                default:
                    _uc_SenderStatus.Visible = false;
                    tabControl_MonitorShow.Visible = true;
                        MonitorDisplayType mtype = GetMonitorType(_vm.SelectColIndex);
                        switch (mtype)
                        {
                            case MonitorDisplayType.ExternalDevice:
                                break;
                            case MonitorDisplayType.Fan:
                                SetBtnStatus(popButton_Fan);
                                break;
                            case MonitorDisplayType.GeneralSwitch:
                                SetBtnStatus(popButton_Cabinet_Door);
                                break;
                            case MonitorDisplayType.Humidity:
                                SetBtnStatus(popButton_Hum);
                                break;
                            case MonitorDisplayType.MCStatus:
                                SetBtnStatus(popButton_Monitor);
                                break;
                            case MonitorDisplayType.Power:
                                SetBtnStatus(popButton_Power);
                                break;
                            case MonitorDisplayType.RowLine:
                                SetBtnStatus(popButton_Cabinet);
                                break;
                            case MonitorDisplayType.SBStatus:
                                SetBtnStatus(popButton_Scanner);
                                break;
                            case MonitorDisplayType.SenderDVI:
                                break;
                            case MonitorDisplayType.Smoke:
                                SetBtnStatus(popButton_Smoke);
                                break;
                            case MonitorDisplayType.Temperature:
                                SetBtnStatus(popButton_tem);
                                break;
                        }
                    if (string.IsNullOrEmpty(_vm.SN) || _vm.ScreenMonitorData == null
                        || !_vm.ScreenMonitorData.MonitorResInfDic.ContainsKey(_vm.SN))
                    {
                        tabControl_MonitorShow.Visible = false;
                        return;
                    }

                    MarsScreenMonitorDataViewer mars = _screenMonitorInfoList.Find(a => a.ScreenSN == _vm.SN.Replace("-", ""));
                    if (mars != null)
                    {
                        mars.CurMonitorConfigInfo = SetMonitorConfigData(_vm.SN);
                        mars.UpdateMonitorInfo(
                            _vm.ScreenMonitorData.MonitorResInfDic[_vm.SN], _vm.SN.Replace("-", ""),
                            _vm.ScreenMonitorData.MonitorDataDic[_vm.SN]);

                        int curRatioZoom = 40;

                        if (mtype != MonitorDisplayType.Power
                            && mtype != MonitorDisplayType.Fan)
                        {
                            curRatioZoom = mars.CurZoomRation;
                        }
                        mars.CurType = mtype;
                        if (mtype != MonitorDisplayType.Power
                            && mtype != MonitorDisplayType.Fan)
                        {
                            mars.CurZoomRation = curRatioZoom;
                        }
                        mars.RefreshDisplayAndStatisticsInfo();
                    }
                    break;
            }
        }
        #endregion

        #endregion
    }
}