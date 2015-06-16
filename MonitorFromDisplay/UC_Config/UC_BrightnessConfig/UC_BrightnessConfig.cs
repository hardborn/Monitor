using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.MonitorDataManager;
using Nova.Resource.Language;
using System.Collections;
using Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.Monitoring.Common;
using Nova.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_BrightnessConfig : UC_ConfigBase
    {
        private Hashtable _langTable;
        public event EventHandler SubmitEvent;
        private volatile UC_BrightnessConfig_VM _brightnessVM;
        private BrightnessConfigType _type;
        private DisplaySmartBrightEasyConfigBase _brightnessSoftwareConfig;
        private DisplaySmartBrightEasyConfigBase _brightnessHardwareConfig;
        private volatile SmartLightConfigInfo _screenConfigInfo;
        private SmartLightConfigInfo _screenConfigInfobak;
        private string _currentSn = string.Empty;
        private LightSensorConfigState _configState;
        public DisplaySmartBrightEasyConfigBase BrightnessSoftwareConfig
        {
            get { return _brightnessSoftwareConfig; }
        }

        public DisplaySmartBrightEasyConfigBase BrightnessHardwareConfig
        {
            get { return _brightnessHardwareConfig; }
        }

        //public SmartLightConfigInfo _screenConfigInfo
        //{
        //    get { return _screenConfigInfo; }
        //    set
        //    {
        //        _screenConfigInfo = value ;
        //    }
        //}

        public LightSensorConfigState ConfigState
        {
            get { return _configState; }
            set
            {
                _configState = value;
                AdjustBrightnessConfig(_configState);
            }
        }

        public event Action<bool> CloseFormHandler = null;

        public override void crystalButton_OK_Location(Point value)
        {
            crystalButton_OK.Location = value;
            openLightSensoreConfigButton.Location = new Point(openLightSensoreConfigButton.Location.X - 105, openLightSensoreConfigButton.Location.Y);
            panel1.Width = openLightSensoreConfigButton.Location.X - panel1.Location.X;
        }

        private void AdjustBrightnessConfig(LightSensorConfigState configState)
        {
            if (_brightnessVM == null || _brightnessVM.BrightnessConfigList == null)
            {
                return;
            }

            switch (configState)
            {
                case LightSensorConfigState.OK_State:
                    this.alertInfoLabel.Text = string.Empty;
                    break;
                case LightSensorConfigState.NoSensor_State:
                    var resultItemNoSensor = _brightnessVM.BrightnessConfigList.FirstOrDefault(c => c.Type == SmartBrightAdjustType.AutoBright && c.IsConfigEnable);
                    if (resultItemNoSensor == null)
                    {
                        this.alertInfoLabel.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensorconfig", "没有选择所需的光探头,所有自动调节配置将失效!");
                    }
                    break;
                case LightSensorConfigState.NoMapping_State:
                    var resultItemNoMapping = _brightnessVM.BrightnessConfigList.FirstOrDefault(c => c.Type == SmartBrightAdjustType.AutoBright && c.IsConfigEnable);
                    if (resultItemNoMapping == null)
                    {
                        this.alertInfoLabel.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensormappingconfig", "没有配置光探头的亮度映射表,所有自动调节配置将失效!");
                    }
                    break;
                case LightSensorConfigState.InvalidSensor_State:
                    this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "partlightsensorfail", "配置了自动亮度，选择的部分光探头在无软件时不可用!");
                    break;
                default:
                    break;
            }

            //if (configState == LightSensorConfigState.NoSensor_State || configState == LightSensorConfigState.NoMapping_State)
            //{

            //    _screenConfigInfo.DispaySoftWareConfig.OneDayConfigList.ForEach((c) =>
            //    {
            //        if (c.ScheduleType == SmartBrightAdjustType.AutoBright)
            //        {
            //            c.IsConfigEnable = false;
            //        }
            //    });
            //}
        }
        public UC_BrightnessConfig()
        {
            InitializeComponent();
            UpdateLang(CommonUI.BrightnessConfigLangPath);
            _brightnessVM = new UC_BrightnessConfig_VM();

            dataGridView_BrightnessConfig.ReadOnly = false;
            BrightnessConfig_BindingSource.DataSource = _brightnessVM.BrightnessConfigList;
            dataGridView_BrightnessConfig.DataSource = BrightnessConfig_BindingSource;

            dataGridView_BrightnessConfig.CellContentClick += dataGridView_BrightnessConfig_CellContentClick;
            dataGridView_BrightnessConfig.CellValueChanged += dataGridView_BrightnessConfig_CellValueChanged;
            dataGridView_BrightnessConfig.CurrentCellDirtyStateChanged += dataGridView_BrightnessConfig_CurrentCellDirtyStateChanged;

            foreach (DataGridViewColumn item in dataGridView_BrightnessConfig.Columns)
            {
                if (item.Name.ToLower().Equals("isconfigenable"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "isconfigenable", "启用");
                    item.Width = 90;
                    item.ReadOnly = false;
                }
                else if (item.Name.ToLower().Equals("time"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "time", "时间");
                    item.ReadOnly = true;
                    item.Width = 90;
                    item.DefaultCellStyle.Format = "HH:mm";
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else if (item.Name.ToLower().Equals("typestr"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "typestr", "控制类型");
                    item.ReadOnly = true;
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else if (item.Name.ToLower().Equals("dayliststr"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "dayliststr", "执行周期");
                    item.ReadOnly = true;
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else if (item.Name.ToLower().Equals("brightnessdisplay"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "brightnessdisplay", "亮度(%)");
                    item.ReadOnly = true;
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    item.Visible = false;
                }
            }

            DataGridViewLinkColumn editLinks = new DataGridViewLinkColumn();
            editLinks.HeaderText = string.Empty;
            editLinks.ReadOnly = true;
            editLinks.Text = CommonUI.GetCustomMessage(_langTable, "editname", "编辑");
            editLinks.UseColumnTextForLinkValue = true;
            editLinks.Width = 60;
            dataGridView_BrightnessConfig.Columns.Add(editLinks);

            DataGridViewLinkColumn delateLinks = new DataGridViewLinkColumn();
            delateLinks.HeaderText = string.Empty;
            delateLinks.ReadOnly = true;
            delateLinks.Text = CommonUI.GetCustomMessage(_langTable, "deletename", "删除");
            delateLinks.UseColumnTextForLinkValue = true;
            delateLinks.Width = 60;
            dataGridView_BrightnessConfig.Columns.Add(delateLinks);
        }




        delegate void SetBrightnessValueTextCallback(string value);
        void UC_BrightnessConfig_BrightnessValueRefreshed(object sender, BrightnessValueRefreshEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentSn))
            {
                return;
            }
            if (e.SN == _currentSn)
            {
                //SetBrightnessValue(e.BrightnessValue);
            }
        }

        private void SetBrightnessValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            if (this.currentBrightnessTextBox.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.currentBrightnessTextBox.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.currentBrightnessTextBox.Disposing || this.currentBrightnessTextBox.IsDisposed)
                        return;
                }
                SetBrightnessValueTextCallback d = new SetBrightnessValueTextCallback(SetBrightnessValue);
                this.currentBrightnessTextBox.Invoke(d, new object[] { value });
            }
            else
            {
                this.currentBrightnessTextBox.Text = Math.Round((100d * Convert.ToDouble(value) / 255), 0).ToString();
            }
        }

        protected override void Register()
        {
            Messenger.Default.Register<string>(this, MsgToken.MSG_BrightnessConfig, OnMsgControlConfig);
            //MonitorAllConfig.Instance().BrightnessValueRefreshed += UC_BrightnessConfig_BrightnessValueRefreshed;
            MonitorAllConfig.Instance().BrightnessChangedEvent += UC_BrightnessConfig_BrightnessChangedEvent;
        }

        private void UC_BrightnessConfig_BrightnessChangedEvent(SmartLightConfigInfo obj)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { UC_BrightnessConfig_BrightnessChangedEvent(obj); }));
                return;
            }
            if (obj == null)
            {
                return;
            }
            if (_screenConfigInfo != null && _screenConfigInfo.ScreenSN == obj.ScreenSN)
            {
                _screenConfigInfo = (SmartLightConfigInfo)(obj.Clone());
                _screenConfigInfobak = (SmartLightConfigInfo)_screenConfigInfo.Clone();
                _screenConfigInfobak.DisplayHardcareConfig = null;
                InitialControlConfig(obj.ScreenSN, (DisplaySmartBrightEasyConfigBase)obj.DispaySoftWareConfig);
            }
        }



        protected override void UnRegister()
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_BrightnessConfig, OnMsgControlConfig);
            //MonitorAllConfig.Instance().BrightnessValueRefreshed -= UC_BrightnessConfig_BrightnessValueRefreshed;
            MonitorAllConfig.Instance().BrightnessChangedEvent -= UC_BrightnessConfig_BrightnessChangedEvent;
        }

        private void OnMsgControlConfig(string sn)
        {
            if (sn != MonitorAllConfig.Instance().ALLScreenName)
            {
                if (_screenConfigInfo != null && _screenConfigInfo.ScreenSN == sn)
                {
                    return;
                }
                SmartLightConfigInfo tmp = MonitorAllConfig.Instance().BrightnessConfigList.Find(a => a.ScreenSN == sn);

                _screenConfigInfo = new SmartLightConfigInfo();
                if (tmp == null)
                {
                    _screenConfigInfo.ScreenSN = sn;
                    _screenConfigInfo.HwExecTypeValue = BrightnessHWExecType.DisHardWareControl;
                }
                else
                {
                    _screenConfigInfo = (SmartLightConfigInfo)(tmp.Clone());
                }
                _screenConfigInfobak = (SmartLightConfigInfo)_screenConfigInfo.Clone();
                _screenConfigInfobak.DisplayHardcareConfig = null;

                InitialControlConfig(sn, _screenConfigInfo.DispaySoftWareConfig);
            }
        }

        private void InitialLangTable()
        {
            BrightnessLangTable.CycleTypeTable = new Dictionary<CycleType, string>();
            BrightnessLangTable.CycleTypeTable.Add(CycleType.everyday, CommonUI.GetCustomMessage(_langTable, "everyday", "每天"));
            BrightnessLangTable.CycleTypeTable.Add(CycleType.userDefined, CommonUI.GetCustomMessage(_langTable, "userdefined", "自定义"));
            BrightnessLangTable.CycleTypeTable.Add(CycleType.workday, CommonUI.GetCustomMessage(_langTable, "workday", "周一到周五"));

            BrightnessLangTable.DayTable = new Dictionary<DayOfWeek, string>();
            BrightnessLangTable.DayTable.Add(DayOfWeek.Monday, CommonUI.GetCustomMessage(_langTable, "monday", "周一"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Tuesday, CommonUI.GetCustomMessage(_langTable, "tuesday", "周二"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Wednesday, CommonUI.GetCustomMessage(_langTable, "wednesday", "周三"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Thursday, CommonUI.GetCustomMessage(_langTable, "thursday", "周四"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Friday, CommonUI.GetCustomMessage(_langTable, "friday", "周五"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Saturday, CommonUI.GetCustomMessage(_langTable, "saturday", "周六"));
            BrightnessLangTable.DayTable.Add(DayOfWeek.Sunday, CommonUI.GetCustomMessage(_langTable, "sunday", "周日"));
            BrightnessLangTable.SmartBrightTypeTable = new Dictionary<SmartBrightAdjustType, string>();
            BrightnessLangTable.SmartBrightTypeTable.Add(SmartBrightAdjustType.AutoBright, CommonUI.GetCustomMessage(_langTable, "autobright", "自动亮度"));
            BrightnessLangTable.SmartBrightTypeTable.Add(SmartBrightAdjustType.FixBright, CommonUI.GetCustomMessage(_langTable, "fixbright", "固定亮度"));
        }

        public void InitialControlConfig(string sn, DisplaySmartBrightEasyConfigBase cfg)
        {
#if test
            if (MonitorAllConfig.Instance().BrightnessConfigList == null || MonitorAllConfig.Instance().BrightnessConfigList.Count == 0)
            {
                MonitorAllConfig.Instance().BrightnessConfigList = new List<DisplaySmartBrightEasyConfig>();
                DisplaySmartBrightEasyConfig cfg = new DisplaySmartBrightEasyConfig();
                cfg.DisplayUDID = "1405220000041214-00";
                cfg.AutoBrightSetting = new AutoBrightExtendData();
                cfg.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
                OneSmartBrightEasyConfig oneCfg = new OneSmartBrightEasyConfig();
                oneCfg.BrightPercent = 80;
                oneCfg.CustomDayCollection = new List<DayOfWeek>();
                oneCfg.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Thursday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Friday);
                cfg.OneDayConfigList.Add(oneCfg);
                oneCfg.IsConfigEnable = true;
                oneCfg.ScheduleType = SmartBrightAdjustType.FixBright;
                oneCfg.StartTime = DateTime.Now;


                OneSmartBrightEasyConfig oneCfg1 = new OneSmartBrightEasyConfig();
                oneCfg1.BrightPercent = 80;
                oneCfg1.CustomDayCollection = new List<DayOfWeek>();
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Thursday);
                cfg.OneDayConfigList.Add(oneCfg1);
                oneCfg1.IsConfigEnable = true;
                oneCfg1.ScheduleType = SmartBrightAdjustType.AutoBright;
                oneCfg1.StartTime = DateTime.Now;


                OneSmartBrightEasyConfig oneCfg2 = new OneSmartBrightEasyConfig();
                oneCfg2.BrightPercent = 80;
                oneCfg2.CustomDayCollection = new List<DayOfWeek>();
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Thursday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Friday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Saturday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Sunday);
                cfg.OneDayConfigList.Add(oneCfg2);
                oneCfg2.IsConfigEnable = true;
                oneCfg2.ScheduleType = SmartBrightAdjustType.AutoBright;
                oneCfg2.StartTime = DateTime.Now;
                MonitorAllConfig.Instance().BrightnessConfigList.Add(cfg);
            }
#endif
            _currentSn = sn;
            //SetBrightnessValue(MonitorAllConfig.Instance().GetCurrentBrightnessValueBy(_currentSn));
            InitializeViewData(sn, cfg);
            _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
            if (cfg == null)
            {
                ConfigState = LightSensorConfigState.NoSensor_State;
                return;
            }
            if (cfg.AutoBrightSetting == null || cfg.AutoBrightSetting.AutoBrightMappingList == null || cfg.AutoBrightSetting.AutoBrightMappingList.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoMapping_State;
            }

            if (cfg.AutoBrightSetting == null || cfg.AutoBrightSetting.UseLightSensorList == null || cfg.AutoBrightSetting.UseLightSensorList.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoSensor_State;
            }
        }


        delegate void InitializeViewDataCallback(string sn, DisplaySmartBrightEasyConfigBase cfg);
        private void InitializeViewData(string sn, DisplaySmartBrightEasyConfigBase cfg)
        {

            if (this.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.Disposing || this.IsDisposed)
                        return;
                }
                InitializeViewDataCallback d = new InitializeViewDataCallback(InitializeViewData);
                this.Invoke(d, new object[] { sn, cfg });
            }
            else
            {
                dataGridView_BrightnessConfig.Rows.Clear();
                BrightnessConfig_BindingSource.Clear();
                _brightnessVM.Initialize(sn, cfg);
                BrightnessConfig_BindingSource.ResetBindings(false);
                _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
                this.alertInfoLabel.Text = string.Empty;
                if (dataGridView_BrightnessConfig.SelectedRows.Count != 0)
                {
                    dataGridView_BrightnessConfig.SelectedRows[0].Selected = false;
                }
            }
        }

        public void InitialControlConfig(string sn, BrightnessConfigType type, DisplaySmartBrightEasyConfigBase cfg)
        {
#if test
            if (MonitorAllConfig.Instance().BrightnessConfigList == null || MonitorAllConfig.Instance().BrightnessConfigList.Count == 0)
            {
                MonitorAllConfig.Instance().BrightnessConfigList = new List<DisplaySmartBrightEasyConfig>();
                DisplaySmartBrightEasyConfig cfg = new DisplaySmartBrightEasyConfig();
                cfg.DisplayUDID = "1405220000041214-00";
                cfg.AutoBrightSetting = new AutoBrightExtendData();
                cfg.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
                OneSmartBrightEasyConfig oneCfg = new OneSmartBrightEasyConfig();
                oneCfg.BrightPercent = 80;
                oneCfg.CustomDayCollection = new List<DayOfWeek>();
                oneCfg.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Thursday);
                oneCfg.CustomDayCollection.Add(DayOfWeek.Friday);
                cfg.OneDayConfigList.Add(oneCfg);
                oneCfg.IsConfigEnable = true;
                oneCfg.ScheduleType = SmartBrightAdjustType.FixBright;
                oneCfg.StartTime = DateTime.Now;


                OneSmartBrightEasyConfig oneCfg1 = new OneSmartBrightEasyConfig();
                oneCfg1.BrightPercent = 80;
                oneCfg1.CustomDayCollection = new List<DayOfWeek>();
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg1.CustomDayCollection.Add(DayOfWeek.Thursday);
                cfg.OneDayConfigList.Add(oneCfg1);
                oneCfg1.IsConfigEnable = true;
                oneCfg1.ScheduleType = SmartBrightAdjustType.AutoBright;
                oneCfg1.StartTime = DateTime.Now;


                OneSmartBrightEasyConfig oneCfg2 = new OneSmartBrightEasyConfig();
                oneCfg2.BrightPercent = 80;
                oneCfg2.CustomDayCollection = new List<DayOfWeek>();
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Monday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Tuesday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Wednesday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Thursday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Friday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Saturday);
                oneCfg2.CustomDayCollection.Add(DayOfWeek.Sunday);
                cfg.OneDayConfigList.Add(oneCfg2);
                oneCfg2.IsConfigEnable = true;
                oneCfg2.ScheduleType = SmartBrightAdjustType.AutoBright;
                oneCfg2.StartTime = DateTime.Now;
                MonitorAllConfig.Instance().BrightnessConfigList.Add(cfg);
            }
