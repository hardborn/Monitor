using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.MonitorDataManager;
using System.Collections;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public partial class UC_PowerManager : UC_ConfigBase
    {
        public delegate void DataSaveEventHandler(Dictionary<string, PowerCtrl_Type> dicPowerStatusList);
        public event DataSaveEventHandler DataSaveEvent = null;
        private Hashtable _langTable;
        private List<System.Windows.Forms.Control> _ctrlList;
        private Dictionary<string, PowerCtrl_Type> _dicPowerStatusList = new Dictionary<string, PowerCtrl_Type>();
        public Dictionary<string, PowerCtrl_Type> DicPowerStatusList
        {
            get { return _dicPowerStatusList; }
        }
        #region 公共方法
        public UC_PowerManager()
        {
            InitializeComponent();
            UpdateLang(CommonUI.ControlConfigLangPath);
            _ctrlList = new List<System.Windows.Forms.Control>();
        }
        public void SetPowerList(Dictionary<string, PowerCtrl_Type> dicPowerStatusList)
        {
            _dicPowerStatusList = dicPowerStatusList;
            if (_ctrlList.Count != 0)
            {
                foreach (var item in _ctrlList)
                {
                    panel_PowerManager.Controls.Remove(item);
                }
                _ctrlList.Clear();
            }
            int index = 0;
            foreach (var item in _dicPowerStatusList.Keys)
            {
                AddItem(index++, MonitorAllConfig.Instance().PeripheralCfgDICDic[item], item, _dicPowerStatusList[item], ref _ctrlList);
            }
            if (index > 6)
                panel_PowerManager.Height += 30 * (index - 6);
        }
        #endregion
        #region 私有函数
        /// <summary>
        /// 更新语言资源
        /// </summary>
        /// <param name="langResFile">语言资源路径</param>
        private void UpdateLang(string langResFile)
        {
            MultiLanguageUtils.UpdateLanguage(langResFile, "UC_PowerManager", this);
        }
        void AddItem(int index, string display, string name, PowerCtrl_Type status, ref List<System.Windows.Forms.Control> ctrlList)
        {
            Size panelSize = new Size(panel_PowerManager.Width - 6, 30);
            Point panelLoc = new Point(3, 3 + panelSize.Height * index);

            Size labelSize = new Size(panelSize.Width - 70 - 93 - 93 - 24 - 3, 23);
            Point labelLoc = new Point(3, 2);

            Panel p = new Panel();
            Label l = new Label();
            RadioButton rb_on = new RadioButton();
            RadioButton rb_off = new RadioButton();
            RadioButton rb_still = new RadioButton();
            switch (status)
            {
                case PowerCtrl_Type.open:
                    rb_on.Checked = true;
                    break;
                case PowerCtrl_Type.close:
                    rb_off.Checked = true;
                    break;
                case PowerCtrl_Type.still:
                    rb_still.Checked = true;
                    break;
                default:
                    break;
            }
            //add controls
            p.Controls.Add(l);
            p.Controls.Add(rb_on);
            p.Controls.Add(rb_off);
            p.Controls.Add(rb_still);
            panel_PowerManager.Controls.Add(p);

            //initial control
            p.Size = panelSize;
            p.Location = panelLoc;// Point(panelLoc.X, panelLoc.Y);
            p.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            //initial control
            l.AutoSize = false;
            l.Location = labelLoc;
            l.Size = labelSize;
            l.Text = display;
            l.TextAlign = ContentAlignment.MiddleRight;
            l.Anchor = System.Windows.Forms.AnchorStyles.Top;

            //initial control
            rb_on.Text = "";
            rb_on.Name = "rb_on";
            rb_on.Tag = name;
            rb_on.AutoSize = false;
            rb_on.Size = new System.Drawing.Size(14, 13);
            rb_on.Location = new Point(p.Width - 58 - 90 - 93, 7);
            rb_on.CheckedChanged += CheckedChanged;
            rb_on.Anchor = System.Windows.Forms.AnchorStyles.Top;

            //initial control
            rb_off.Text = "";
            rb_off.Name = "rb_off";
            rb_off.Tag = name;
            rb_off.AutoSize = false;
            rb_off.Size = new System.Drawing.Size(14, 13);
            rb_off.Location = new Point(p.Width - 58 - 90, 7);
            rb_off.CheckedChanged += CheckedChanged;
            rb_off.Anchor = System.Windows.Forms.AnchorStyles.Top;

            //initial control
            rb_still.Name = "rb_still";
            rb_still.Text = "";
            rb_still.Tag = name;
            rb_still.AutoSize = false;
            rb_still.Size = new System.Drawing.Size(14, 13);
            rb_still.Location = new Point(p.Width - 58, 7);
            rb_still.CheckedChanged += CheckedChanged;
            rb_still.Anchor = System.Windows.Forms.AnchorStyles.Top;

            ctrlList.Add(rb_on);
            ctrlList.Add(rb_off);
            ctrlList.Add(rb_still);
            ctrlList.Add(l);
            ctrlList.Add(p);
        }
        void CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null) return;
            System.Windows.Forms.Control c = (System.Windows.Forms.Control)sender;
            if (c == null) return;
            PowerCtrl_Type status = PowerCtrl_Type.still;
            if (c.Name == "rb_off") status = PowerCtrl_Type.close;
            else if (c.Name == "rb_on") status = PowerCtrl_Type.open;
            if (!_dicPowerStatusList.Keys.Contains((string)c.Tag))
                _dicPowerStatusList.Add((string)c.Tag, status);
            else _dicPowerStatusList[(string)c.Tag] = status;
            if (DataSaveEvent != null) DataSaveEvent(_dicPowerStatusList);
        }
        #endregion
    }
}
