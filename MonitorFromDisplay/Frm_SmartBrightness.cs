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
    public partial class Frm_SmartBrightness : Frm_CommonBase
    {
        private System.Threading.Timer _topmostTimer = null;
        Hashtable _hashtable = null;
        private string _sn = string.Empty;
        private string _snName = string.Empty;

        public Frm_SmartBrightness()
        {
            InitializeComponent();
            crystalButton_Cancel.BringToFront();
            _topmostTimer = new System.Threading.Timer(ThreadSetTopMostCallback);
            uC_BrightnessConfigExample.CloseFormHandler += uC_BrightnessConfigExample_CloseFormHandler;
        }

        void uC_BrightnessConfigExample_CloseFormHandler(bool obj)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { uC_BrightnessConfigExample_CloseFormHandler(obj); }));
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool _allConfig = false;
        public bool AllConfig
        {
            get
            {
                return _allConfig;
            }
            set
            {
                _allConfig = value;
                SetUI();
            }
        }

        private void Frm_SmartBrightness_Load(object sender, EventArgs e)
        {
            uC_BrightnessExample.Visible = false;
            uC_BrightnessConfigExample.Visible = true;
            this.Width = 750;
        }
                
        private void SetUI()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { SetUI(); }));
                return;
            }
            if (_allConfig)
            {
                uC_BrightnessExample.Visible = true;
                uC_BrightnessExample.UpdateLang();
                uC_BrightnessConfigExample.Visible = false;
                this.Width = 500;
            }
            else
            {
                uC_BrightnessExample.Visible = false;
                uC_BrightnessConfigExample.Visible = true;
                this.Width = 750;
            }
        }

        public void SetSNInitialize(string sn)
        {
            string[] str = sn.Split('●');
            if (str.Length == 3)
            {
                _snName = str[0] + "-" + MonitorAllConfig.Instance().ScreenName + (int.Parse(str[1]) + 1);
                _sn = str[2];
            }
            if (string.IsNullOrEmpty(_sn))
            {
                ShowCustomMessageBox(CommonUI.GetCustomMessage(_hashtable, "GetSNFailed", "获取屏体的SN信息失败:监控中不存在此屏信息"), "",
                    MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
            }
            else
            {
                SendSNMessage(_sn);
            }
        }

        private void SendSNMessage(string sn)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>
                (sn, Nova.Monitoring.MonitorDataManager.MsgToken.MSG_BrightnessConfig);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UpdateLanguage(string langType, string proxyLangName)
        {
            CommonUI.SetLanguage(langType);
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "Frm_SmartBrightness_String", out _hashtable);
        }

        private static object _lockInstance = new object();
        private static Frm_SmartBrightness _instance = null;
        public static Frm_SmartBrightness Instance(bool isOpen)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                lock (_lockInstance)
                {
                    if (_instance == null || _instance.IsDisposed)
                    {
                        _instance = new Frm_SmartBrightness();
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

        private void Frm_SmartBrightness_FormClosing(object sender, FormClosingEventArgs e)
        {
            uC_BrightnessConfigExample.CloseFormHandler -= uC_BrightnessConfigExample_CloseFormHandler;
            _topmostTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            _topmostTimer.Dispose();
            if (_instance != null && !_instance.IsDisposed)
            {
                _instance.Dispose();
                _instance = null;
            }
        }
    }
}
