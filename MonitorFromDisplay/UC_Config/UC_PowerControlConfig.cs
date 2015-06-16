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
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_PowerControlConfig : UC_ConfigBase
    {
        UC_PowerControlConfig_VM _vm = null;
        public UC_PowerControlConfig()
        {
            InitializeComponent();
            _vm = new UC_PowerControlConfig_VM();            
            uCPowerControlConfigVMBindingSource.DataSource = _vm; 
            bindingSource1.DataSource = _vm.PowerConfig;
            dataGridViewBaseEx_FuncCardPower.DataSource = bindingSource1;
            dataGridViewBaseEx_FuncCardPower.Columns[1].Width = 200;
            dataGridViewBaseEx_FuncCardPower.Columns[1].HeaderText = "设备别名";
            (dataGridViewBaseEx_FuncCardPower.Columns[1] as DataGridViewTextBoxColumn).MaxInputLength = 20;
            dataGridViewBaseEx_FuncCardPower.Columns[0].Width = 200;
            dataGridViewBaseEx_FuncCardPower.Columns[0].HeaderText = "设备位置";
            dataGridViewBaseEx_FuncCardPower.Columns[0].ReadOnly = true;
            UpdateLanguage();
            RefreshFuncCardPower();
        }
        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_PowerControlConfig_String", out _hashTable);
            HsLangTable = _hashTable;
            dataGridViewBaseEx_FuncCardPower.Columns[1].HeaderText = CommonUI.GetCustomMessage(_hashTable,"FuncAliaName","设备别名");
            dataGridViewBaseEx_FuncCardPower.Columns[0].HeaderText = CommonUI.GetCustomMessage(_hashTable,"FuncLocation","设备位置");
        }
        protected override void Register()
        {
            Messenger.Default.Register<string>(this, MsgToken.MSG_MonitorCardPowerConfig, OnMsgMonitorCardPowerConfig);
            MonitorAllConfig.Instance().GetFunctionCardPowerEvent += UC_PowerControlConfig_GetFunctionCardPowerEvent;
        }
        protected override void UnRegister()
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_MonitorCardPowerConfig, OnMsgMonitorCardPowerConfig);
            MonitorAllConfig.Instance().GetFunctionCardPowerEvent -= UC_PowerControlConfig_GetFunctionCardPowerEvent;
        }
        private void OnMsgMonitorCardPowerConfig(string sn)
        {
            //RefreshFuncCardPower();
        }
        delegate void FunctionCardPowerEventDel(object sender, EventArgs e);
        void UC_PowerControlConfig_GetFunctionCardPowerEvent(object sender, EventArgs e)
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
                FunctionCardPowerEventDel cs = new FunctionCardPowerEventDel(UC_PowerControlConfig_GetFunctionCardPowerEvent);
                this.BeginInvoke(cs,new object[]{sender,e});
            }
            else
            {
                _vm.CmdInitialize.Execute(null);
                bindingSource1.ResetBindings(false);
                if (_vm.PowerConfig == null || _vm.PowerConfig.Count == 0)
                {
                    crystalButton_OK.Enabled = false;
                }
                else
                {
                    crystalButton_OK.Enabled = true;
                } 
                this.Invalidate();
                CloseProcessForm();
            }
        }

        private void crystalButton_RefreshPower_Click(object sender, EventArgs e)
        {
            RefreshFuncCardPower();
        }
        private void RefreshFuncCardPower()
        {
            MonitorAllConfig.Instance().RefreshAllFunctionCardInfos();
            ShowSending("RefreshFuncCardPower", "正在获取多功能卡信息,请稍候...", true);
        }

        private void dataGridViewBaseEx_FuncCardPower_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                FunctionCardRoadInfo funcRoad = (FunctionCardRoadInfo)e.Value;
                e.Value = funcRoad.ToString();
            }
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            SaveConfig((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "PowerControlSaveSuccess", "外设电源配置保存成功，用于策略选择!"), 
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "PowerControlSaveFailed", "外设电源配置保存失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
             });
            ShowSending("PowerControlConfigSave", "正在保存外设电源配置,请稍候...", true);
        }

        private void SaveConfig(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.OnCmdSaveConfig();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.Invoke(callBackAction, new object[] { result });
            }, null);
        }

        private void dataGridViewBaseEx_FuncCardPower_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewBaseEx_FuncCardPower_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1 || e.RowIndex < 0)
            {
                return;
            }

            string strName = string.Empty;
            if (dataGridViewBaseEx_FuncCardPower[1, e.RowIndex].Value == null ||
                string.IsNullOrEmpty(dataGridViewBaseEx_FuncCardPower[1, e.RowIndex].Value.ToString()))
            {
                dataGridViewBaseEx_FuncCardPower[1, e.RowIndex].ToolTipText = "不允许名字为空";
                dataGridViewBaseEx_FuncCardPower[1, e.RowIndex].Style.BackColor = Color.Yellow;
                crystalButton_OK.Enabled = false;
                return;
            }
            Dictionary<int, string> values = new Dictionary<int, string>();
            int dwCount = dataGridViewBaseEx_FuncCardPower.Rows.Count;
            bool isExistError = false;
            for (int i = 0; i < dwCount; i++)
            {
                if (dataGridViewBaseEx_FuncCardPower[1, i].Value == null)
                {
                    dataGridViewBaseEx_FuncCardPower[1, i].Style.BackColor = Color.Yellow;
                    dataGridViewBaseEx_FuncCardPower[1, i].ToolTipText 
                    = CommonUI.GetCustomMessage(HsLangTable, "PowerControlTipEmpty","不允许名字为空");
                    isExistError = true;
                }
                else
                {
                    values.Add(i, dataGridViewBaseEx_FuncCardPower[1, i].Value.ToString());
                }
            }
            foreach (KeyValuePair<int, string> keyvalue in values)
            {
                if (values.Values.ToList().FindAll(a => a == keyvalue.Value).Count > 1)
                {
                    dataGridViewBaseEx_FuncCardPower[1, keyvalue.Key].Style.BackColor = Color.Yellow;
                    dataGridViewBaseEx_FuncCardPower[1, keyvalue.Key].ToolTipText
                        = CommonUI.GetCustomMessage(HsLangTable, "PowerControlTipDuplicate", "不允许名字重复");
                    isExistError = true;
                }
                else
                {
                    dataGridViewBaseEx_FuncCardPower[1, keyvalue.Key].Style.BackColor = Color.White;
                    dataGridViewBaseEx_FuncCardPower[1, keyvalue.Key].ToolTipText = "";
                }
            }

            if (isExistError == true)
            {
                crystalButton_OK.Enabled = false;
            }
            else
            {
                crystalButton_OK.Enabled = true;
            }
        }
    }
}
