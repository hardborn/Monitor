using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using System.Collections;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_SmokeControlConfig : UC_ConfigBase
    {
        public delegate void DataSaveEventHandler(Dictionary<string, PowerCtrl_Type> dicPowerStatusList);
        public event DataSaveEventHandler DataSaveEvent = null;
        private Hashtable _langTable;
        private List<System.Windows.Forms.Control> _ctrlList = new List<System.Windows.Forms.Control>();
        private Dictionary<string, PowerCtrl_Type> _powerStatusDic;
        public Dictionary<string, PowerCtrl_Type> PowerStatusDic
        {
            get { return _powerStatusDic; }
        }
        #region 公共方法
        public UC_SmokeControlConfig()
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
        }
        public void SetPowerList(Dictionary<string, PowerCtrl_Type> dicPowerStatus)
        {
            _powerStatusDic = dicPowerStatus;
            if (_ctrlList.Count != 0)
            {
                foreach (var item in _ctrlList)
                {
                    panel_PowerManager.Controls.Remove(item);
                }
                _ctrlList.Clear();
            }
            int index = 0;
            foreach (var item in _powerStatusDic.Keys)
            {
                if (_powerStatusDic[item] == PowerCtrl_Type.still)
                    AddItem(index++, MonitorAllConfig.Instance().PeripheralCfgDICDic[item], item, false, ref _ctrlList);
                else if (_powerStatusDic[item] == PowerCtrl_Type.close)
                    AddItem(index++, MonitorAllConfig.Instance().PeripheralCfgDICDic[item], item, true, ref _ctrlList);
            }
            if (index > 7)
                panel_PowerManager.Height += 30 * (index - 7);
        }
        #endregion
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_SmokeControlConfig", this);
        }
        void AddItem(int index, string display, string key, bool status, ref List<System.Windows.Forms.Control> ctrlList)
        {
            Size panelSize = new Size(panel_PowerManager.Width - 6, 30);
            Point panelLoc = new Point(3, 3 + panelSize.Height * index);

            Size labelSize = new Size(panelSize.Width - 120 - 98 - 20 - 3, 23);
            Point labelLoc = new Point(3, 2);

            Panel p = new Panel();
            Label l = new Label();
            RadioButton rb_off = new RadioButton();
            RadioButton rb_still = new RadioButton();
            if (status) rb_off.Checked = true;
            else rb_still.Checked = true;

            //add controls
            p.Controls.Add(l);
            p.Controls.Add(rb_off);
            p.Controls.Add(rb_still);
            panel_PowerManager.Controls.Add(p);
            //initial control
            p.Size = panelSize;
            p.Location = panelLoc;// new Point(panelLoc.X, panelLoc.Y);
            p.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            //initial control
            l.AutoSize = false;
            l.Text = display;
            l.TextAlign = ContentAlignment.MiddleRight;
            l.Anchor = System.Windows.Forms.AnchorStyles.Top;
            l.Location = labelLoc;
            l.Size = labelSize;

            //initial control
            rb_off.Text = "";
            rb_off.AutoSize = false;
            rb_off.Size = new System.Drawing.Size(14, 13);
            rb_off.Location = new Point(p.Width - 105 - 98, 7);
            rb_off.CheckedChanged += CheckedChanged;
            rb_off.Anchor = System.Windows.Forms.AnchorStyles.Top;
            rb_off.Name = "rb_off";
            rb_off.Tag = key;

            //initial control
            rb_still.Text = "";
            rb_still.AutoSize = false;
            rb_still.Size = new System.Drawing.Size(14, 13);
            rb_still.Location = new Point(p.Width - 105, 7);
            rb_still.CheckedChanged += CheckedChanged;
            rb_still.Anchor = System.Windows.Forms.AnchorStyles.Top;
            rb_still.Name = "rb_still";
            rb_still.Tag = key;

            rb_off.Checked = status;
            rb_still.Checked = !status;

            ctrlList.Add(rb_off);
            ctrlList.Add(rb_still);
            ctrlList.Add(l);
            ctrlList.Add(p);
        }
        void CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null) return;
            System.Windows.Forms.RadioButton c = (System.Windows.Forms.RadioButton)sender;
            if (c == null) return;
            PowerCtrl_Type status = PowerCtrl_Type.still;
            if (c.Name == "rb_off" && c.Checked) status = PowerCtrl_Type.close;
            else if (c.Name == "rb_still" && c.Checked) status = PowerCtrl_Type.still;
            if (!_powerStatusDic.Keys.Contains((string)c.Tag))
                _powerStatusDic.Add((string)c.Tag, status);
            else _powerStatusDic[(string)c.Tag] = status;
            if (DataSaveEvent != null) DataSaveEvent(_powerStatusDic);
        }
        #endregion
    }
}
