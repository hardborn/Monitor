using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.Resource.Language;

namespace Nova.Monitoring.UI.ScannerStatusDisplay
{
    public partial class Frm_ComplexScreenDisplay : Form
    {
        private UC_ComplexScreenLayout _complexScreenInfo = null;
        private ClickLinkLabelType _curHightLightType = ClickLinkLabelType.None;

        public Font CtrlOfFormFont
        {
            get { return this.Font; }
            set { MarsScreenMonitorDataViewer.AdjustFontObj.Attach(this); }
        }
        public Font MonitorInfoListFont
        {
            get
            {
                if (_complexScreenInfo != null)
                {
                    return _complexScreenInfo.MonitorInfoListFont;
                }
                return this.Font;
            }
            set
            {
                if (_complexScreenInfo != null)
                {
                    _complexScreenInfo.MonitorInfoListFont = value;
                }
            }
        }
        public Font CustomToolTipFont
        {
            get
            {
                if (_complexScreenInfo != null)
                {
                    return _complexScreenInfo.CustomToolTipFont;
                }
                return this.Font;
            }
            set
            {
                if (_complexScreenInfo != null)
                {
                    _complexScreenInfo.CustomToolTipFont = value;
                }
            }
        }
        public CtrlSytemMode CurCtrlSystemMode
        {
            get
            {
                if (_complexScreenInfo != null)
                {
                    return _complexScreenInfo.CurCtrlSystemMode;
                }
                return CtrlSytemMode.HasSenderMode;
            }
            set
            {
                if (_complexScreenInfo != null)
                {
                    _complexScreenInfo.CurCtrlSystemMode = value;
                }
            }
        }

        public Frm_ComplexScreenDisplay(UC_ComplexScreenLayout complexScreenInfo, 
                                        ClickLinkLabelType hightLightType)
        {
            InitializeComponent();

            _complexScreenInfo = complexScreenInfo;
            _curHightLightType = hightLightType;

            _complexScreenInfo.Parent = this.dbfPanel_ComplexScreenDisplay;
            _complexScreenInfo.Dock = DockStyle.Fill;
        }

        public void UpdateLanguage(string frmText)
        {
            this.Text = frmText;
        }
        private void Frm_ComplexScreenDisplay_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (_complexScreenInfo != null)
                {
                    _complexScreenInfo.InvalidateAllScanBoardInfo();
                    _complexScreenInfo.ShowMaxMinRow(_curHightLightType);
                }
            }
        }

        private void Frm_ComplexScreenDisplay_Load(object sender, EventArgs e)
        {
            //_complexScreenInfo.Parent = this.dbfPanel_ComplexScreenDisplay;
            //_complexScreenInfo.Dock = DockStyle.Fill;
        }
    }
}