using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.Common;
using Nova.Resource.Language;
using System.Collections;

namespace Nova.Monitoring.UI.MonitorFromDisplay.UC_Config
{
    public partial class UC_ScreenSummary : UserControl
    {
        private Hashtable _languageTable;
        public string Id { get; set; }
        public string Name { get; set; }

        public UC_ScreenSummary()
        {
            InitializeComponent();
            this.Load += UC_ScreenSummary_Load;
        }

        void UC_ScreenSummary_Load(object sender, EventArgs e)
        {
            ApplyUILanguageTable();
        }

        public UC_ScreenSummary(LedRegistationInfo screenInfo)
            : this()
        {
            this.screenNameTextBox.Text = string.IsNullOrEmpty(screenInfo.led_name) ? screenInfo.sn_num : screenInfo.led_name;
            this.Id = screenInfo.sn_num;
            this.Name = screenInfo.led_name;
        }

        private void screenNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            this.Name = textBox.Text.Trim();
        }

        private void ApplyUILanguageTable()
        {
            MultiLanguageUtils.UpdateLanguage(CommonUI.ScreenRegistrationLangPath, "UC_ScreenSummary", this);
            MultiLanguageUtils.ReadLanguageResource(CommonUI.ScreenRegistrationLangPath, "UC_ScreenSummary", out _languageTable);

            if (_languageTable == null || _languageTable.Count == 0)
                return;

        }
    }
}
