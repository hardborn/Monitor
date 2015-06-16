using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using PresentationControls;
using Nova.Monitoring.Common;
using System.Collections;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_WHControlConfig_Smoke : UC_ConfigBase
    {
        private Hashtable _langTable;
        private UC_WHControlConfig_Smoke_VM _smokeVM;
        public UC_WHControlConfig_Smoke_VM SmokeVM
        {
            get { return _smokeVM; }
        }
        private UC_SmokeControlConfig _uc_smokeCtrlCfg;
        #region 公共方法
        public UC_WHControlConfig_Smoke(UC_WHControlConfig_Smoke_VM stratInfo)
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
            _smokeVM = stratInfo;
            numericUpDown_smokeCount.Value = SmokeVM.GreaterThan;
            InitialSmokeVM();
            panel_PowerList.Controls.Add(_uc_smokeCtrlCfg);
            _uc_smokeCtrlCfg.Dock = DockStyle.Fill;
        }
        public bool IsOk()
        {
            if (_smokeVM.PowerCtrlDic == null || _smokeVM.PowerCtrlDic.Count == 0)
                return false;
            bool isValid = false;
            foreach (var item in _smokeVM.PowerCtrlDic.Values)
            {
                if (item != PowerCtrl_Type.still)
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid) return false;
            return true;
        }
        #endregion
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_WHControlConfig_Smoke", this);
        }
        private void InitialSmokeVM()
        {
            _uc_smokeCtrlCfg = new UC_SmokeControlConfig();
            _uc_smokeCtrlCfg.DataSaveEvent += UC_smokeCtrlCfg_DataSaveEvent;
            _uc_smokeCtrlCfg.SetPowerList(_smokeVM.PowerCtrlDic);
        }
        void UC_smokeCtrlCfg_DataSaveEvent(Dictionary<string, PowerCtrl_Type> dicPowerStatusList)
        {
            _smokeVM.PowerCtrlDic = dicPowerStatusList;
        }
        #endregion

        private void numericUpDown_smokeCount_ValueChanged(object sender, EventArgs e)
        {
            _smokeVM.GreaterThan = (int)numericUpDown_smokeCount.Value;
        }
    }
}
