using Nova.Monitoring.MonitorDataManager;
namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_MonitorDisplayMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            ResourceDispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MonitorDisplayMain));
            this.tableLayoutPanel_MonitorData = new System.Windows.Forms.TableLayoutPanel();
            this.panel_DataList = new System.Windows.Forms.Panel();
            this.uC_MonitorDataListDW = new Nova.Monitoring.UI.MonitorFromDisplay.UC_MonitorDataList();
            this.panel_DisplayValue = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.popButton_Monitor = new Nova.Control.Button.PopButton();
            this.popButton_Cabinet_Door = new Nova.Control.Button.PopButton();
            this.popButton_Sender = new Nova.Control.Button.PopButton();
            this.popButton_Cabinet = new Nova.Control.Button.PopButton();
            this.popButton_Scanner = new Nova.Control.Button.PopButton();
            this.popButton_Power = new Nova.Control.Button.PopButton();
            this.popButton_tem = new Nova.Control.Button.PopButton();
            this.popButton_Fan = new Nova.Control.Button.PopButton();
            this.popButton_Hum = new Nova.Control.Button.PopButton();
            this.popButton_Smoke = new Nova.Control.Button.PopButton();
            this.panel_MonitorShow = new System.Windows.Forms.Panel();
            this.tabControl_MonitorShow = new System.Windows.Forms.TabControl();
            this.crystalButton_Config = new Nova.Control.CrystalButton();
            this.crystalButton_Regist = new Nova.Control.CrystalButton();
            this.crystalButton_MonitorRefresh = new Nova.Control.CrystalButton();
            this.statusStrip_mainStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_AutoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.bindingSource_DataList = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip_Notice = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel_MonitorData.SuspendLayout();
            this.panel_DataList.SuspendLayout();
            this.panel_DisplayValue.SuspendLayout();
            this.panel.SuspendLayout();
            this.panel_MonitorShow.SuspendLayout();
            this.statusStrip_mainStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_DataList)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_MonitorData
            // 
            this.tableLayoutPanel_MonitorData.ColumnCount = 1;
            this.tableLayoutPanel_MonitorData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_MonitorData.Controls.Add(this.panel_DataList, 0, 1);
            this.tableLayoutPanel_MonitorData.Controls.Add(this.panel_DisplayValue, 0, 0);
            this.tableLayoutPanel_MonitorData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_MonitorData.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_MonitorData.Name = "tableLayoutPanel_MonitorData";
            this.tableLayoutPanel_MonitorData.RowCount = 2;
            this.tableLayoutPanel_MonitorData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_MonitorData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel_MonitorData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_MonitorData.Size = new System.Drawing.Size(775, 585);
            this.tableLayoutPanel_MonitorData.TabIndex = 0;
            // 
            // panel_DataList
            // 
            this.panel_DataList.Controls.Add(this.uC_MonitorDataListDW);
            this.panel_DataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DataList.Location = new System.Drawing.Point(3, 453);
            this.panel_DataList.Name = "panel_DataList";
            this.panel_DataList.Size = new System.Drawing.Size(769, 129);
            this.panel_DataList.TabIndex = 1;
            // 
            // uC_MonitorDataListDW
            // 
            this.uC_MonitorDataListDW.BackColor = System.Drawing.Color.AliceBlue;
            this.uC_MonitorDataListDW.DataSource = null;
            this.uC_MonitorDataListDW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_MonitorDataListDW.Location = new System.Drawing.Point(0, 0);
            this.uC_MonitorDataListDW.Name = "uC_MonitorDataListDW";
            this.uC_MonitorDataListDW.Size = new System.Drawing.Size(769, 129);
            this.uC_MonitorDataListDW.TabIndex = 0;
            this.uC_MonitorDataListDW.TabStop = false;
            // 
            // panel_DisplayValue
            // 
            this.panel_DisplayValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_DisplayValue.Controls.Add(this.panel);
            this.panel_DisplayValue.Controls.Add(this.panel_MonitorShow);
            this.panel_DisplayValue.Controls.Add(this.crystalButton_Config);
            this.panel_DisplayValue.Controls.Add(this.crystalButton_Regist);
            this.panel_DisplayValue.Controls.Add(this.crystalButton_MonitorRefresh);
            this.panel_DisplayValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DisplayValue.Location = new System.Drawing.Point(3, 3);
            this.panel_DisplayValue.Name = "panel_DisplayValue";
            this.panel_DisplayValue.Size = new System.Drawing.Size(769, 444);
            this.panel_DisplayValue.TabIndex = 3;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.popButton_Monitor);
            this.panel.Controls.Add(this.popButton_Cabinet_Door);
            this.panel.Controls.Add(this.popButton_Sender);
            this.panel.Controls.Add(this.popButton_Cabinet);
            this.panel.Controls.Add(this.popButton_Scanner);
            this.panel.Controls.Add(this.popButton_Power);
            this.panel.Controls.Add(this.popButton_tem);
            this.panel.Controls.Add(this.popButton_Fan);
            this.panel.Controls.Add(this.popButton_Hum);
            this.panel.Controls.Add(this.popButton_Smoke);
            this.panel.Enabled = false;
            this.panel.Location = new System.Drawing.Point(2, 4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(50, 435);
            this.panel.TabIndex = 0;
            // 
            // popButton_Monitor
            // 
            this.popButton_Monitor.AutoSize = true;
            this.popButton_Monitor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Monitor.BackgroundImage")));
            this.popButton_Monitor.BorderLineWidth = 4;
            this.popButton_Monitor.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Monitor.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Monitor.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Monitor.FlatAppearance.BorderSize = 0;
            this.popButton_Monitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Monitor.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.MonitorCard;
            this.popButton_Monitor.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Monitor.Location = new System.Drawing.Point(5, 132);
            this.popButton_Monitor.Name = "popButton_Monitor";
            this.popButton_Monitor.Size = new System.Drawing.Size(40, 40);
            this.popButton_Monitor.TabIndex = 4;
            this.popButton_Monitor.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Monitor.UseVisualStyleBackColor = true;
            this.popButton_Monitor.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Monitor.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Monitor.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Monitor.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Cabinet_Door
            // 
            this.popButton_Cabinet_Door.AutoSize = true;
            this.popButton_Cabinet_Door.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Cabinet_Door.BackgroundImage")));
            this.popButton_Cabinet_Door.BorderLineWidth = 4;
            this.popButton_Cabinet_Door.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Cabinet_Door.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Cabinet_Door.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Cabinet_Door.FlatAppearance.BorderSize = 0;
            this.popButton_Cabinet_Door.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Cabinet_Door.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Cabinet_Door;
            this.popButton_Cabinet_Door.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Cabinet_Door.Location = new System.Drawing.Point(5, 390);
            this.popButton_Cabinet_Door.Name = "popButton_Cabinet_Door";
            this.popButton_Cabinet_Door.Size = new System.Drawing.Size(40, 40);
            this.popButton_Cabinet_Door.TabIndex = 4;
            this.popButton_Cabinet_Door.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Cabinet_Door.UseVisualStyleBackColor = true;
            this.popButton_Cabinet_Door.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Cabinet_Door.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Cabinet_Door.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Cabinet_Door.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Sender
            // 
            this.popButton_Sender.AutoSize = true;
            this.popButton_Sender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Sender.BackgroundImage")));
            this.popButton_Sender.BorderLineWidth = 4;
            this.popButton_Sender.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Sender.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Sender.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Sender.FlatAppearance.BorderSize = 0;
            this.popButton_Sender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Sender.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.SenderStatus;
            this.popButton_Sender.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Sender.Location = new System.Drawing.Point(5, 3);
            this.popButton_Sender.Name = "popButton_Sender";
            this.popButton_Sender.Size = new System.Drawing.Size(40, 40);
            this.popButton_Sender.TabIndex = 4;
            this.popButton_Sender.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Sender.UseVisualStyleBackColor = true;
            this.popButton_Sender.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Sender.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Sender.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Sender.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Cabinet
            // 
            this.popButton_Cabinet.AutoSize = true;
            this.popButton_Cabinet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Cabinet.BackgroundImage")));
            this.popButton_Cabinet.BorderLineWidth = 4;
            this.popButton_Cabinet.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Cabinet.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Cabinet.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Cabinet.FlatAppearance.BorderSize = 0;
            this.popButton_Cabinet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Cabinet.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Cabinet;
            this.popButton_Cabinet.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Cabinet.Location = new System.Drawing.Point(5, 347);
            this.popButton_Cabinet.Name = "popButton_Cabinet";
            this.popButton_Cabinet.Size = new System.Drawing.Size(40, 40);
            this.popButton_Cabinet.TabIndex = 4;
            this.popButton_Cabinet.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Cabinet.UseVisualStyleBackColor = true;
            this.popButton_Cabinet.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Cabinet.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Cabinet.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Cabinet.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Scanner
            // 
            this.popButton_Scanner.AutoSize = true;
            this.popButton_Scanner.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Scanner.BackgroundImage")));
            this.popButton_Scanner.BorderLineWidth = 4;
            this.popButton_Scanner.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Groove;
            this.popButton_Scanner.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Scanner.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Scanner.FlatAppearance.BorderSize = 0;
            this.popButton_Scanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Scanner.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.SBStatus;
            this.popButton_Scanner.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Scanner.Location = new System.Drawing.Point(5, 46);
            this.popButton_Scanner.Name = "popButton_Scanner";
            this.popButton_Scanner.Size = new System.Drawing.Size(40, 40);
            this.popButton_Scanner.TabIndex = 4;
            this.popButton_Scanner.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Scanner.UseVisualStyleBackColor = true;
            this.popButton_Scanner.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Scanner.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Scanner.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Scanner.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Power
            // 
            this.popButton_Power.AutoSize = true;
            this.popButton_Power.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Power.BackgroundImage")));
            this.popButton_Power.BorderLineWidth = 4;
            this.popButton_Power.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Power.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Power.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Power.FlatAppearance.BorderSize = 0;
            this.popButton_Power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Power.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Power;
            this.popButton_Power.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Power.Location = new System.Drawing.Point(5, 304);
            this.popButton_Power.Name = "popButton_Power";
            this.popButton_Power.Size = new System.Drawing.Size(40, 40);
            this.popButton_Power.TabIndex = 4;
            this.popButton_Power.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Power.UseVisualStyleBackColor = true;
            this.popButton_Power.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Power.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Power.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Power.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_tem
            // 
            this.popButton_tem.AutoSize = true;
            this.popButton_tem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_tem.BackgroundImage")));
            this.popButton_tem.BorderLineWidth = 4;
            this.popButton_tem.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_tem.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_tem.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_tem.FlatAppearance.BorderSize = 0;
            this.popButton_tem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_tem.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Temperature;
            this.popButton_tem.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_tem.Location = new System.Drawing.Point(5, 89);
            this.popButton_tem.Name = "popButton_tem";
            this.popButton_tem.Size = new System.Drawing.Size(40, 40);
            this.popButton_tem.TabIndex = 4;
            this.popButton_tem.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_tem.UseVisualStyleBackColor = true;
            this.popButton_tem.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_tem.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_tem.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_tem.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Fan
            // 
            this.popButton_Fan.AutoSize = true;
            this.popButton_Fan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Fan.BackgroundImage")));
            this.popButton_Fan.BorderLineWidth = 4;
            this.popButton_Fan.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Fan.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Fan.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Fan.FlatAppearance.BorderSize = 0;
            this.popButton_Fan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Fan.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Fan;
            this.popButton_Fan.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Fan.Location = new System.Drawing.Point(5, 261);
            this.popButton_Fan.Name = "popButton_Fan";
            this.popButton_Fan.Size = new System.Drawing.Size(40, 40);
            this.popButton_Fan.TabIndex = 4;
            this.popButton_Fan.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Fan.UseVisualStyleBackColor = true;
            this.popButton_Fan.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Fan.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Fan.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Fan.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Hum
            // 
            this.popButton_Hum.AutoSize = true;
            this.popButton_Hum.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Hum.BackgroundImage")));
            this.popButton_Hum.BorderLineWidth = 4;
            this.popButton_Hum.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Hum.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Hum.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Hum.FlatAppearance.BorderSize = 0;
            this.popButton_Hum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Hum.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Humidity;
            this.popButton_Hum.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Hum.Location = new System.Drawing.Point(5, 175);
            this.popButton_Hum.Name = "popButton_Hum";
            this.popButton_Hum.Size = new System.Drawing.Size(40, 40);
            this.popButton_Hum.TabIndex = 4;
            this.popButton_Hum.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Hum.UseVisualStyleBackColor = true;
            this.popButton_Hum.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Hum.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Hum.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Hum.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // popButton_Smoke
            // 
            this.popButton_Smoke.AutoSize = true;
            this.popButton_Smoke.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("popButton_Smoke.BackgroundImage")));
            this.popButton_Smoke.BorderLineWidth = 4;
            this.popButton_Smoke.ButtonBorderStyle = Nova.Control.Button.ButtonBorderStyleType.Ridge;
            this.popButton_Smoke.DarkShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.popButton_Smoke.DownStyleBackColor = System.Drawing.Color.Yellow;
            this.popButton_Smoke.FlatAppearance.BorderSize = 0;
            this.popButton_Smoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popButton_Smoke.Image = global::Nova.Monitoring.UI.MonitorFromDisplay.Properties.Resources.Smoke;
            this.popButton_Smoke.LightShadowColor = System.Drawing.SystemColors.ControlLightLight;
            this.popButton_Smoke.Location = new System.Drawing.Point(5, 218);
            this.popButton_Smoke.Name = "popButton_Smoke";
            this.popButton_Smoke.Size = new System.Drawing.Size(40, 40);
            this.popButton_Smoke.TabIndex = 4;
            this.popButton_Smoke.UpStyleBackColor = System.Drawing.SystemColors.ButtonFace;
            this.popButton_Smoke.UseVisualStyleBackColor = true;
            this.popButton_Smoke.Click += new System.EventHandler(this.popButton_Sender_Click);
            this.popButton_Smoke.MouseEnter += new System.EventHandler(this.popButton_Sender_MouseEnter);
            this.popButton_Smoke.MouseLeave += new System.EventHandler(this.popButton_Fan_MouseLeave);
            this.popButton_Smoke.MouseHover += new System.EventHandler(this.popButton_Fan_MouseHover);
            // 
            // panel_MonitorShow
            // 
            this.panel_MonitorShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_MonitorShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MonitorShow.Controls.Add(this.tabControl_MonitorShow);
            this.panel_MonitorShow.Location = new System.Drawing.Point(53, 3);
            this.panel_MonitorShow.Name = "panel_MonitorShow";
            this.panel_MonitorShow.Size = new System.Drawing.Size(612, 436);
            this.panel_MonitorShow.TabIndex = 2;
            // 
            // tabControl_MonitorShow
            // 
            this.tabControl_MonitorShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_MonitorShow.Location = new System.Drawing.Point(0, 0);
            this.tabControl_MonitorShow.Name = "tabControl_MonitorShow";
            this.tabControl_MonitorShow.SelectedIndex = 0;
            this.tabControl_MonitorShow.Size = new System.Drawing.Size(610, 434);
            this.tabControl_MonitorShow.TabIndex = 3;
            this.tabControl_MonitorShow.SelectedIndexChanged += new System.EventHandler(this.tabControl_MonitorShow_SelectedIndexChanged);
            // 
            // crystalButton_Config
            // 
            this.crystalButton_Config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_Config.AutoEllipsis = true;
            this.crystalButton_Config.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Config.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Config.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Config.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Config.ButtonCornorRadius = 3;
            this.crystalButton_Config.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Config.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Config.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Config.IsButtonFoucs = false;
            this.crystalButton_Config.Location = new System.Drawing.Point(671, 403);
            this.crystalButton_Config.Name = "crystalButton_Config";
            this.crystalButton_Config.Size = new System.Drawing.Size(90, 33);
            this.crystalButton_Config.TabIndex = 1;
            this.crystalButton_Config.Text = "配置";
            this.crystalButton_Config.Transparency = 50;
            this.crystalButton_Config.UseVisualStyleBackColor = true;
            this.crystalButton_Config.Click += new System.EventHandler(this.crystalButton_Config_Click);
            // 
            // crystalButton_Regist
            // 
            this.crystalButton_Regist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_Regist.AutoEllipsis = true;
            this.crystalButton_Regist.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Regist.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Regist.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Regist.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Regist.ButtonCornorRadius = 3;
            this.crystalButton_Regist.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Regist.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Regist.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Regist.IsButtonFoucs = false;
            this.crystalButton_Regist.Location = new System.Drawing.Point(670, 206);
            this.crystalButton_Regist.Name = "crystalButton_Regist";
            this.crystalButton_Regist.Size = new System.Drawing.Size(90, 33);
            this.crystalButton_Regist.TabIndex = 1;
            this.crystalButton_Regist.Text = "注册";
            this.crystalButton_Regist.Transparency = 50;
            this.crystalButton_Regist.UseVisualStyleBackColor = true;
            this.crystalButton_Regist.Visible = false;
            this.crystalButton_Regist.Click += new System.EventHandler(this.crystalButton_Regist_Click);
            // 
            // crystalButton_MonitorRefresh
            // 
            this.crystalButton_MonitorRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalButton_MonitorRefresh.AutoEllipsis = true;
            this.crystalButton_MonitorRefresh.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_MonitorRefresh.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_MonitorRefresh.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_MonitorRefresh.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_MonitorRefresh.ButtonCornorRadius = 3;
            this.crystalButton_MonitorRefresh.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_MonitorRefresh.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_MonitorRefresh.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_MonitorRefresh.IsButtonFoucs = false;
            this.crystalButton_MonitorRefresh.Location = new System.Drawing.Point(671, 362);
            this.crystalButton_MonitorRefresh.Name = "crystalButton_MonitorRefresh";
            this.crystalButton_MonitorRefresh.Size = new System.Drawing.Size(90, 33);
            this.crystalButton_MonitorRefresh.TabIndex = 1;
            this.crystalButton_MonitorRefresh.Text = "监控刷新";
            this.crystalButton_MonitorRefresh.Transparency = 50;
            this.crystalButton_MonitorRefresh.UseVisualStyleBackColor = true;
            this.crystalButton_MonitorRefresh.Click += new System.EventHandler(this.crystalButton_MonitorRefresh_Click);
            // 
            // statusStrip_mainStatus
            // 
            this.statusStrip_mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_AutoStatus});
            this.statusStrip_mainStatus.Location = new System.Drawing.Point(0, 585);
            this.statusStrip_mainStatus.Name = "statusStrip_mainStatus";
            this.statusStrip_mainStatus.Size = new System.Drawing.Size(775, 22);
            this.statusStrip_mainStatus.TabIndex = 1;
            this.statusStrip_mainStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            this.toolStripStatusLabel_Status.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabel_Status.Text = "监控状态正常:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // toolStripStatusLabel_AutoStatus
            // 
            this.toolStripStatusLabel_AutoStatus.Name = "toolStripStatusLabel_AutoStatus";
            this.toolStripStatusLabel_AutoStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // bindingSource_DataList
            // 
            this.bindingSource_DataList.DataMember = "MonitorDataFlags";
            this.bindingSource_DataList.DataSource = typeof(Nova.Monitoring.MonitorDataManager.Frm_MonitorDisplayMain_VM);
            // 
            // toolTip_Notice
            // 
            this.toolTip_Notice.AutoPopDelay = 5000;
            this.toolTip_Notice.InitialDelay = 50;
            this.toolTip_Notice.ReshowDelay = 10;
            // 
            // Frm_MonitorDisplayMain
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(775, 607);
            this.Controls.Add(this.tableLayoutPanel_MonitorData);
            this.Controls.Add(this.statusStrip_mainStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_MonitorDisplayMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控终端-数据展示";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MonitorDisplayMain_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MonitorDisplayMain_Load);
            this.tableLayoutPanel_MonitorData.ResumeLayout(false);
            this.panel_DataList.ResumeLayout(false);
            this.panel_DisplayValue.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.panel_MonitorShow.ResumeLayout(false);
            this.statusStrip_mainStatus.ResumeLayout(false);
            this.statusStrip_mainStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_DataList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_MonitorData;
        private System.Windows.Forms.Panel panel_DataList;
        private System.Windows.Forms.BindingSource bindingSource_DataList;
        private System.Windows.Forms.StatusStrip statusStrip_mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_AutoStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel_DisplayValue;
        private Nova.Control.CrystalButton crystalButton_Config;
        private Nova.Control.CrystalButton crystalButton_MonitorRefresh;
        private Nova.Control.CrystalButton crystalButton_Regist;
        private UC_MonitorDataList uC_MonitorDataListDW;
        private System.Windows.Forms.Panel panel_MonitorShow;
        private Nova.Control.Button.PopButton popButton_Cabinet_Door;
        private Nova.Control.Button.PopButton popButton_Cabinet;
        private Nova.Control.Button.PopButton popButton_Power;
        private Nova.Control.Button.PopButton popButton_Fan;
        private Nova.Control.Button.PopButton popButton_Smoke;
        private Nova.Control.Button.PopButton popButton_Hum;
        private Nova.Control.Button.PopButton popButton_Monitor;
        private Nova.Control.Button.PopButton popButton_tem;
        private Nova.Control.Button.PopButton popButton_Scanner;
        private Nova.Control.Button.PopButton popButton_Sender;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolTip toolTip_Notice;
        private System.Windows.Forms.TabControl tabControl_MonitorShow;
    }
}