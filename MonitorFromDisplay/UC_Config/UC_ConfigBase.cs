using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.LCT.GigabitSystem.Common;
using Nova.LCT.GigabitSystem.UI;
using Nova.Windows.Forms;
using Nova.Drawing;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_ConfigBase : UserControl
    {
        private Form_ProcessingEx _processForm = null;
        protected FontAdjuster adjusterFontObj = new FontAdjuster();
        public UC_ConfigBase()
        {
            InitializeComponent();
            UnRegister();
            Register();
            UpdateFont();
        }
        public System.Collections.Hashtable HsLangTable { get; set; }

        protected virtual void Register()
        {

        }

        protected virtual void UnRegister()
        {

        }

        protected bool UpdateFont()
        {
            adjusterFontObj.Attach(this);
            adjusterFontObj.UpdateFont(CommonUI.SoftwareFont);            
            return true;
        }

        public string ScreenSN { get; set; }

        public virtual void crystalButton_OK_Location(Point value)
        {
            crystalButton_OK.Location = value;
        }

        private void UC_ConfigBase_VisibleChanged(object sender, EventArgs e)
        {
            //if (this.Visible)
            //{
            //    Register();
            //}
            //else
            //{
            //    UnRegister();
            //}
        }
        protected void ShowSending(string msgKey,string msgDefault)
        {
            ShowSending(msgKey, msgDefault, false);
        }
        protected void ShowSending(string msgKey, string msgDefault,bool isDisplay)
        {
            string msg = string.Empty;
            if (!CustomTransform.GetLanguageString(msgKey, HsLangTable, out msg))
            {
                msg = msgDefault;
            }
            ShowProcessForm(msg, isDisplay);
        }

        #region 公用弹出框，进度条
        protected delegate DialogResult ShowCustomMessageBoxDele(string msg, string title,
            MessageBoxButtons buttons, Nova.Windows.Forms.MessageBoxIconType icon);
        protected DialogResult ShowCustomMessageBox(string msg, string title,
            MessageBoxButtons buttons, Nova.Windows.Forms.MessageBoxIconType icon)
        {
            if (!this.InvokeRequired)
            {
                return CustomMessageBox.ShowCustomMessageBox(this.ParentForm, msg, title, buttons, icon);
            }
            else
            {
                ShowCustomMessageBoxDele cs = new ShowCustomMessageBoxDele(ShowCustomMessageBox);
                return (DialogResult)this.Invoke(cs, new object[] { msg, title, buttons, icon });
            }
        }

        protected delegate void ShowProcessFormDele(string msg, bool isShowCancelButton);
        protected void ShowProcessForm(string msg, bool isShowCancelButton)
        {
            if (!this.InvokeRequired)
            {
                if (_processForm == null || _processForm.IsDisposed)
                {

                }
                else
                {
                    _processForm.Dispose();
                }

                _processForm = new Form_ProcessingEx();
                _processForm.StartPosition = FormStartPosition.CenterParent;
                _processForm.IsShowCancelButton = isShowCancelButton;
                _processForm.ProcessText = msg;
                _processForm.ShowDialog(this);
            }
            else
            {
                ShowProcessFormDele cs = new ShowProcessFormDele(ShowProcessForm);
                this.Invoke(cs, new object[] { msg, isShowCancelButton });
            }
        }

        protected delegate void CloseProcessFormDele();
        protected void CloseProcessForm()
        {
            if (_processForm != null)
            {
                if (!this.InvokeRequired)
                {
                    if (_processForm != null && !_processForm.IsDisposed)
                    {
                        _processForm.Close();
                        _processForm.Dispose();
                    }
                }
                else
                {
                    CloseProcessFormDele cs = new CloseProcessFormDele(CloseProcessForm);
                    this.Invoke(cs);
                }
            }
        }
        #endregion
    }
}
