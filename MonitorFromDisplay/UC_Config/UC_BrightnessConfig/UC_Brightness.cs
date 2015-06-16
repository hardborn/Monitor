using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.MonitorDataManager;
using Nova.LCT.GigabitSystem.HWConfigAccessor;
using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig;
using Nova.Resource.Language;
using System.Collections;
using Nova.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_Brightness : UC_ConfigBase
    {
        private string _sn;
        private Hashtable _langTable;
        private SmartLightConfigInfo _screenConfigInfo = new SmartLightConfigInfo();
        public UC_Brightness()
        {
            InitializeComponent();
            UpdateLang(CommonUI.BrightnessConfigLangPath);

            if (MonitorAllConfig.Instance().BrightnessConfigList != null
                && MonitorAllConfig.Instance().BrightnessConfigList.Count > 0)
            {
                int autoBrightPeriod = 0;
                int readLuxCnt = 0;
                if (MonitorAllConfig.Instance().BrightnessConfigList[0].DisplayHardcareConfig != null
                    && MonitorAllConfig.Instance().BrightnessConfigList[0].HwExecTypeValue==BrightnessHWExecType.HardWareControl)
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

        public void UpdateLang()
        {
            UpdateLang(CommonUI.BrightnessConfigLangPath);
        }
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_Brightness", this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "UC_Brightness_String", out _langTable);
            HsLangTable = _langTable;
            InitialLangTable();
        }
        private void InitialLangTable()
        {

        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            SaveConfig((result) =>
            {
                switch (result)
                {
                    case 0:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "savesuccess", "保存成功"));
                        break;
                    case 1:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "savesuccessNoConfig", "未配置亮度信息"));
                        break;
                    default:
                        CustomMessageBox.ShowCustomMessageBox(this.ParentForm, CommonUI.GetCustomMessage(_langTable, "savefailed", "保存失败"));
                        break;
                }
                CloseProcessForm();
            });
            ShowSending("SaveBrightConfig", "正在保存亮度配置,请稍候...", true);
            return;
        }
        private void SaveConfig(Action<int> callBackAction)
        {
            Func<int> func = () =>
            {
                return MonitorAllConfig.Instance().SaveBrigthnessAutoConfig((int)numericUpDown_AutoBrightPeriod.Value,
                    (int)numericUpDown_ReadLuxCnt.Value, checkBox_IsSmartEnable.Checked, checkBox_IsGradualEnable.Checked);
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }
    }
}
