using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_EMailNotifyLog : UC_ConfigBase
    {
        private string _emailStatusSended = "已发送";
        private string _emailStatusSucceed = "成功";
        private string _emailStatusFailed = "失败";
        private UC_EMailNotifyLog_VM _vm = null;
        public UC_EMailNotifyLog()
        {
            InitializeComponent();
            _vm = new UC_EMailNotifyLog_VM();
            dataGridView_logShow.AutoGenerateColumns = false;
            dataGridView_logShow.Columns[3].Width = dataGridView_logShow.Width - 155;
            notifyContentBS.DataSource = _vm;
            notifyContentBS.ResetBindings(false);
            UpdateLanguage();
            //this.dataGridView_logShow.DataSource = _vm.NotifyContentList;
        }
        private void crystalButton_addDate_Click(object sender, EventArgs e)
        {
            DateTime temp = dTP_logTime.Value;
            temp = temp.AddDays(1);
            dTP_logTime.Value = temp;
        }

        private void crystalButton_reduceDate_Click(object sender, EventArgs e)
        {
            DateTime temp = dTP_logTime.Value;
            temp = temp.AddDays(-1);
            dTP_logTime.Value = temp;
        }

        private void crystalButton_refresh_Click(object sender, EventArgs e)
        {
            notifyContentBS.Clear();
            _vm.SelectedTimes = dTP_logTime.Value.ToString("yyyy-MM-dd");
            _vm.CmdRefresh.Execute(null);
            notifyContentBS.ResetBindings(false);
        }
        private void crystalButton_deleLog_Click(object sender, EventArgs e)
        {
            notifyContentBS.Clear();
          DeleteOneLog((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "DeleteEMailLogSuccess", "日志删除成功!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "DeleteEMailLogFailed", "日志删除失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
                notifyContentBS.ResetBindings(false);
            });
            
            ShowSending("DeleteEMailLog", "正在删除邮件日志,请稍候...", true);
        }
        private void DeleteOneLog(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _vm.OnCmdDeleteLog();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }

        private void dTP_logTime_ValueChanged(object sender, EventArgs e)
        {
            notifyContentBS.Clear();
            _vm.SelectedTimes = dTP_logTime.Value.ToString("yyyy-MM-dd");
            _vm.CmdRefresh.Execute(null);
            notifyContentBS.ResetBindings(false);
        }
        private void dataGridView_logShow_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = dataGridView_logShow.Columns[e.ColumnIndex];
            if (column.Name != "Column_NotifyState")
            {
                return;
            }
            string msg = "";

            NotifyContent info = (NotifyContent)notifyContentBS[e.RowIndex];
            if (info.NotifyState == NotifyState.Failed)
            {
                msg = _emailStatusFailed;
            }
            else if (info.NotifyState == NotifyState.Succeed)
            {
                msg = _emailStatusSucceed;
            }
            else
            {
                msg = _emailStatusSended;
            }

            e.Value = msg;
        }

        private void UC_EMailNotifyLog_Load(object sender, EventArgs e)
        {
            notifyContentBS.Clear();
            dTP_logTime.Value = DateTime.Now;
            _vm.SelectedTimes = dTP_logTime.Value.ToString("yyyy-MM-dd");
            _vm.CmdRefresh.Execute(null);
            notifyContentBS.ResetBindings(false);
        }

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_EMailNotifyLog_String", out _hashTable);
            HsLangTable = _hashTable;
            _emailStatusFailed = CommonUI.GetCustomMessage(HsLangTable, "StatusFailed", "失败");
            _emailStatusSended=CommonUI.GetCustomMessage(HsLangTable, "StatusSended", "已发送");
            _emailStatusSucceed=CommonUI.GetCustomMessage(HsLangTable, "StatusSucceed", "成功");
            dataGridView_logShow.Columns["Column_NotifyTime"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ColumnNotifyTime", "通知时间");
            dataGridView_logShow.Columns["Column_ReceiveName"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ColumnReceiveName", "收信人");
            dataGridView_logShow.Columns["Column_Title"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ColumnTitle", "标题");
            dataGridView_logShow.Columns["Column_MsgContent"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ColumnMsgContent", "通知内容");
            dataGridView_logShow.Columns["Column_NotifyState"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ColumnNotifyState", "发送状态");
        }
        
    }
}
