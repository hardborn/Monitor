using Nova.Drawing;
using Nova.LCT.GigabitSystem.UI;
using Nova.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class Frm_CommonBase : Form
    {
        private Form_ProcessingEx _processForm = null;
        public FontAdjuster adjusterFontObj = new FontAdjuster();
        public Frm_CommonBase()
        {
            InitializeComponent();
            UpdateFont();
        }

        protected virtual bool UpdateFont()
        {
            return UpdateFont(CommonUI.SoftwareFont);
        }

        public bool UpdateFont(Font font)
        {
            CommonUI.SoftwareFont = font;
            adjusterFontObj.Attach(this);
            adjusterFontObj.UpdateFont(font);
            return true;
        }

        #region 公用弹出框，进度条
        protected delegate DialogResult ShowCustomMessageBoxDele(string msg, string title,
            MessageBoxButtons buttons, Nova.Windows.Forms.MessageBoxIconType icon);
        protected DialogResult ShowCustomMessageBox(string msg, string title,
            MessageBoxButtons buttons, Nova.Windows.Forms.MessageBoxIconType icon)
        {
            if (!this.InvokeRequired)
            {
                return CustomMessageBox.ShowCustomMessageBox(this, msg, title, buttons, icon);
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
