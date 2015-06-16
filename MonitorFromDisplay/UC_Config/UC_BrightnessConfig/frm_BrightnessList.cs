using Nova.LCT.GigabitSystem.Common;
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
    public partial class frm_BrightnessList : Frm_CommonBase
    {
        private UC_BrightnessConfig _uc_BrightnessConfig;
        public DisplaySmartBrightEasyConfigBase BrightnessConfig
        {
            get { return _uc_BrightnessConfig.BrightnessSoftwareConfig; }
        }
        public frm_BrightnessList(string sn, MonitorDataManager.BrightnessConfigType type, DisplaySmartBrightEasyConfigBase cfg)
        {
            InitializeComponent();
            _uc_BrightnessConfig = new UC_BrightnessConfig();
            this.Controls.Add(_uc_BrightnessConfig);
            _uc_BrightnessConfig.Dock = DockStyle.Fill;
            _uc_BrightnessConfig.SubmitEvent += uC_BrightnessConfig_SubmitEvent;
            _uc_BrightnessConfig.InitialControlConfig(sn, type, cfg);
        }

        void uC_BrightnessConfig_SubmitEvent(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
