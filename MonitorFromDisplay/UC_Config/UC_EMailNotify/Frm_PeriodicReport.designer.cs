namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    partial class Frm_PeriodicReport
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
            this.crystalButton_Ok = new Nova.Control.CrystalButton();
            this.crystalButton_Cancel = new Nova.Control.CrystalButton();
            this.checkBox_EveryDay = new System.Windows.Forms.CheckBox();
            this.checkBox_Weekly = new System.Windows.Forms.CheckBox();
            this.checkBox_Monthly = new System.Windows.Forms.CheckBox();
            this.label_Date = new System.Windows.Forms.Label();
            this.dateTimePicker_Day = new System.Windows.Forms.DateTimePicker();
            this.checkBox_Monday = new System.Windows.Forms.CheckBox();
            this.checkBox_Tuesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Wednesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Thursday = new System.Windows.Forms.CheckBox();
            this.checkBox_Friday = new System.Windows.Forms.CheckBox();
            this.checkBox_Saturday = new System.Windows.Forms.CheckBox();
            this.checkBox_Sunday = new System.Windows.Forms.CheckBox();
            this.label_hour = new System.Windows.Forms.Label();
            this.dateTimePicker_hour = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // crystalButton_Ok
            // 
            this.crystalButton_Ok.AutoEllipsis = true;
            this.crystalButton_Ok.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Ok.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Ok.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Ok.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Ok.ButtonCornorRadius = 5;
            this.crystalButton_Ok.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Ok.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Ok.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_Ok.IsButtonFoucs = false;
            this.crystalButton_Ok.Location = new System.Drawing.Point(209, 209);
            this.crystalButton_Ok.Name = "crystalButton_Ok";
            this.crystalButton_Ok.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_Ok.TabIndex = 12;
            this.crystalButton_Ok.Text = "确定";
            this.crystalButton_Ok.Transparency = 50;
            this.crystalButton_Ok.UseVisualStyleBackColor = false;
            this.crystalButton_Ok.Click += new System.EventHandler(this.crystalButton_Ok_Click);
            // 
            // crystalButton_Cancel
            // 
            this.crystalButton_Cancel.AutoEllipsis = true;
            this.crystalButton_Cancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.crystalButton_Cancel.BottonActivatedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Cancel.ButtonBusyBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonClickColor = System.Drawing.Color.Green;
            this.crystalButton_Cancel.ButtonCornorRadius = 5;
            this.crystalButton_Cancel.ButtonFreeBorderColor = System.Drawing.Color.DimGray;
            this.crystalButton_Cancel.ButtonSelectedColor = System.Drawing.Color.LimeGreen;
            this.crystalButton_Cancel.ForeColor = System.Drawing.Color.Black;
            this.crystalButton_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crystalButton_Cancel.IsButtonFoucs = false;
            this.crystalButton_Cancel.Location = new System.Drawing.Point(306, 209);
            this.crystalButton_Cancel.Name = "crystalButton_Cancel";
            this.crystalButton_Cancel.Size = new System.Drawing.Size(80, 30);
            this.crystalButton_Cancel.TabIndex = 13;
            this.crystalButton_Cancel.Text = "取消";
            this.crystalButton_Cancel.Transparency = 50;
            this.crystalButton_Cancel.UseVisualStyleBackColor = false;
            this.crystalButton_Cancel.Click += new System.EventHandler(this.crystalButton_Cancel_Click);
            // 
            // checkBox_EveryDay
            // 
            this.checkBox_EveryDay.Location = new System.Drawing.Point(10, 9);
            this.checkBox_EveryDay.Name = "checkBox_EveryDay";
            this.checkBox_EveryDay.Size = new System.Drawing.Size(109, 20);
            this.checkBox_EveryDay.TabIndex = 0;
            this.checkBox_EveryDay.Text = "每天";
            this.checkBox_EveryDay.UseVisualStyleBackColor = true;
            this.checkBox_EveryDay.CheckedChanged += new System.EventHandler(this.checkBox_EveryDay_CheckedChanged);
            // 
            // checkBox_Weekly
            // 
            this.checkBox_Weekly.Location = new System.Drawing.Point(131, 9);
            this.checkBox_Weekly.Name = "checkBox_Weekly";
            this.checkBox_Weekly.Size = new System.Drawing.Size(109, 20);
            this.checkBox_Weekly.TabIndex = 1;
            this.checkBox_Weekly.Text = "每周";
            this.checkBox_Weekly.UseVisualStyleBackColor = true;
            this.checkBox_Weekly.CheckedChanged += new System.EventHandler(this.checkBox_Weekly_CheckedChanged);
            // 
            // checkBox_Monthly
            // 
            this.checkBox_Monthly.Location = new System.Drawing.Point(252, 9);
            this.checkBox_Monthly.Name = "checkBox_Monthly";
            this.checkBox_Monthly.Size = new System.Drawing.Size(109, 20);
            this.checkBox_Monthly.TabIndex = 2;
            this.checkBox_Monthly.Text = "每月";
            this.checkBox_Monthly.UseVisualStyleBackColor = true;
            this.checkBox_Monthly.CheckedChanged += new System.EventHandler(this.checkBox_Monthly_CheckedChanged);
            // 
            // label_Date
            // 
            this.label_Date.Location = new System.Drawing.Point(10, 58);
            this.label_Date.Name = "label_Date";
            this.label_Date.Size = new System.Drawing.Size(117, 20);
            this.label_Date.TabIndex = 21;
            this.label_Date.Text = "发送日期：";
            // 
            // dateTimePicker_Day
            // 
            this.dateTimePicker_Day.CustomFormat = "dd";
            this.dateTimePicker_Day.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Day.Location = new System.Drawing.Point(133, 58);
            this.dateTimePicker_Day.Name = "dateTimePicker_Day";
            this.dateTimePicker_Day.ShowUpDown = true;
            this.dateTimePicker_Day.Size = new System.Drawing.Size(87, 21);
            this.dateTimePicker_Day.TabIndex = 3;
            // 
            // checkBox_Monday
            // 
            this.checkBox_Monday.Location = new System.Drawing.Point(10, 95);
            this.checkBox_Monday.Name = "checkBox_Monday";
            this.checkBox_Monday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Monday.TabIndex = 4;
            this.checkBox_Monday.Text = "星期一";
            this.checkBox_Monday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Tuesday
            // 
            this.checkBox_Tuesday.Location = new System.Drawing.Point(111, 95);
            this.checkBox_Tuesday.Name = "checkBox_Tuesday";
            this.checkBox_Tuesday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Tuesday.TabIndex = 5;
            this.checkBox_Tuesday.Text = "星期二";
            this.checkBox_Tuesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Wednesday
            // 
            this.checkBox_Wednesday.Location = new System.Drawing.Point(209, 95);
            this.checkBox_Wednesday.Name = "checkBox_Wednesday";
            this.checkBox_Wednesday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Wednesday.TabIndex = 6;
            this.checkBox_Wednesday.Text = "星期三";
            this.checkBox_Wednesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Thursday
            // 
            this.checkBox_Thursday.Location = new System.Drawing.Point(307, 95);
            this.checkBox_Thursday.Name = "checkBox_Thursday";
            this.checkBox_Thursday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Thursday.TabIndex = 7;
            this.checkBox_Thursday.Text = "星期四";
            this.checkBox_Thursday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Friday
            // 
            this.checkBox_Friday.Location = new System.Drawing.Point(10, 128);
            this.checkBox_Friday.Name = "checkBox_Friday";
            this.checkBox_Friday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Friday.TabIndex = 8;
            this.checkBox_Friday.Text = "星期五";
            this.checkBox_Friday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Saturday
            // 
            this.checkBox_Saturday.Location = new System.Drawing.Point(111, 128);
            this.checkBox_Saturday.Name = "checkBox_Saturday";
            this.checkBox_Saturday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Saturday.TabIndex = 9;
            this.checkBox_Saturday.Text = "星期六";
            this.checkBox_Saturday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Sunday
            // 
            this.checkBox_Sunday.Location = new System.Drawing.Point(209, 128);
            this.checkBox_Sunday.Name = "checkBox_Sunday";
            this.checkBox_Sunday.Size = new System.Drawing.Size(95, 20);
            this.checkBox_Sunday.TabIndex = 10;
            this.checkBox_Sunday.Text = "星期日";
            this.checkBox_Sunday.UseVisualStyleBackColor = true;
            // 
            // label_hour
            // 
            this.label_hour.Location = new System.Drawing.Point(10, 166);
            this.label_hour.Name = "label_hour";
            this.label_hour.Size = new System.Drawing.Size(117, 20);
            this.label_hour.TabIndex = 17;
            this.label_hour.Text = "发送时间:";
            // 
            // dateTimePicker_hour
            // 
            this.dateTimePicker_hour.CustomFormat = "HH:mm";
            this.dateTimePicker_hour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_hour.Location = new System.Drawing.Point(147, 166);
            this.dateTimePicker_hour.Name = "dateTimePicker_hour";
            this.dateTimePicker_hour.ShowUpDown = true;
            this.dateTimePicker_hour.Size = new System.Drawing.Size(95, 21);
            this.dateTimePicker_hour.TabIndex = 11;
            // 
            // Frm_PeriodicReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(408, 251);
            this.Controls.Add(this.dateTimePicker_hour);
            this.Controls.Add(this.checkBox_Sunday);
            this.Controls.Add(this.checkBox_Saturday);
            this.Controls.Add(this.checkBox_Friday);
            this.Controls.Add(this.checkBox_Thursday);
            this.Controls.Add(this.checkBox_Wednesday);
            this.Controls.Add(this.checkBox_Tuesday);
            this.Controls.Add(this.checkBox_Monday);
            this.Controls.Add(this.dateTimePicker_Day);
            this.Controls.Add(this.label_Date);
            this.Controls.Add(this.label_hour);
            this.Controls.Add(this.checkBox_Monthly);
            this.Controls.Add(this.checkBox_Weekly);
            this.Controls.Add(this.checkBox_EveryDay);
            this.Controls.Add(this.crystalButton_Cancel);
            this.Controls.Add(this.crystalButton_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_PeriodicReport";
            this.Tag = "hh";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Frm_PeriodicReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Nova.Control.CrystalButton crystalButton_Ok;
        private Nova.Control.CrystalButton crystalButton_Cancel;
        private System.Windows.Forms.CheckBox checkBox_EveryDay;
        private System.Windows.Forms.CheckBox checkBox_Weekly;
        private System.Windows.Forms.CheckBox checkBox_Monthly;
        private System.Windows.Forms.Label label_Date;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Day;
        private System.Windows.Forms.CheckBox checkBox_Monday;
        private System.Windows.Forms.CheckBox checkBox_Tuesday;
        private System.Windows.Forms.CheckBox checkBox_Wednesday;
        private System.Windows.Forms.CheckBox checkBox_Thursday;
        private System.Windows.Forms.CheckBox checkBox_Friday;
        private System.Windows.Forms.CheckBox checkBox_Saturday;
        private System.Windows.Forms.CheckBox checkBox_Sunday;
        private System.Windows.Forms.Label label_hour;
        private System.Windows.Forms.DateTimePicker dateTimePicker_hour;
    }
}