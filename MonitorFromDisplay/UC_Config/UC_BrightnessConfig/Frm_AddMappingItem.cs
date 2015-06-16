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
    public partial class Frm_AddMappingItem : Frm_CommonBase
    {
        private Hashtable _languageTable;
        public int EnvironmentItem { get; set; }
        public int LedItem { get; set; }
        DataGridView _dgv;
        public Frm_AddMappingItem(DataGridView dgv)
        {
            InitializeComponent();
            this.Load += Frm_AddMappingItem_Load;
            _dgv = dgv;
        }

        void Frm_AddMappingItem_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.EnvironmentItem = (int)this.EnvironmentNumericUpDown.Value;
            this.LedItem = (int)this.LedNumericUpDown.Value;
            if (!IsBrightness())
            {
                string errorTitleText = CommonUI.GetCustomMessage(_languageTable, "errorTitle", "错误");
                string errorText = CommonUI.GetCustomMessage(_languageTable, "error01", "存在相同的环境亮度配置，请重新修改");
                ShowCustomMessageBox(errorText, errorTitleText, MessageBoxButtons.OK, Windows.Forms.MessageBoxIconType.Error);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool IsBrightness()
        {
            foreach (DataGridViewRow row in _dgv.Rows)
            {
                if (row == null || row.Cells[0] == null || row.Cells[1].Value == null)
                    break;

                if (row.Cells[0].Value.ToString().Equals(this.EnvironmentItem.ToString()))
                    return false;
            }
            return true;
        }

        private void ApplyUILanguageTable()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.OpticalProbeConfigLangPath, this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.OpticalProbeConfigLangPath, "Frm_AddMappingItem", out _languageTable);
        }
    }
}
