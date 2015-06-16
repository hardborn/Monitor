namespace MonitorDataAccessorTest
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.txt_SqLiteFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonReadLog = new System.Windows.Forms.Button();
            this.txt_RowCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button_SetuserInfo = new System.Windows.Forms.Button();
            this.button_GetUserInfo = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SelectFile.Location = new System.Drawing.Point(1245, 8);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(41, 23);
            this.btn_SelectFile.TabIndex = 7;
            this.btn_SelectFile.Text = "保存";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // txt_SqLiteFile
            // 
            this.txt_SqLiteFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SqLiteFile.Location = new System.Drawing.Point(144, 10);
            this.txt_SqLiteFile.Name = "txt_SqLiteFile";
            this.txt_SqLiteFile.Size = new System.Drawing.Size(1095, 21);
            this.txt_SqLiteFile.TabIndex = 6;
            this.txt_SqLiteFile.Text = "C:\\Users\\Administrator\\Desktop\\123123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "数据库文件路径：";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(391, 46);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 35);
            this.buttonCreate.TabIndex = 9;
            this.buttonCreate.Text = "创建数据表";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(230, 46);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(87, 35);
            this.buttonConnect.TabIndex = 9;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1298, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1298, 251);
            this.dataGridView1.TabIndex = 11;
            // 
            // buttonReadLog
            // 
            this.buttonReadLog.Location = new System.Drawing.Point(236, 117);
            this.buttonReadLog.Name = "buttonReadLog";
            this.buttonReadLog.Size = new System.Drawing.Size(87, 35);
            this.buttonReadLog.TabIndex = 9;
            this.buttonReadLog.Text = "读取日志";
            this.buttonReadLog.UseVisualStyleBackColor = true;
            this.buttonReadLog.Click += new System.EventHandler(this.buttonReadLog_Click);
            // 
            // txt_RowCount
            // 
            this.txt_RowCount.Location = new System.Drawing.Point(618, 418);
            this.txt_RowCount.Name = "txt_RowCount";
            this.txt_RowCount.ReadOnly = true;
            this.txt_RowCount.Size = new System.Drawing.Size(96, 21);
            this.txt_RowCount.TabIndex = 13;
            this.txt_RowCount.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(535, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "查询结果个数";
            // 
            // buttonWrite
            // 
            this.buttonWrite.Location = new System.Drawing.Point(143, 117);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(87, 35);
            this.buttonWrite.TabIndex = 9;
            this.buttonWrite.Text = "填写日志";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBox.Location = new System.Drawing.Point(62, 126);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(75, 20);
            this.comboBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "条目";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(329, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 35);
            this.button1.TabIndex = 9;
            this.button1.Text = "更新策略";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(422, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "读取策略";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(515, 118);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 35);
            this.button3.TabIndex = 9;
            this.button3.Text = "更新硬件配置";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(608, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 35);
            this.button4.TabIndex = 9;
            this.button4.Text = "读取硬件配置";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(701, 118);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 35);
            this.button5.TabIndex = 9;
            this.button5.Text = "更新告警配置";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(794, 118);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 35);
            this.button6.TabIndex = 9;
            this.button6.Text = "读取告警配置";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(608, 46);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(87, 35);
            this.button7.TabIndex = 9;
            this.button7.Text = "更新外设配置";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(701, 46);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(87, 35);
            this.button8.TabIndex = 9;
            this.button8.Text = "读取外设配置";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(887, 117);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(87, 35);
            this.button9.TabIndex = 9;
            this.button9.Text = "更新软件配置";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(980, 117);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(87, 35);
            this.button10.TabIndex = 9;
            this.button10.Text = "读取软件配置";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(846, 46);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(108, 35);
            this.button11.TabIndex = 9;
            this.button11.Text = "更新光探头配置";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(971, 46);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(108, 35);
            this.button12.TabIndex = 9;
            this.button12.Text = "读取光探头配置";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button_SetuserInfo
            // 
            this.button_SetuserInfo.Location = new System.Drawing.Point(1073, 117);
            this.button_SetuserInfo.Name = "button_SetuserInfo";
            this.button_SetuserInfo.Size = new System.Drawing.Size(87, 35);
            this.button_SetuserInfo.TabIndex = 9;
            this.button_SetuserInfo.Text = "更新用户配置";
            this.button_SetuserInfo.UseVisualStyleBackColor = true;
            this.button_SetuserInfo.Click += new System.EventHandler(this.button_SetuserInfo_Click);
            // 
            // button_GetUserInfo
            // 
            this.button_GetUserInfo.Location = new System.Drawing.Point(1166, 117);
            this.button_GetUserInfo.Name = "button_GetUserInfo";
            this.button_GetUserInfo.Size = new System.Drawing.Size(87, 35);
            this.button_GetUserInfo.TabIndex = 9;
            this.button_GetUserInfo.Text = "读取用户配置";
            this.button_GetUserInfo.UseVisualStyleBackColor = true;
            this.button_GetUserInfo.Click += new System.EventHandler(this.button_GetUserInfo_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1298, 464);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.txt_RowCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_GetUserInfo);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_SetuserInfo);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.buttonReadLog);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_SelectFile);
            this.Controls.Add(this.txt_SqLiteFile);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonitorDataAccessorDEMO";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.TextBox txt_SqLiteFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonReadLog;
        private System.Windows.Forms.TextBox txt_RowCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button_SetuserInfo;
        private System.Windows.Forms.Button button_GetUserInfo;
    }
}

