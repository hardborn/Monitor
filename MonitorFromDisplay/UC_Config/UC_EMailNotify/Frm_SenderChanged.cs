using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.Resource.Language;
using Nova.Windows.Forms;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class Frm_SenderChanged : Frm_CommonBase
    {

        #region 属性
        public string EmailAddr
        {
            get
            {
                return textBox_emailAddr.Text;
            }
            set
            {
                textBox_emailAddr.Text = value;
            }
        }
        public string Password 
        {
            get
            {
                return textBox_passWord.Text;
            }
            set
            {
                textBox_passWord.Text = value;
            }
        }
        public string SMTPServer
        {
            get
            {
                return textBox_smtpServer.Text;
            }
            set
            {
                textBox_smtpServer.Text = value;
            }
        }
        public int Port
        {
            get
            {
                return int.Parse(numberTextBox_port.Text);
            }
            set
            {
                numberTextBox_port.Text = value.ToString();
            }
        }

        public bool IsEnableSsl
        {
            get
            {
                return checkBox_SSLOpen.Checked;
            }
            set
            {
                checkBox_SSLOpen.Checked = value;
            }
        }
        
        #endregion
        private Hashtable _hashtable = null;
        public Frm_SenderChanged()
        {
            InitializeComponent();
        }
        public bool UpdateLanguage(Hashtable hashtable)
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
           _hashtable=hashtable;
            
            return true;
        }
        #region 内部方法
        private bool CheckEmailAddr(string emailAddr, out string errorText)
        {
            
            errorText = "";
            if (!CustomTransform.CheckEmailValid(emailAddr))
            {
                errorText = CommonUI.GetCustomMessage(_hashtable, "EmailAddrError", "邮箱地址格式不正确!");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckEmailPW(string passWord, out string errorText)
        {
            if (passWord == string.Empty)
            {
               errorText = CommonUI.GetCustomMessage(_hashtable, "EmailAddrNotNull", "邮箱密码不能为空!");
                return false;
            }
            else 
            {
                errorText = "";
                return true;
            }
        }
        private bool CheckSMTP(string smtp, out string errorText)
        {
            if (smtp == "")
            {
                errorText = CommonUI.GetCustomMessage(_hashtable, "SMTPServerNotNull", "SMTP服务器不能为空!");
                return false;
            }
            else
            {
                errorText = "";
                return true;
            }
        }
        private bool CheckPort(string port, out string errorText)
        {
            ushort temp = 0;
            bool res = ushort.TryParse(port, out temp);
            if (!res || temp == 0)
            {
               errorText = CommonUI.GetCustomMessage(_hashtable, "EmailPortError", "端口格式不正确!");
               return false;
            }
            else
            {
                errorText = "";
                return true;
            }
        }
        #endregion


        private void Frm_SenderChanged_Load(object sender, EventArgs e)
        {
#if CreateSource
            FormLanguageFile.ExportFormLanguage(this, "D:\\资源\\" + this.Name + ".xml");
#endif

        }

        private void textBox_passWord_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            bool res = CheckEmailPW(textBox_passWord.Text, out msg);
            if (!res)
            {
                errorProvider_showError.SetError(textBox_passWord, msg);
            }
            else
            {
                errorProvider_showError.SetError(textBox_passWord, msg);
            }
        }

        private void textBox_emailAddr_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            bool res = CheckEmailAddr(textBox_emailAddr.Text, out msg);
            if (!res)
            {
                errorProvider_showError.SetError(textBox_emailAddr, msg);
            }
            else
            {
                errorProvider_showError.SetError(textBox_emailAddr, msg);
            }
        }

        private void textBox_smtpServer_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            bool res = CheckSMTP(textBox_smtpServer.Text, out msg);
            if (!res)
            {
                errorProvider_showError.SetError(textBox_smtpServer, msg);
            }
            else
            {
                errorProvider_showError.SetError(textBox_smtpServer, msg);
            }
        }

        private void numberTextBox_port_Validating(object sender, EventArgs e)
        {
            string msg = "";
            bool res = CheckPort(numberTextBox_port.Text, out msg);
            if (!res)
            {
                errorProvider_showError.SetError(numberTextBox_port, msg);
            }
            else
            {
                errorProvider_showError.SetError(numberTextBox_port, msg);
            }
        }

        private void crystalButton_apply_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                ctrl.Focus();
            }
            string msg = "";
            if ((msg = errorProvider_showError.GetError(textBox_emailAddr)) != string.Empty)
            {
                textBox_emailAddr.Focus();
                textBox_emailAddr.SelectAll();
            }
            else if ((msg = errorProvider_showError.GetError(textBox_passWord)) != string.Empty)
            {
                textBox_passWord.Focus();
                textBox_passWord.SelectAll();

            }
            else if ((msg = errorProvider_showError.GetError(textBox_smtpServer)) != string.Empty)
            {
                textBox_smtpServer.Focus();
                textBox_smtpServer.SelectAll();
            }
            else if ((msg = errorProvider_showError.GetError(numberTextBox_port)) != string.Empty)
            {
                numberTextBox_port.Focus();
                numberTextBox_port.SelectAll();
            }
            if (msg != "")
            {
                CustomMessageBox.ShowCustomMessageBox(this, msg, "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                DialogResult = DialogResult.None;
                return;
            }
            crystalButton_apply.Focus();
        }

        private void Frm_SenderChanged_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}