#endif
            _type = type;
            dataGridView_BrightnessConfig.Rows.Clear();
            BrightnessConfig_BindingSource.Clear();
            _brightnessVM.Initialize(sn, cfg);
            BrightnessConfig_BindingSource.ResetBindings(false);
            _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
            if (dataGridView_BrightnessConfig.SelectedRows.Count != 0)
            {
                dataGridView_BrightnessConfig.SelectedRows[0].Selected = false;
            }
            if (cfg == null)
                return;
            if (cfg.AutoBrightSetting == null || cfg.AutoBrightSetting.AutoBrightMappingList == null || cfg.AutoBrightSetting.AutoBrightMappingList.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoMapping_State;
            }

            if (cfg.AutoBrightSetting == null || cfg.AutoBrightSetting.UseLightSensorList == null || cfg.AutoBrightSetting.UseLightSensorList.Count == 0)
            {
                ConfigState = LightSensorConfigState.NoSensor_State;
            }
        }
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_BrightnessConfig", this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "UC_BrightnessConfig", out _langTable);
            HsLangTable = _langTable;
            InitialLangTable();
        }
        private void dataGridView_BrightnessConfig_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_BrightnessConfig.Rows.Count == 0)
            {
                //button_edit.Enabled = false;
                ToolStripMenuItem_edit.Enabled = false;
                //button_delete.Enabled = false;
                ToolStripMenuItem_delete.Enabled = false;
                button_deleteAll.Enabled = false;
                ToolStripMenuItem_deleteAll.Enabled = false;
            }
            else
            {
                button_deleteAll.Enabled = true;
                ToolStripMenuItem_deleteAll.Enabled = true;
            }
            if (dataGridView_BrightnessConfig.SelectedRows.Count > 0)
            {
                //button_edit.Enabled = true;
                ToolStripMenuItem_edit.Enabled = true;
                //button_delete.Enabled = true;
                ToolStripMenuItem_delete.Enabled = true;
            }
            else
            {
                //button_edit.Enabled = false;
                ToolStripMenuItem_edit.Enabled = false;
                //button_delete.Enabled = false;
                ToolStripMenuItem_delete.Enabled = false;
            }
        }

        void dataGridView_BrightnessConfig_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView_BrightnessConfig.IsCurrentCellDirty)
            {
                dataGridView_BrightnessConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void dataGridView_BrightnessConfig_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                for (int i = 0; i < dataGridView_BrightnessConfig.Rows.Count; i++)
                {
                    var typeCell = dataGridView_BrightnessConfig.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                    var enableCell = dataGridView_BrightnessConfig.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                    if (enableCell == null || typeCell == null)
                        return;
                    bool isChecked = (bool)enableCell.Value;
                    string type = typeCell.Value as string;
                    if (!isChecked)
                    {
                        this.alertInfoLabel.Text = string.Empty;
                    }
                    if (isChecked && type == CommonUI.GetCustomMessage(_langTable, "autobright", "自动亮度"))
                    {
                        if (ConfigState == LightSensorConfigState.NoSensor_State)
                        {
                            this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensorconfig", "没有选择所需的光探头,所有自动调节配置将失效!");
                            break;
                        }
                        else if (ConfigState == LightSensorConfigState.NoMapping_State)
                        {
                            this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensormappingconfig", "没有配置光探头的亮度映射表,所有自动调节配置将失效!");
                            break;
                        }
                        else if (ConfigState == LightSensorConfigState.InvalidSensor_State)
                        {
                            this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "partlightsensorfail", "配置了自动亮度，选择的部分光探头在无软件时不可用!");
                            break;
                        }
                        else
                        {
                            this.alertInfoLabel.Text = string.Empty;
                        }
                    }
                }
            }
        }


        private void dataGridView_BrightnessConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                Frm_BrightnessAdjustmentSetting frm_Config = new Frm_BrightnessAdjustmentSetting((BrightnessConfigInfo)_brightnessVM.BrightnessConfigList[e.RowIndex].Clone(), _brightnessVM.AutoBrightData, _brightnessVM.BrightnessConfigList);
                if (frm_Config.ShowDialog() == DialogResult.OK)
                {
                    if (frm_Config.BrightnessCfg != null)
                    {
                        if (frm_Config.BrightnessCfg.Type == SmartBrightAdjustType.FixBright)
                            frm_Config.BrightnessCfg.Brightness = (float)Math.Round(frm_Config.BrightnessCfg.Brightness, 1);
                    }
                    _brightnessVM.BrightnessConfigList[e.RowIndex] = frm_Config.BrightnessCfg;
                    _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
                    BrightnessConfig_BindingSource.ResetBindings(false);
                    AdjustBrightnessConfig(ConfigState);
                }
            }
            else if (e.ColumnIndex == 1)
            {
                if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_deletebrightnessconfig", "确定删除选择项吗？"), "", MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIconType.Question) != DialogResult.Yes) return;
                _brightnessVM.BrightnessConfigList.Remove(_brightnessVM.BrightnessConfigList[e.RowIndex]);
                BrightnessConfig_BindingSource.ResetBindings(false);
                AdjustBrightnessConfig(ConfigState);
            }
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            Frm_BrightnessAdjustmentSetting frm_Config = new Frm_BrightnessAdjustmentSetting(null, _brightnessVM.AutoBrightData, _brightnessVM.BrightnessConfigList);
            //frm_Config.Owner = (Form)this.Parent.Parent.Parent;
            if (frm_Config.ShowDialog() == DialogResult.OK)
            {
                //frm_Config.BrightnessCfg.EditingOperation = CommonUI.GetCustomMessage(_langTable, "editname", "编辑");
                //frm_Config.BrightnessCfg.DelationOperation = CommonUI.GetCustomMessage(_langTable, "deletename", "删除");
                _brightnessVM.BrightnessConfigList.Add(frm_Config.BrightnessCfg);
                _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
                BrightnessConfig_BindingSource.ResetBindings(false);
                if (dataGridView_BrightnessConfig.Rows.Count > 0)
                {
                    dataGridView_BrightnessConfig.Rows[dataGridView_BrightnessConfig.Rows.Count - 1].Selected = true;
                }
                AdjustBrightnessConfig(ConfigState);
            }

        }

        private int CompareBrightnessByTime(BrightnessConfigInfo x, BrightnessConfigInfo y)
        {
            if (x.Time == null)
            {
                if (y.Time == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y.Time == null)
                {
                    return 1;
                }
                else
                {
                    //int retval = x.Time.CompareTo(y.Time);
                    if (x.Time.Hour - y.Time.Hour > 0)
                    {
                        return 1;
                    }
                    else if (x.Time.Hour - y.Time.Hour < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        if (x.Time.Minute - y.Time.Minute > 0)
                        {
                            return 1;
                        }
                        else if (x.Time.Minute - y.Time.Minute < 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    //if (retval != 0)
                    //{
                    //    return retval;
                    //}
                    //else
                    //{
                    //    return x.Time.CompareTo(y.Time);
                    //}
                }
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            //BrightnessConfigInfo brightness = _brightnessVM.BrightnessConfigList[dataGridView_BrightnessConfig.SelectedRows[0].Index].Clone();
            Frm_BrightnessAdjustmentSetting frm_Config = new Frm_BrightnessAdjustmentSetting((BrightnessConfigInfo)_brightnessVM.BrightnessConfigList[dataGridView_BrightnessConfig.SelectedRows[0].Index].Clone(), _brightnessVM.AutoBrightData, _brightnessVM.BrightnessConfigList);
            //frm_Config.Owner = (Form)this.Parent.Parent.Parent;
            if (frm_Config.ShowDialog() == DialogResult.OK)
            {
                _brightnessVM.BrightnessConfigList[dataGridView_BrightnessConfig.SelectedRows[0].Index] = frm_Config.BrightnessCfg;
                _brightnessVM.BrightnessConfigList.Sort(CompareBrightnessByTime);
                BrightnessConfig_BindingSource.ResetBindings(false);
                AdjustBrightnessConfig(ConfigState);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_deletebrightnessconfig", "确定删除选择项吗？"), "", MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIconType.Question) != DialogResult.Yes) return;
            _brightnessVM.BrightnessConfigList.Remove(_brightnessVM.BrightnessConfigList[dataGridView_BrightnessConfig.SelectedCells[0].RowIndex]);
            BrightnessConfig_BindingSource.ResetBindings(false);
            AdjustBrightnessConfig(ConfigState);
        }

        private void button_deleteAll_Click(object sender, EventArgs e)
        {
            if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_brightnessconfig_empty", "是否要清空亮度配置列表？"), "", MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIconType.Error) != DialogResult.Yes) return;
            _brightnessVM.BrightnessConfigList.Clear();
            BrightnessConfig_BindingSource.ResetBindings(false);
            AdjustBrightnessConfig(ConfigState);
        }

        private void dataGridView_BrightnessConfig_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                dataGridView_BrightnessConfig.CurrentRow.Selected = false;
                dataGridView_BrightnessConfig.Rows[e.RowIndex].Selected = true;
            }
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            //if (!_brightnessVM.IsOK())
            //{
            //    if (_brightnessVM.AutoBrightData == null || _brightnessVM.AutoBrightData.AutoBrightMappingList == null || _brightnessVM.AutoBrightData.AutoBrightMappingList.Count == 0)
            //        ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_configlightprobe", "请为该显示屏配置光探头（自动亮度根据光探头调节列表调节显示屏亮度）！"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
            //    else ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_configlightprobe_lightlist", "请配置光探头调节列表（自动亮度根据光探头调节列表调节显示屏亮度）！"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
            //    return;
            //}
            if (!SetBrightConfig())
            {
                return;
            }
            //_screenConfigInfo.DispaySoftWareConfig = (DisplaySmartBrightEasyConfig)_brightnessSoftwareConfig;
            //_screenConfigInfo.DisplayHardcareConfig = _brightnessHardwareConfig;

            if (SubmitEvent != null) SubmitEvent(sender, e);
            if (!MonitorAllConfig.Instance().SaveBrightnessConfig(_screenConfigInfo))
            {
                CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "savefailed", "保存失败"));
            }
            //ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "savefailed", "保存失败"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
            else
            {
                if (CloseFormHandler != null)
                {
                    CloseFormHandler(true);
                }
                else
                {
                    _screenConfigInfobak = (SmartLightConfigInfo)_screenConfigInfo.Clone();
                    _screenConfigInfobak.DisplayHardcareConfig = null;
                    //MessageBox.Show(CommonUI.GetCustomMessage(_langTable, "savesuccess", "保存成功"));
                    CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "savesuccess", "保存成功"));
                }
            }
        }

        private void openOpticalProbeConfigButton_Click(object sender, EventArgs e)
        {
            frm_OpticalProbeConfig frmConfig = new frm_OpticalProbeConfig(true, _screenConfigInfo);
            frmConfig.SensorTestEvent -= frmConfig_SensorTestEvent;
            frmConfig.SensorTestEvent += frmConfig_SensorTestEvent;
            //frmConfig.Owner = (Form)this.Parent.Parent.Parent;
            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                if (_screenConfigInfo.DispaySoftWareConfig == null)
                {
                    _screenConfigInfo.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                }
                _screenConfigInfo.DispaySoftWareConfig.AutoBrightSetting = frmConfig.BrightExtendData;
                if (_screenConfigInfo.DisplayHardcareConfig == null)
                {
                    _screenConfigInfo.DisplayHardcareConfig = new DisplaySmartBrightEasyConfigBase();
                }
                _screenConfigInfo.DisplayHardcareConfig.AutoBrightSetting = frmConfig.BrightExtendData;

                ConfigState = frmConfig.ConfigState;
            }
        }

        private void frmConfig_SensorTestEvent(bool isTest, List<PeripheralsLocation> sensorList)
        {
            if (isTest)
            {
                SmartLightConfigInfo smartTest = new SmartLightConfigInfo();
                smartTest.ScreenSN = _currentSn;
                smartTest.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
                smartTest.DispaySoftWareConfig.AutoAdjustPeriod = 5;
                smartTest.DispaySoftWareConfig.AutoBrightReadLuxCnt = 1;
                smartTest.DispaySoftWareConfig.DisplayUDID = _currentSn;
                smartTest.DispaySoftWareConfig.IsSmartEnable = true;
                smartTest.DispaySoftWareConfig.IsBrightGradualEnable = true;
                smartTest.DispaySoftWareConfig.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
                smartTest.DispaySoftWareConfig.OneDayConfigList.Add(new OneSmartBrightEasyConfig()
                {
                    IsConfigEnable = true,
                    BrightPercent = 0,
                    CustomDayCollection = new List<DayOfWeek>() 
                        {
                            (DayOfWeek)0,(DayOfWeek)1,(DayOfWeek)2,(DayOfWeek)3,(DayOfWeek)4,(DayOfWeek)5,(DayOfWeek)6
                        },
                    ScheduleType = SmartBrightAdjustType.AutoBright,
                    StartTime = new DateTime(2014, 1, 1, 0, 0, 0)
                });

                smartTest.DispaySoftWareConfig.AutoBrightSetting.AutoBrightMappingList = new List<DisplayAutoBrightMapping>()
                {
                    new DisplayAutoBrightMapping(){EnvironmentBright=200, DisplayBright=90},
                    new DisplayAutoBrightMapping(){EnvironmentBright=100, DisplayBright=50},
                    new DisplayAutoBrightMapping(){EnvironmentBright=0, DisplayBright=0}
                };
                smartTest.DispaySoftWareConfig.AutoBrightSetting.UseLightSensorList = sensorList;
                MonitorAllConfig.Instance().SaveBrightnessConfig(smartTest, false);
            }
            else
            {
                MonitorAllConfig.Instance().SaveBrightnessConfig(_screenConfigInfobak, false);
            }
        }

        private void SetAlertInfo(LightSensorConfigState configState)
        {
            switch (configState)
            {
                case LightSensorConfigState.OK_State:
                    this.alertInfoLabel.Text = string.Empty;
                    break;
                case LightSensorConfigState.NoSensor_State:
                    this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensorconfig", "没有选择所需的光探头,所有自动调节配置将失效!");
                    break;
                case LightSensorConfigState.NoMapping_State:
                    this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "nolightsensormappingconfig", "没有配置光探头的亮度映射表,所有自动调节配置将失效!");
                    break;
                case LightSensorConfigState.InvalidSensor_State:
                    this.alertInfoLabel.Text = CommonUI.GetCustomMessage(_langTable, "partlightsensorfail", "配置了自动亮度，选择的部分光探头在无软件时不可用!");
                    break;
                default:
                    break;
            }
        }

        private void BrightnessConfig_BindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            AdjustBrightnessConfig(ConfigState);
        }

        private bool SetBrightConfig()
        {
            if (ConfigState == LightSensorConfigState.NoSensor_State && !string.IsNullOrEmpty(this.alertInfoLabel.Text))
            {
                ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_configlightprobe", "请为该显示屏配置光探头（自动亮度根据光探头调节列表调节显示屏亮度）！"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
                return false;
            }

            if (ConfigState == LightSensorConfigState.NoMapping_State && !string.IsNullOrEmpty(this.alertInfoLabel.Text))
            {
                ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_configlightprobe_lightlist", "请配置光探头调节列表（自动亮度根据光探头调节列表调节显示屏亮度）！"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
                return false;
            }

            if (_screenConfigInfo.DispaySoftWareConfig == null)
            {
                _screenConfigInfo.DispaySoftWareConfig = new DisplaySmartBrightEasyConfig();
            }
            _screenConfigInfo.DispaySoftWareConfig.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
            OneSmartBrightEasyConfig oneConfig;
            foreach (var item in _brightnessVM.BrightnessConfigList)
            {
                oneConfig = new OneSmartBrightEasyConfig();
                oneConfig.BrightPercent = item.Brightness;
                oneConfig.CustomDayCollection = item.DayList;
                oneConfig.IsConfigEnable = item.IsConfigEnable;
                oneConfig.ScheduleType = item.Type;
                oneConfig.StartTime = item.Time;
                _screenConfigInfo.DispaySoftWareConfig.OneDayConfigList.Add(oneConfig);
            }
            ((DisplaySmartBrightEasyConfig)_screenConfigInfo.DispaySoftWareConfig).DisplayUDID = _brightnessVM.SN;

            //_brightnessHardwareConfig = new DisplaySmartBrightEasyConfigBase();
            // _screenConfigInfo.DisplayHardcareConfig.AutoBrightSetting = _brightnessVM.AutoBrightData;

            if (_screenConfigInfo.DisplayHardcareConfig == null)
            {
                _screenConfigInfo.DisplayHardcareConfig = new DisplaySmartBrightEasyConfigBase();
            }
            _screenConfigInfo.DisplayHardcareConfig.OneDayConfigList = new List<OneSmartBrightEasyConfig>();
            foreach (var item in _brightnessVM.BrightnessConfigList)
            {
                oneConfig = new OneSmartBrightEasyConfig();
                oneConfig.BrightPercent = item.Brightness;
                oneConfig.CustomDayCollection = item.DayList;
                oneConfig.IsConfigEnable = item.IsConfigEnable;
                oneConfig.ScheduleType = item.Type;
                oneConfig.StartTime = item.Time;
                _screenConfigInfo.DisplayHardcareConfig.OneDayConfigList.Add(oneConfig);
            }
            return true;
        }

    }
}
