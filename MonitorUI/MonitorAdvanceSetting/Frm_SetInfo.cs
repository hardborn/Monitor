using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public partial class Frm_SetInfo : Form
    {
        private byte _count = 0;
        public byte Count
        {
            get { return _count; }
        }

        private string _typeStr = "";

        public Frm_SetInfo(string typeStr, int count, int maxCount)
        {
            InitializeComponent();
            _typeStr = typeStr;
            if (count == 0)
            {
                checkBox_IsConnect.Checked = false;
                label_Count.Enabled = false;
                numericUpDown_Count.Enabled = false;
            }
            else
            {
                checkBox_IsConnect.Checked = true;
                label_Count.Enabled = true;
                numericUpDown_Count.Enabled = true;
                numericUpDown_Count.Value = count;
            }
            numericUpDown_Count.Maximum = maxCount;
        }

        /// <summary>
        /// ‘ÿ»Î”Ô—‘◊ ‘¥
        /// </summary>
        /// <param name="langFileName"></param>
        /// <param name="proxyLangName"></param>
        /// <returns></returns>
        private void UpdateLanguage(string langFileName)
        {
            MultiLanguageUtils.UpdateLanguage(langFileName, this);
            checkBox_IsConnect.Text += _typeStr;
        }
        private void Frm_SetInfo_Load(object sender, EventArgs e)
        {
            UpdateLanguage(Frm_FanPowerAdvanceSetting.LanFileName);
            Frm_FanPowerAdvanceSetting.AdjusterFontObj.Attach(this);
        }

        private void crystalButton_OK_Click(object sender, EventArgs e)
        {
            if (!checkBox_IsConnect.Checked)
            {
                _count = 0;
            }
            else
            {
                _count = Convert.ToByte(numericUpDown_Count.Value);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crystalButton_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkBox_IsConnect_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_Count.Enabled = checkBox_IsConnect.Checked;
            label_Count.Enabled = checkBox_IsConnect.Checked;
        }
    }
}