using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.Common;
using Nova.Resource.Language;
using System.Collections;
using Nova.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_EMailNotify : UC_ConfigBase
    {
        private int _curSelectReceiverIndex = -1;
        private UC_EMailNotify_VM _uc_EMailNotify_VM = new UC_EMailNotify_VM();
        public UC_EMailNotify()
        {
            InitializeComponent();
            eMailNotifyConfigBS.DataSource = _uc_EMailNotify_VM;
            this.dataGridView_receiver.DataSource = _uc_EMailNotify_VM.NotifyConfig.ReceiveInfoList;
            dataGridView_receiver.Columns[0].Width = 200;
            dataGridView_receiver.Columns[1].Width = dataGridView_receiver.Width-205;
            UpdateLanguage();
            eMailNotifyConfigBS.ResetCurrentItem();
        }

        private void linkLabel_modifySender_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Frm_SenderChanged frm = new Frm_SenderChanged())
            {
                frm.UpdateLanguage(HsLangTable);
                EMailNotifyConfig temp = _uc_EMailNotify_VM.NotifyConfig;
                frm.EmailAddr = temp.EmailAddr;
                frm.Password = temp.Password;
                frm.SMTPServer = temp.SmtpServer;
                frm.Port = temp.Port;
                frm.IsEnableSsl = temp.IsEnableSsl;
                if (frm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    temp = _uc_EMailNotify_VM.NotifyConfig;
                    temp.EmailAddr = frm.EmailAddr;
                    temp.Password = frm.Password;
                    temp.SmtpServer = frm.SMTPServer;
                    temp.Port = frm.Port;
                    temp.IsEnableSsl = frm.IsEnableSsl;
                    eMailNotifyConfigBS.ResetCurrentItem();
                }
            }
        }

        private void crystalButton_useDefault_Click(object sender, EventArgs e)
        {
            EMailNotifyConfig temp = _uc_EMailNotify_VM.NotifyConfig;
            temp.EmailAddr = EMailNotifyConfig.DEFAULT_EMAIL_ADDR;
            temp.Password = EMailNotifyConfig.DEFAULT_EMAIL_PW;
            temp.SmtpServer = EMailNotifyConfig.DEFAULT_SMTP_SERVER;
            temp.Port = EMailNotifyConfig.DEFAULT_EMAIL_PORT;
            eMailNotifyConfigBS.ResetCurrentItem();
        }

        private void crystalButton_addRecv_Click(object sender, EventArgs e)
        {
            using (Frm_ReceiverChanged frm = new Frm_ReceiverChanged())
            {
                frm.UpdateLanguage(HsLangTable);
                frm.ReceiverChangedEvent += new ReceiverChangedEventHandler(frm_ReceiverChangedEvent);
                frm.DisplayMode = DisplayType.Add;
                frm.ShowDialog();
            }
        }
        private void frm_ReceiverChangedEvent(object sender, ReceiverChangedEventArgs e)
        {
            EMailNotifyConfig temp = _uc_EMailNotify_VM.NotifyConfig;
            if (e.DisplayType == DisplayType.Add)
            {
                temp.ReceiveInfoList.Add(new ReceiverInfo(e.Name, e.EmailAddr));
            }
            else
            {
                ReceiverInfo info = temp.ReceiveInfoList[_curSelectReceiverIndex];
                info.Name = e.Name;
                info.EmailAddr = e.EmailAddr;
            }
            eMailNotifyConfigBS.ResetBindings(false);
        }
        private void crystalButton_modifyRecv_Click(object sender, EventArgs e)
        {
            if (dataGridView_receiver.SelectedRows.Count == 0)
            {
                ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "SelectEditReceiver", "请选择要编辑的收件人!"),
                                "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                return;
            }
            using (Frm_ReceiverChanged frm = new Frm_ReceiverChanged())
            {
                frm.UpdateLanguage(HsLangTable);
                _curSelectReceiverIndex = dataGridView_receiver.SelectedRows[0].Index;
                frm.ReceiverChangedEvent += new ReceiverChangedEventHandler(frm_ReceiverChangedEvent);

                EMailNotifyConfig temp = _uc_EMailNotify_VM.NotifyConfig;
                ReceiverInfo info = temp.ReceiveInfoList[_curSelectReceiverIndex];

                frm.DisplayMode = DisplayType.Modify;
                frm.NameChanged = info.Name;
                frm.EmailAddr = info.EmailAddr;
                frm.ShowDialog();
            }
        }

        private void crystalButton_deleteRecv_Click(object sender, EventArgs e)
        {
            if (dataGridView_receiver.SelectedRows.Count == 0)
            {
                ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable,"SelectDeleteReceiver","请选择要删除的收件人!"),
                                "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                return;
            }
            if (ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "SureDeleteReceiver", "确认要删除选择的收件人!"),
                                "", MessageBoxButtons.OKCancel, MessageBoxIconType.Question)
                != DialogResult.OK)
            {
                return;
            }

            foreach (DataGridViewRow row in dataGridView_receiver.SelectedRows)
            {
                dataGridView_receiver.Rows.Remove(row);
            }
        }

        private void linkLabel_PeriodicReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkBox_SystemRefresh.Checked)
            {
                using (Frm_PeriodicReport periodicReport = new Frm_PeriodicReport())
                {
                    periodicReport.UpdateLanguage(HsLangTable);

                    EMailNotifyConfig temp = _uc_EMailNotify_VM.NotifyConfig;
                    periodicReport.SendTimer = temp.SendTimer;
                    periodicReport.SendModel = temp.SendMailModel;
                    periodicReport.SendMailWeek = temp.SendMailWeek;
                    periodicReport.StartPosition = FormStartPosition.CenterScreen;
                    if (periodicReport.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        temp = _uc_EMailNotify_VM.NotifyConfig;
                        temp.SendTimer = periodicReport.SendTimer;
                        temp.SendMailModel = periodicReport.SendModel;
                        temp.SendMailWeek = periodicReport.SendMailWeek;
                        eMailNotifyConfigBS.ResetCurrentItem();
                    }
                }
            }
        }

        private void UpdateLanguage()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            Hashtable _hashTable = null;
            MultiLanguageUtils.ReadLanguageResource(CommonUI.LanguageName, "UC_EMailNotify_String", out _hashTable);
            HsLangTable = _hashTable;

            dataGridView_receiver.Columns["Name"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "ReceiverName", "姓名");
            dataGridView_receiver.Columns["EmailAddr"].HeaderText =
                CommonUI.GetCustomMessage(HsLangTable, "EmailAddr", "邮箱地址");
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            SaveConfig((result) =>
            {
                if (result)
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "SaveEMailConfigSuccess", "保存邮件配置成功!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                else
                {
                    ShowCustomMessageBox(CommonUI.GetCustomMessage(HsLangTable, "SaveEMailConfigFailed", "保存邮件配置失败!"),
                        null, MessageBoxButtons.OK, Nova.Windows.Forms.MessageBoxIconType.Alert);
                }
                CloseProcessForm();
            });
            ShowSending("SaveEMailConfig", "正在保存邮件配置,请稍候...", true);
        }
        private void SaveConfig(Action<bool> callBackAction)
        {
            Func<bool> func = () =>
            {
                return _uc_EMailNotify_VM.OnCmdSaveTo();
            };
            func.BeginInvoke((ar) =>
            {
                var result = func.EndInvoke(ar);
                this.BeginInvoke(callBackAction, new object[] { result });
            }, null);
        }
    }
}
