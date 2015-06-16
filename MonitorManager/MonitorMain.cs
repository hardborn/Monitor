using Nova.Diagnostics;
using Nova.LCT.GigabitSystem.Language;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.UI.MonitorFromDisplay;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorManager
{
    public partial class MonitorMain : Form
    {
        private static object lockobj = new object();
        string langType = "zh-CN";
        private Hashtable _errorFrmHashTable = null;

        private readonly string STR_LANG_ZHCN = "zh-ch";
        private readonly string STR_LANG_KOKR = "ko-kr";
        private Font _zhchFont = new Font("Arial", 8.5f);
        private Font _kokrFont = new Font("Arial", 8.5f);
        private Font _enFont = new Font("Arial", 8.5f);
        private Font _softwareFont = null;
        private string _monitorVersion = string.Empty;


        public MonitorMain(bool isOpenMain)
        {
            MonitorAllConfig.Instance().WriteLogToFile("开始初始化主界面...");
            Initialize();
            MonitorAllConfig.Instance().WriteLogToFile("初始化主界面完成...");
            if (isOpenMain)
            {
                OpenMainUI(string.Empty);
                MonitorAllConfig.Instance().WriteLogToFile("打开了主界面...");
            }
            MonitorAllConfig.Instance().WriteLogToFile("初始化主界面构造完成...");
        }

        private void Initialize()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            InitializeComponent();

            System.Drawing.Text.InstalledFontCollection fc = new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily font in fc.Families)
            {
                if (font.Name == "宋体")
                {
                    _zhchFont = new Font("宋体", 9);
                }

                if (font.Name == "Batang")
                {
                    _kokrFont = new Font("Batang", 8.5f);
                }
            }
            //初始化非UI异常捕获事件,该函数执行后,非UI异常由ExceptionHandle接管
            //ExceptionHandle.InitializeNonUIUnhandledExceptionHandler();
            //设置异常被ExceptionHandle捕获后的委托函数
            //ExceptionHandle.UIExceptionEvent += new ExceptionHandle.ExceptionEventHandler(UIExceptionHandlerDelegate);
            //ExceptionHandle.NonUIExceptionEvent += new ExceptionHandle.ExceptionEventHandler(NonUIExceptionHandlerDelegate);
            StartService();
        }

        private void MonitorMain_Load(object sender, EventArgs e)
        {
            MonitorMain_FormClosing(this, new FormClosingEventArgs(CloseReason.None, false));
        }

        private void MonitorMain_OpenUIHandlerEvent(string key, string value)
        {
            Action action = null;
            switch (key)
            {
                case "OpenMonitoring":
                    action = new Action(() =>
                    {
                        Debug.WriteLine("监控收到：打开主窗体内容");
                        OpenMainUI(string.Empty);
                    });
                    action.BeginInvoke(null, null);
                    break;
                case "OpenSmartBrightness":
                    action = new Action(() =>
                    {
                        OpenSmartBrightnessUI(value, false);
                    });
                    action.BeginInvoke(null, null);
                    break;
                case "OpenRegist":
                    action = new Action(() =>
                    {
                        OpenRegist(string.Empty);
                    });
                    action.BeginInvoke(null, null);
                    break;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateLanguage();
            if (e.Button == MouseButtons.Left)
            {
                //Rectangle rect = System.Windows.Forms.SystemInformation.WorkingArea;
                //int x = rect.Width - _displayInfoFrm.Width - 2;
                //int y = rect.Height - _displayInfoFrm.Height - 4;
            }
            else
            {
                //_displayInfoFrm.Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenMainUI(string.Empty);
        }

        private void toolStripMenuItem_HeartbeatConfig_Click(object sender, EventArgs e)
        {
            OpenMainUI(string.Empty);
        }


        private delegate void OpenMainClickHanlder(string param);
        private void OpenMainUI(string param)
        {
            if (!this.InvokeRequired)
            {
                UpdateLanguage();
                MonitorFromDisplay.Frm_MonitorDisplayMain.Instance(true).EnableTopMost(true);
                MonitorFromDisplay.Frm_MonitorDisplayMain.Instance(false).UpdateLanguage(langType, CommonInfo.ProtocalLangFileName);
                MonitorFromDisplay.Frm_MonitorDisplayMain.Instance(false).UpdateFont(_softwareFont);
                MonitorFromDisplay.Frm_MonitorDisplayMain.Instance(false).Activate();
            }
            else
            {
                OpenMainClickHanlder oh = new OpenMainClickHanlder(OpenMainUI);
                this.Invoke(oh, new object[] { param });
            }
        }

        private void OpenSmartBrightnessUI(string param, bool isAllConfig)
        {
            if (!this.InvokeRequired)
            {
                UpdateLanguage();
                MonitorFromDisplay.Frm_SmartBrightness.Instance(true).EnableTopMost(true);
                MonitorFromDisplay.Frm_SmartBrightness.Instance(false).UpdateLanguage(langType, CommonInfo.ProtocalLangFileName);
                MonitorFromDisplay.Frm_SmartBrightness.Instance(false).UpdateFont(_softwareFont);
                if (isAllConfig)
                {
                    MonitorFromDisplay.Frm_SmartBrightness.Instance(false).AllConfig = true;
                }
                else
                {
                    MonitorFromDisplay.Frm_SmartBrightness.Instance(false).AllConfig = false;
                    MonitorFromDisplay.Frm_SmartBrightness.Instance(false).SetSNInitialize(param);
                }
                MonitorFromDisplay.Frm_SmartBrightness.Instance(false).Activate();
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate { OpenSmartBrightnessUI(param, isAllConfig); }));
            }
        }
        private void OpenRegist(string param)
        {
            if (!this.InvokeRequired)
            {
                UpdateLanguage();
                MonitorFromDisplay.Frm_RegistrationManager.Instance(true).EnableTopMost(true);
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).ApplyUILanguageTable(langType);
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).UpdateFont(_softwareFont);
                MonitorFromDisplay.Frm_RegistrationManager.Instance(false).Activate();
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate { OpenRegist(param); }));
            }
        }
        private void toolStripMenuItem_MonitorConfig_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_OpenBrightAllConfig_Click(object sender, EventArgs e)
        {
            OpenSmartBrightnessUI(string.Empty, true);
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            MonitorMain_FormClosing(sender, new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        /// <summary>
        /// 更新语言资源
        /// </summary>
        private void UpdateLanguage()
        {
            try
            {
                if (File.Exists(CommonInfo.LangTypeFileName))
                {
                    File.Open(CommonInfo.LangTypeFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    langType = File.ReadAllText(CommonInfo.LangTypeFileName);
                    OutputDebugInfoStr(false, "获取语言资源类型OK,类型为：" + langType);
                }
            }
            catch (Exception ex)
            {
                langType = "zh-CN";
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch：获取语言资源类型异常:"+ex.ToString());
                OutputDebugInfoStr(false, "获取语言资源类型异常:" + ex.Message);
            }

            CommonInfo.ProtocalLangFileName = CommonInfo.AppLangPath + langType + "\\Protocal." + langType + ".resources";
            CommonInfo.LanFileName = CommonInfo.AppLangPath + langType + "\\Frm_MonitorDisplay." + langType + ".resources";
            Nova.Monitoring.UI.MonitorFromDisplay.CommonUI.SetLanguage(langType);
            MultiLanguageUtils.ReadLanguageResource(CommonInfo.LanFileName, "Frm_MonitorMain", out LangHashTable);
            toolStripMenuItem_OpenMain.Text = GetLangControlText("toolStripMenuItem_OpenMain", "打开用户界面(&O)");
            ToolStripMenuItem_Exit.Text = GetLangControlText("ToolStripMenuItem_Exit", "退出(&Q)");
            toolStripMenuItem_OpenBrightAllConfig.Text = GetLangControlText("toolStripMenuItem_OpenBrightAllConfig", "亮度高级配置");
            toolStripMenuItem_ReReadScreen.Text = GetLangControlText("toolStripMenuItem_ReReadScreen", "重读屏体");
            //notifyIcon.Text = GetLangControlText("notifyIcon", "监控终端平台");
            MultiLanguageUtils.ReadLanguageResource(CommonInfo.LanFileName, "Form_ErrorNotice", out _errorFrmHashTable);
            ProtocalLangParser = new ProtocalLanguageParser(CommonInfo.ProtocalLangFileName);
            //MultiLanguageUtils.ReadLanguageResource(CommonInfo.LanFileName, "Frm_MonitorStatusDisplayInfo", out LangHashTable);
            //MultiLanguageUtils.ReadLanguageResource(CommonInfo.ProtocalLangFileName, "ProtocolEnum_Interface", out ProtocalHashTable);
            CustomMessageBox.LangFileName = CommonInfo.LanFileName;
            Hashtable lang;
            MultiLanguageUtils.ReadLanguageResource(CommonInfo.LanFileName, "EMailNotify_String", out lang);
            MonitorAllConfig.Instance().EMailLangHsTable = lang;
            MonitorAllConfig.Instance().ScreenName = GetLangControlText("Screen", "屏");
            MonitorAllConfig.Instance().WriteLogToFile("ScreenName Language:"+MonitorAllConfig.Instance().ScreenName);
            Font oldFont = _softwareFont;

            if (langType.ToLower() == STR_LANG_ZHCN.ToLower())
            {
                _softwareFont = _zhchFont;
            }
            else if (langType.ToLower() == STR_LANG_KOKR.ToLower())
            {
                _softwareFont = _kokrFont;
            }
            else
            {
                _softwareFont = _enFont;
            }

            if (MonitorFromDisplay.Frm_MonitorDisplayMain.IsOpen)
            {
                MonitorFromDisplay.Frm_MonitorDisplayMain.Instance(false).UpdateLanguage(langType, CommonInfo.ProtocalLangFileName);
            }

            UpdateFont(_softwareFont);
        }

        private bool UpdateFont(Font font)
        {
            CustomMessageBox.WindowFont = font;
            return true;
        }

        #region 异常捕获处理
        /// <summary>
        /// UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        /// <param name="exText"></param>
        private void UIExceptionHandlerDelegate(object sender, Exception ex, string exText)
        {
            SaveExceptionInfo(ex.ToString());
            Form_ErrorNotice frm;
            InitErrorWindow(exText, true, out frm);
            if (frm.ShowDialog(this) == DialogResult.Abort)
            {
                System.Environment.Exit(0);
                Process.GetCurrentProcess().Kill();
            }
        }
        /// <summary>
        ///  Non UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ExceptionMsg"></param>
        private void NonUIExceptionHandlerDelegate(object sender, Exception ex, string exText)
        {
            SaveExceptionInfo(ex.ToString());
            Action action = new Action(() =>
            {
                System.Threading.Thread.Sleep(4000);
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            });
            action.BeginInvoke(null, null);
            //Form_ErrorNotice frm;
            //InitErrorWindow(exText, false, out frm);
            //frm.ShowDialog();
        }

        void UIThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SaveExceptionInfo(e.Exception.ToString());
            Form_ErrorNotice frm;
            InitErrorWindow(e.Exception.Message, true, out frm);
            if (frm.ShowDialog(this) == DialogResult.Abort)
            {
                System.Environment.Exit(0);
            }
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            SaveExceptionInfo(e.ExceptionObject.ToString());
            //Process.Start(Application.ExecutablePath);
            //Process.GetCurrentProcess().Kill();
            Action action = new Action(() =>
            {
                System.Threading.Thread.Sleep(4000);
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            });
            action.BeginInvoke(null, null);
        }

        /// <summary>
        /// 保存异常日志
        /// </summary>
        /// <param name="exceptionMsg"></param>
        private void SaveExceptionInfo(string exceptionMsg)
        {
            if (!Directory.Exists(Path.GetDirectoryName(CommonInfo.ERRORLOGPATH)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CommonInfo.ERRORLOGPATH));
            }
            string fileName = CommonInfo.ERRORLOGPATH + DateTime.Now.ToString("yy-MM-dd") + ".log";
            TextWriterTraceListener textWrite = new TextWriterTraceListener(fileName, "ExceptionLog");
            textWrite.Flush();
            textWrite.WriteLine(DateTime.Now.ToString() + "监控平台出现异常--------------------------------");
            textWrite.WriteLine(exceptionMsg);
            textWrite.Flush();
            textWrite.Close();
        }

        private void InitErrorWindow(string ExceptionMsg, bool isAlert, out Form_ErrorNotice errorNotifyWnd)
        {
            Form_ErrorNotice ErrorNoticeDlg = new Form_ErrorNotice();
            ErrorNoticeDlg.bAlert = isAlert;
            ErrorNoticeDlg.ExceptionMsg = ExceptionMsg;
            ErrorNoticeDlg.SoftwareName = this.Text;
            ErrorNoticeDlg.RecipientEmailList.Add("nova_lijin@126.com");
            ErrorNoticeDlg.RecipientEmailList.Add("nova_lixc@126.com");
            ErrorNoticeDlg.RecipientEmailList.Add("nova_sangzhe@126.com");
            ErrorNoticeDlg.LoadLanguageResource(_errorFrmHashTable);
            ErrorNoticeDlg.SoftVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            errorNotifyWnd = ErrorNoticeDlg;
        }
        #endregion

        /// <summary>
        /// 显示调试信息
        /// </summary>
        /// <param name="isSeriousError"></param>
        /// <param name="info"></param>
        private void OutputDebugInfoStr(bool isSeriousError, string info)
        {
            string beginStr = "";
            if (isSeriousError)
            {
                beginStr = AppDomain.CurrentDomain.FriendlyName + NormalClassNameSeperate + CLASS_NAME + SeriousErrorBeginStr;
                System.Diagnostics.Debug.WriteLine(beginStr + info + SeriousErrorInfEnd);
            }
            else
            {
                beginStr = AppDomain.CurrentDomain.FriendlyName + NormalClassNameSeperate + CLASS_NAME + NormalInfoBeginStr;
                System.Diagnostics.Debug.WriteLine(beginStr + info + NormalInfoEnd);
            }
        }

        /// <summary>
        /// 获取控件的Text字符串
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        private string GetLangControlText(string controlName, string defaultText)
        {
            if (LangHashTable != null
             && LangHashTable.Contains(controlName.ToLower()))
            {
                return (string)LangHashTable[controlName.ToLower()];
            }
            return defaultText;
        }

        private void toolStripMenuItem_ReReadScreen_Click(object sender, EventArgs e)
        {
            MonitorAllConfig.Instance().ReReadScreenConfig(2, "Update");
        }

        private void StartService()
        {
            MonitorAllConfig.Instance().NovaCareServerAddress =
                System.Configuration.ConfigurationManager.AppSettings["NovaCareServerAddress"];
            int port = 0;
            if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["DataServicePort"], out port))
            {
                port = 8888;
            }
            MonitorAllConfig.Instance().MonitorDisplayVersion =
                System.Configuration.ConfigurationManager.AppSettings["Version"];
            MonitorAllConfig.Instance().DataServicePort = port;
            MonitorAllConfig.Instance().OpenUIHandlerEvent -= MonitorMain_OpenUIHandlerEvent;
            MonitorAllConfig.Instance().OpenUIHandlerEvent += MonitorMain_OpenUIHandlerEvent;
            MonitorAllConfig.Instance().GetVersions(Application.ProductVersion);
            MonitorAllConfig.Instance().StartServices();
        }

        private void AutoDisposed()
        {
            if (_zhchFont != null)
            {
                _zhchFont.Dispose();
            }
            if (_kokrFont != null)
            {
                _kokrFont.Dispose();
            }
            if (_enFont != null)
            {
                _enFont.Dispose();
            }
            if (_softwareFont != null)
            {
                _softwareFont.Dispose();
            }
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void MonitorMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown &&
                e.CloseReason != CloseReason.None)
            {
                if (CustomMessageBox.ShowCustomMessageBox(this,
                     GetLangControlText("toolStripMenuItem_ClosedTitle", "关闭监控将会影响智能亮度调节,是否关闭监控?"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIconType.Question) == DialogResult.OK)
                {
                    MonitorAllConfig.Instance().Dispose();
                    AutoDisposed();
                }
            }

            e.Cancel = true;
            this.Hide();
        }
        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x11)//WM_QUERYENDSESSION
            {
                msg.Result = (IntPtr)1;//０不关闭程序；１关闭程序
                return;
            }
            else if (msg.Msg == 0x16)//WM_ENDSESSION
            {
                msg.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref msg);
        }
    }
}