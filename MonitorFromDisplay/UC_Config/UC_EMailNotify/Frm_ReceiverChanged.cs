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
using Nova.Monitoring.MonitorDataManager;
using Nova.Monitoring.Common;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class Frm_ReceiverChanged : Frm_CommonBase
    {
        private Hashtable _hashtable = null;
        
        #region 事件
        public event ReceiverChangedEventHandler ReceiverChangedEvent;
        private void OnReceiverChangedEvent(object sender, ReceiverChangedEventArgs e)
        {
            if (ReceiverChangedEvent != null)
            {
                ReceiverChangedEvent(sender, e);
            }
        }
        #endregion

        #region 属性
        public bool IsModify
        {
            get
            {
                return _isModify;
            }
        }
        private bool _isModify = false;

        public DisplayType DisplayMode
        {
            get
            {
                return _displayMode;
            }
            set
            {
                _displayMode = value;
            }
        }
        private DisplayType _displayMode = DisplayType.Add;

        public string NameChanged
        {
            set
            {
                _nameChanged = value;
            }
        }
        private string _nameChanged;

        public string EmailAddr
        {
            set { _emailAddr = value; }
        }
        private string _emailAddr;
        #endregion

        public Frm_ReceiverChanged()
        {
            InitializeComponent();
        }

        public bool UpdateLanguage(Hashtable hashtable)
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.LanguageName, this);
            _hashtable = hashtable;

            return true;
        }


        #region 内部方法
        private bool CheckNameValid(string name, out string errMsg)
        {
            errMsg = "";
            if (name == string.Empty)
            {
                errMsg = CommonUI.GetCustomMessage(_hashtable, "ReceiverNameNotNull", "收件人姓名不能为空!");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckEmailValid(string email, out string errMsg)
        {
            errMsg = "";
            if (!CustomTransform.CheckEmailValid(email))
            {
               errMsg = CommonUI.GetCustomMessage(_hashtable, "EmailAddrError", "邮箱地址格式不正确!");
                return false;
            }
            else 
            {
                return true;
            }
        }
        #endregion

        private void Frm_ReceiverChanged_Load(object sender, EventArgs e)
        {
           
            string msg = "";
            if (_displayMode == DisplayType.Add)
            {
               crystalButton_apply.Text = CommonUI.GetCustomMessage(_hashtable, "AddReceivers", "添加");
            }
            else
            {
                crystalButton_apply.Text = CommonUI.GetCustomMessage(_hashtable, "ModifyReceivers", "修改");
                textBox_receiver.Text = _nameChanged;
                textBox_emailAddr.Text = _emailAddr;
            }
#if CreateSource
            FormLanguageFile.ExportFormLanguage(this, "D:\\资源\\" + this.Name + ".xml");
#endif

           // Frm_MonitorStatusDisplay.AdjusterFontObj.Attach(this);
        }

        private void crystalButton_apply_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                ctrl.Focus();
            } 
            string msg = "";
            if ((msg = errorProvider_showError.GetError(textBox_receiver)) != string.Empty)
            {
                textBox_receiver.Focus();
                textBox_receiver.SelectAll();
            }
            else if ((msg = errorProvider_showError.GetError(textBox_emailAddr)) != string.Empty)
            {
                textBox_emailAddr.Focus();
                textBox_emailAddr.SelectAll();
            }

            if (msg != string.Empty)
            {
                ShowCustomMessageBox(msg, "", MessageBoxButtons.OK, MessageBoxIconType.Error);
                return;
            }

            OnReceiverChangedEvent(this, new ReceiverChangedEventArgs(_displayMode, textBox_receiver.Text, textBox_emailAddr.Text));

            if (_displayMode == DisplayType.Add)
            {
                DialogResult = DialogResult.None;
                textBox_receiver.Clear();
                textBox_emailAddr.Clear();
                textBox_receiver.Focus();
                
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void textBox_receiver_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            if (!CheckNameValid(textBox_receiver.Text, out msg))
            {
                errorProvider_showError.SetError(textBox_receiver, msg);
            }
            else
            {
                errorProvider_showError.SetError(textBox_receiver, "");
            }
        }

        private void textBox_emailAddr_Validating(object sender, CancelEventArgs e)
        {
            string msg = "";
            if (!CheckEmailValid(textBox_emailAddr.Text, out msg))
            {
                errorProvider_showError.SetError(textBox_emailAddr, msg);
            }
            else
            {
                errorProvider_showError.SetError(textBox_emailAddr, "");
            }
        }
    }
}