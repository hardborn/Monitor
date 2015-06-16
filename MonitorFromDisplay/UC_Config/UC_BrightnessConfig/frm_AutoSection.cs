using Nova.LCT.GigabitSystem.Common;
using Nova.Resource.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config.UC_BrightnessConfig
{
    public partial class frm_AutoSection : Frm_CommonBase
    {
        private Hashtable _languageTable;
        private FastSegmentParam _fastSegmentParam = new FastSegmentParam();

        public FastSegmentParam AutoSectionParam
        {
            get { return _fastSegmentParam; }
        }

        public frm_AutoSection()
        {
            InitializeComponent();
            this.Load += frm_AutoSection_Load;
        }

        void frm_AutoSection_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
        }

        private void ApplyUILanguageTable()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.OpticalProbeConfigLangPath, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.OpticalProbeConfigLangPath, "frm_AutoSection", out _languageTable);

            if (_languageTable == null || _languageTable.Count == 0)
                return;

          
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            
            _fastSegmentParam.SegmentNum = int.Parse(this.sectionHScrollBar.Value.ToString());
            _fastSegmentParam.MaxDisplayBright = int.Parse(this.maxLedNumericUpDown.Value.ToString());
            _fastSegmentParam.MaxEnvironmentBright = int.Parse(this.maxEnvironmentNumericUpDown.Value.ToString());
            _fastSegmentParam.MinDisplayBright = int.Parse(this.minLedNumericUpDown.Value.ToString());
            _fastSegmentParam.MinEnvironmentBright = int.Parse(this.minEnvironmentNumericUpDown.Value.ToString());
            
            if (_fastSegmentParam.MaxDisplayBright <= _fastSegmentParam.MinDisplayBright)
            {
                string errorTitleText = CommonUI.GetCustomMessage(_languageTable, "errorTitle", "错误");
                string errorText = CommonUI.GetCustomMessage(_languageTable, "ledBrightness_error01", "屏体亮度最大值不能小于屏体亮度的最小值");
                ShowCustomMessageBox(errorText, errorTitleText, MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
                //MessageBox.Show(this, "屏体亮度最大值不能小于屏体亮度的最小值", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_fastSegmentParam.MaxEnvironmentBright <= _fastSegmentParam.MinEnvironmentBright)
            {
                string errorTitleText = CommonUI.GetCustomMessage(_languageTable, "errorTitle", "错误");
                string errorText = CommonUI.GetCustomMessage(_languageTable, "environmentBrightness_error01", "环境亮度最大值不能小于环境亮度的最小值");
                ShowCustomMessageBox(errorText, errorTitleText, MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
                //MessageBox.Show(this, "环境亮度最大值不能小于环境亮度的最小值", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void sectionHScrollBar_ValueChanged(object sender, EventArgs e)
        {
            HScrollBar hScrollBar = (HScrollBar)sender;
            if (hScrollBar == null)
            {
                return;
            }
            var currentValue = hScrollBar.Value;
            sectionCountLabel.Text = currentValue.ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
