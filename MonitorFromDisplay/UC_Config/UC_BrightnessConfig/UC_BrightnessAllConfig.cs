using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Resource.Language;
using Nova.Monitoring.MonitorDataManager;
using GalaSoft.MvvmLight.Messaging;
using System.Collections;
using Nova.LCT.GigabitSystem.Common;

namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    public partial class UC_BrightnessAllConfig : UC_ConfigBase
    {

        private Hashtable _langTable;
        public UC_BrightnessAllConfig()
        {
            InitializeComponent();

            UpdateLang(CommonUI.BrightnessConfigLangPath);

            Messenger.Default.Register<string>(this, MsgToken.MSG_BrightnessConfig, OnMsgControlConfig);

            if (MonitorAllConfig.Instance().BrightnessConfigList != null
                && MonitorAllConfig.Instance().BrightnessConfigList.Count > 0)
            {
                int autoBrightPeriod = 0;
                int readLuxCnt = 0;
                if (MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig != null)
                {
                    autoBrightPeriod = MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig.AutoAdjustPeriod;
                    readLuxCnt = MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig.AutoBrightReadLuxCnt;
                    checkBox_IsSmartEnable.Checked = MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig.IsSmartEnable;
                    checkBox_IsGradualEnable.Checked = MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig.IsBrightGradualEnable;
                }
                else if (MonitorAllConfig.Instance().BrightnessConfigList[0].DispaySoftWareConfig != null)
                {
                    autoBrightPeriod = MonitorAllConfig.Instance().BrightnessConfigList[0].DispaySoftWareConfig.AutoAdjustPeriod;
                    readLuxCnt = MonitorAllConfig.Instance().BrightnessConfigList[0].DispaySoftWareConfig.AutoBrightReadLuxCnt;
                    checkBox_IsSmartEnable.Checked = MonitorAllConfig.Instance().BrightnessConfigList[0].DispaySoftWareConfig.IsSmartEnable;
                    checkBox_IsGradualEnable.Checked = MonitorAllConfig.Instance().BrightnessConfigList[0].DispaySoftWareConfig.IsBrightGradualEnable;
                }
                if (autoBrightPeriod >= 5 && autoBrightPeriod <= 240)
                {
                    numericUpDown_AutoBrightPeriod.Value = autoBrightPeriod;
                }
                if (readLuxCnt >= 1 && readLuxCnt <= 10)
                {
                    numericUpDown_ReadLuxCnt.Value = readLuxCnt;
                }
            }
        }

        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        public void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_Brightness", this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "UC_Brightness_String", out _langTable);
            InitialLangTable();
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

            //label_tip.Text = string.Format(
            //    CommonUI.GetCustomMessage(_langTable, "label_tip", "提示:存在多屏时：环境亮度配置请选择'{0}'进行配置!"),
            //    MonitorAllConfig.Instance().ALLScreenName);

            BrightnessLangTable.SmartBrightTypeTable = new Dictionary<SmartBrightAdjustType, string>();
            BrightnessLangTable.SmartBrightTypeTable.Add(SmartBrightAdjustType.AutoBright, CommonUI.GetCustomMessage(_langTable, "autobright", "自动亮度"));
            BrightnessLangTable.SmartBrightTypeTable.Add(SmartBrightAdjustType.FixBright, CommonUI.GetCustomMessage(_langTable, "fixbright", "固定亮度"));
        }


        private TabAppearance _appearance;
        private Size _size;
        private TabSizeMode _tabSizeMode;
        private void OnMsgControlConfig(string sn)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { OnMsgControlConfig(sn); }));
                return;
            }
            if (sn == MonitorAllConfig.Instance().ALLScreenName)
            {
                _appearance = brightnessSettingTabControl.Appearance;
                _size = brightnessSettingTabControl.ItemSize;
                _tabSizeMode = brightnessSettingTabControl.SizeMode;
                brightnessSettingTabControl.Appearance = TabAppearance.FlatButtons;
                brightnessSettingTabControl.ItemSize = new Size(0, 1);
                brightnessSettingTabControl.SizeMode = TabSizeMode.Fixed;
                brightnessSettingTabControl.SelectTab("globSettingTabPage");
                //brightnessSettingTabControl.Visible = false;
                //this.Enabled = false;
                //label_tip.Text = CommonUI.GetCustomMessage(_langTable, "choicesinglescreen", "请选择单个显示屏，配置亮度调节");
                //label_tip.BringToFront();
                //label_tip.Visible = true;
                //ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "choicesinglescreen", "请选择单个显示屏，配置亮度调节"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
                //groupBox_AutoBrightConfig.Parent = panel_ConfigBase;
                //groupBox_AutoBrightConfig.Visible = true;
                //groupBox_BrightnessTable.Visible = false;
                return;
            }
            else
            {
                if (MonitorAllConfig.Instance().LedInfoList != null && MonitorAllConfig.Instance().LedInfoList.Count == 1)
                {
                    brightnessSettingTabControl.Appearance = _appearance;
                    brightnessSettingTabControl.ItemSize = _size;
                    brightnessSettingTabControl.SizeMode = _tabSizeMode;
                    brightnessSettingTabControl.SelectTab("brightnessConfigTabPage");
                    
                    //tabControl1.Dock = DockStyle.Fill;
                    //tabControl1.Visible = true;
                    //groupBox_AutoBrightConfig.Visible = true;
                    //groupBox_AutoBrightConfig.Parent = tabPage_AutoBrightConfig;
                    //groupBox_BrightnessTable.Visible = true;
                    //groupBox_BrightnessTable.Parent = tabPage_BrightnessTable;
                    //label_tip.Visible = false;
                }
                else
                {
                    brightnessSettingTabControl.Appearance = TabAppearance.FlatButtons;
                    brightnessSettingTabControl.ItemSize = new Size(0, 1);
                    brightnessSettingTabControl.SizeMode = TabSizeMode.Fixed;
                    brightnessSettingTabControl.SelectTab("brightnessConfigTabPage");
                   
                    //tabControl1.Visible = false;
                    //groupBox_AutoBrightConfig.Visible = false;
                    //groupBox_BrightnessTable.Visible = true;
                    //groupBox_BrightnessTable.Parent = panel_ConfigBase;
                    //label_tip.Visible = true;
                }
            }


            //label_tip.Visible = false;
            //this.Enabled = true;
            //if (sn == _sn)
            //{
            //    return;
            //}
            //_sn = sn;
            //if (MonitorAllConfig.Instance().BrightnessConfigList != null)
            //{
            //    SmartLightConfigInfo tmp = MonitorAllConfig.Instance().BrightnessConfigList.Find(a => a.ScreenSN == sn);

            //    if (tmp == null)
            //    {
            //        _screenConfigInfo = new SmartLightConfigInfo();
            //        _screenConfigInfo.ScreenSN = sn;
            //        _screenConfigInfo.HwExecTypeValue = BrightnessHWExecType.SoftWareControl;
            //    }
            //    else _screenConfigInfo = (SmartLightConfigInfo)(tmp.Clone());
            //}
            //else
            //{
            //    _screenConfigInfo = new SmartLightConfigInfo(); _screenConfigInfo.ScreenSN = sn;
            //    _screenConfigInfo.HwExecTypeValue = BrightnessHWExecType.SoftWareControl;
            //}
            //switch (_screenConfigInfo.HwExecTypeValue)
            //{
            //    case BrightnessHWExecType.DisDisplayBlack:
            //        radioButton_Software.Checked = true;
            //        checkBox_ScreenDisplay.Checked = true;
            //        break;
            //    case BrightnessHWExecType.DisHardWareControl:
            //        radioButton_Software.Checked = true;
            //        if (_screenConfigInfo.DisplayHardcareConfig == null || _screenConfigInfo.DisplayHardcareConfig.OneDayConfigList == null || _screenConfigInfo.DisplayHardcareConfig.OneDayConfigList.Count == 0)
            //        {
            //            checkBox_Hareware.Checked = false;
            //        }
            //        else checkBox_Hareware.Checked = true;
            //        break;
            //    case BrightnessHWExecType.HardWareControl:
            //        radioButton_Hardware.Checked = true;
            //        break;
            //    case BrightnessHWExecType.SoftWareControl:
            //        radioButton_Software.Checked = true;
            //        checkBox_Hareware.Checked = false;
            //        checkBox_ScreenDisplay.Checked = false;
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
