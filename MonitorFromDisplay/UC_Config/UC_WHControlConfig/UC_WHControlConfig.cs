using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using GalaSoft.MvvmLight.Messaging;
using Nova.Monitoring.Common;
using Nova.LCT.GigabitSystem.Common;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_WHControlConfig : UC_ConfigBase
    {
        private UC_WHControlConfig_VM _whCfg_VM;
        private Hashtable _langTable;
        public UC_WHControlConfig()
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
            _whCfg_VM = new UC_WHControlConfig_VM();
            uCWHControlConfigVMBindingSource.DataSource = _whCfg_VM;
            vMBaseListBindingSource.DataSource = _whCfg_VM.VM_BaseList;
            dataGridView_ControlConfig.DataSource = vMBaseListBindingSource;

            foreach (DataGridViewColumn item in dataGridView_ControlConfig.Columns)
            {
                if (item.Name.Equals("StrategyTypeStr"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "strategytypestr", "控制类型");
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                //else if (item.Name.Equals("SN"))
                //{
                //    item.HeaderText = "显示屏";
                //    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //}
                else if (item.Name.Equals("Condition"))
                {
                    item.HeaderText = CommonUI.GetCustomMessage(_langTable, "condition", "条件");
                    item.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    item.Visible = false;
                }
            }
        }
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_WHControlConfig", this);
            MultiLanguageUtils.ReadLanguageResource(langResFile, "UC_WHControlConfig_String", out _langTable);
            InitialLangTable();
        }
        private void InitialLangTable()
        {
            ControlConfigLangTable.StrategyTypeTable = new Dictionary<StrategyType, string>();
            ControlConfigLangTable.StrategyTypeTable.Add(StrategyType.TemperatureStrategy, CommonUI.GetCustomMessage(_langTable, "temperaturestrategy", "温度"));
            ControlConfigLangTable.StrategyTypeTable.Add(StrategyType.SmokeStrategy, CommonUI.GetCustomMessage(_langTable, "smokestrategy", "烟雾"));
            ControlConfigLangTable.ConditionAlgorithmTypeTable = new Dictionary<ConditionAlgorithm, string>();
            ControlConfigLangTable.ConditionAlgorithmTypeTable.Add(ConditionAlgorithm.AverageAlgorithm, CommonUI.GetCustomMessage(_langTable, "averagealgorithm", "平均值"));
            ControlConfigLangTable.ConditionAlgorithmTypeTable.Add(ConditionAlgorithm.MaxValueAlgorithm, CommonUI.GetCustomMessage(_langTable, "maxvaluealgorithm", "最高值"));
        }
        #region 消息注册
        protected override void Register()
        {
            Messenger.Default.Register<string>(this, MsgToken.MSG_ControlConfig, OnMsgControlConfig);
        }
        private void OnMsgControlConfig(string sn)
        {
            if (_whCfg_VM.SelectedScreenSN == sn) return;
            dataGridView_ControlConfig.Rows.Clear();
            vMBaseListBindingSource.Clear();
            _whCfg_VM.Initialize(sn);
            if (_whCfg_VM.IsEnableCtrl)
                label_tip.Visible = false;
            else
                label_tip.Visible = true;
            vMBaseListBindingSource.ResetBindings(false);
        }
        protected override void UnRegister()
        {
            Messenger.Default.Unregister<string>(this, MsgToken.MSG_ControlConfig, OnMsgControlConfig);
        }
        #endregion
        private void dataGridView_ControlConfig_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_ControlConfig.SelectedRows.Count == 0)
            {
                button_edit.Enabled = false;
                ToolStripMenuItem_edit.Enabled = false;
                button_delete.Enabled = false;
                ToolStripMenuItem_delete.Enabled = false;
                button_deleteAll.Enabled = false;
                ToolStripMenuItem_deleteAll.Enabled = false;
                _whCfg_VM.SelectedStrategy = null;
                return;
            }
            else
            {
                button_edit.Enabled = true;
                ToolStripMenuItem_edit.Enabled = true;
                button_delete.Enabled = true;
                ToolStripMenuItem_delete.Enabled = true;
                button_deleteAll.Enabled = true;
                ToolStripMenuItem_deleteAll.Enabled = true;
                if (_whCfg_VM.VM_BaseList.Count != 0)
                    _whCfg_VM.SelectedStrategy = _whCfg_VM.VM_BaseList[dataGridView_ControlConfig.SelectedRows[0].Index];
            }
        }
        private void dataGridView_ControlConfig_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                using (SolidBrush b = new SolidBrush(dataGridView_ControlConfig.RowHeadersDefaultCellStyle.ForeColor))
                {
                    string linenum = e.RowIndex.ToString();
                    int linen = 0;
                    linen = Convert.ToInt32(linenum) + 1;
                    string line = linen.ToString();
                    e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 5);
                    SolidBrush B = new SolidBrush(Color.Red);
                }
            }
            catch (Exception ex)
            {
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch:dataGridView_ControlConfig_RowPostPaint Error:" + ex.ToString(), true);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            frm_WHControlConfig_Editor frm_Editor = new frm_WHControlConfig_Editor(_whCfg_VM.SelectedScreenSN, null, _whCfg_VM.TemStrategyList, _whCfg_VM.SmokeStrategyList);
            if (frm_Editor.ShowDialog() != DialogResult.OK) return;
            _whCfg_VM.AddStartegy(frm_Editor.Strategy);
            vMBaseListBindingSource.ResetBindings(false);
        }
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (_whCfg_VM.SelectedStrategy != null)
            {
                if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_delete", "确定删除所选配置？"), "", MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIconType.Question) == DialogResult.OK)
                {
                    if (_whCfg_VM.DeleteStartegy(_whCfg_VM.VM_BaseList[dataGridView_ControlConfig.SelectedRows[0].Index]))
                    {
                        ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_deletesuccess", "删除成功"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
                        vMBaseListBindingSource.ResetBindings(false);
                    }
                    else ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_deletefailed", "删除失败"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
                }
            }
        }
        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            if (_whCfg_VM.Save()) ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "savesuccess", "保存成功"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
            else ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "savefailed", "保存失败"), "", MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Alert);
        }
        private void button_edit_Click(object sender, EventArgs e)
        {
            frm_WHControlConfig_Editor frm_Editor = new frm_WHControlConfig_Editor(_whCfg_VM.SelectedScreenSN, _whCfg_VM.SelectedStrategy, _whCfg_VM.TemStrategyList, _whCfg_VM.SmokeStrategyList);
            if (frm_Editor.ShowDialog() != DialogResult.OK) return;
            _whCfg_VM.ModifyStartegy(frm_Editor.Strategy);
            vMBaseListBindingSource.ResetBindings(false);
        }

        private void button_deleteAll_Click(object sender, EventArgs e)
        {
            if (_whCfg_VM.VM_BaseList == null || _whCfg_VM.VM_BaseList.Count == 0)
                return;
            if (ShowCustomMessageBox(CommonUI.GetCustomMessage(_langTable, "msg_deleteall", "确定清理所有配置？"), "", MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIconType.Question) == DialogResult.OK)
            {
                if (_whCfg_VM.VM_BaseList != null)
                {
                    UC_WHControlConfig_Tem_VM tem_VM;
                    UC_WHControlConfig_Smoke_VM smoke_VM;
                    List<UC_WHControlConfig_VM_Base> straList = new List<UC_WHControlConfig_VM_Base>();
                    foreach (var item in _whCfg_VM.VM_BaseList)
                    {
                        if (item.StratlType == StrategyType.TemperatureStrategy)
                        {
                            tem_VM = (UC_WHControlConfig_Tem_VM)item;
                            straList.Add(new UC_WHControlConfig_Tem_VM(tem_VM.SN, tem_VM.ID, tem_VM.ConditionAlgor, tem_VM.LessThan, tem_VM.GreaterThan, tem_VM.IsControlBrightness, tem_VM.Brightness, tem_VM.PowerCtrlDic));
                        }
                        else if (item.StratlType == StrategyType.SmokeStrategy)
                        {
                            smoke_VM = (UC_WHControlConfig_Smoke_VM)item;
                            straList.Add(new UC_WHControlConfig_Smoke_VM(smoke_VM.SN, smoke_VM.ID, smoke_VM.GreaterThan, item.PowerCtrlDic));
                        }
                    }
                    foreach (var item in straList)
                    {
                        _whCfg_VM.DeleteStartegy(item);
                    }
                    vMBaseListBindingSource.ResetBindings(false);
                }

            }
        }

    }
}
