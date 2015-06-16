using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Monitoring.DAL;
using Nova.Monitoring.Utilities;

namespace MonitorDataAccessorTest
{
    public partial class frmMain : Form
    {
        MonitorDataAccessor _accessor;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                txt_SqLiteFile.Text = save.FileName;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //if (_accessor != null)
            //{
            //    _accessor.Dispose();
            //}
            //bool bSuccess;
            //_accessor = new MonitorDataAccessor(txt_SqLiteFile.Text.Trim(), out bSuccess);
            //if (!bSuccess) toolStripStatusLabel1.Text = "数据库连接失败";
            //else toolStripStatusLabel1.Text = "数据库连接成功";
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!_accessor.CreateTables()) toolStripStatusLabel1.Text = "数据表创建失败";
            else toolStripStatusLabel1.Text = "数据表创建成功";
        }

        private void buttonReadLog_Click(object sender, EventArgs e)
        {
            ReadLog();
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            WriteLog();
        }
        private void ReadLog()
        {
            List<OprLog> oprLog = _accessor.GetOprLog("", -1, -1, DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0)).Ticks, DateTime.UtcNow.AddDays(3).Ticks);
            DataTable dt = new DataTable();
            dt.Columns.Add("sn");
            dt.Columns.Add("type");
            dt.Columns.Add("source");
            dt.Columns.Add("updatetime");
            dt.Columns.Add("content");
            dt.Columns.Add("condition");
            foreach (var item in oprLog)
            {
                dt.Rows.Add(new object[] { item.Sn, item.Type, item.Source, item.Updatetime, item.Content, item.Condition });
            }
            SelectResultBind(dt);
        }
        private void WriteLog()
        {
            OprLog opLog = new OprLog();
            for (int i = 0; i < comboBox.SelectedIndex + 1; i++)
            {
                opLog.Sn = "sn_" + i;
                opLog.Type = i;
                opLog.Source = i + 2;
                opLog.Updatetime = DateTime.UtcNow.AddHours(i);
                opLog.Content = "content" + i;
                opLog.Condition = "condition" + i;
                if (!_accessor.InsertOprLog(opLog))
                {
                    toolStripStatusLabel1.Text = "填写日志失败";
                    return;
                }
            }
            toolStripStatusLabel1.Text = "填写日志成功";
        }
        private void SelectResultBind(DataTable dt)
        {
            dataGridView1.DataSource = dt;
            if (dt == null)
            {
                txt_RowCount.Text = "-1";
            }
            else
            {
                txt_RowCount.Text = dt.Rows.Count.ToString();
            }
        }
        private void UpdateStrategy()
        {
            if (!_accessor.UpdateStrategy("content112", SystemHelper.GetUtcTicksByDateTime(DateTime.Now)))
                toolStripStatusLabel1.Text = "更新策略失败";
            else toolStripStatusLabel1.Text = "更新策略成功";

        }
        private void ReadStrategy()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("content");
            //foreach (var item in dic.Keys)
            //{
            string content;
            DateTime time;
            _accessor.GetStrategy(out content, out time);
            dt.Rows.Add(new object[] { content, time });
            //}
            SelectResultBind(dt);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateStrategy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadStrategy();
        }
        private void UpdateHWCfg()
        {
            if (!_accessor.UpdateHardWareCfg("sn_001", "223333332", SystemHelper.GetUtcTicksByDateTime(DateTime.Now)))
                toolStripStatusLabel1.Text = "更新硬件配置失败";
            else
                toolStripStatusLabel1.Text = "更新硬件配置成功";
        }
        private void GetHWCfg()
        {
            List<ConfigInfo> dic = _accessor.GetHardWareCfg("");
            DataTable dt = new DataTable();
            dt.Columns.Add("sn");
            dt.Columns.Add("content");
            foreach (var item in dic)
            {
                dt.Rows.Add(new object[] { item.SN, item.Content });
            }
            SelectResultBind(dt);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UpdateHWCfg();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetHWCfg();
        }

        private void UpdateAlarmCfg()
        {
            if (!_accessor.UpdateAlarmCfg("sn_002", "2222", DateTime.UtcNow.Ticks))
                toolStripStatusLabel1.Text = "更新告警配置失败";
            else
                toolStripStatusLabel1.Text = "更新告警配置成功";
        }
        private void ReadAlarmCfg()
        {
            List<ConfigInfo> dic = _accessor.GetAlarmCfg("");
            DataTable dt = new DataTable();
            dt.Columns.Add("sn");
            dt.Columns.Add("content");
            foreach (var item in dic)
            {
                dt.Rows.Add(new object[] { item.SN, item.Content });
            }
            SelectResultBind(dt);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            UpdateAlarmCfg();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReadAlarmCfg();
        }

        private void UpdatePeripheralCfg()
        {
            if (!_accessor.UpdatePeripheralCfg("21111111", SystemHelper.GetUtcTicksByDateTime(DateTime.Now)))
                toolStripStatusLabel1.Text = "更新外设配置失败";
            else
                toolStripStatusLabel1.Text = "更新外设配置成功";
        }
        private void GetPeripheralCfg()
        {
            string dic;
            DateTime time;
            _accessor.GetPeripheralCfg(out dic, out time);
            DataTable dt = new DataTable();
            dt.Columns.Add("content");
            dt.Rows.Add(new object[] { dic });
            SelectResultBind(dt);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdatePeripheralCfg();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GetPeripheralCfg();
        }

        private void UpdateSWCfg()
        {
            if (!_accessor.UpdateSoftWareCfg("2", SystemHelper.GetUtcTicksByDateTime(DateTime.Now)))
                toolStripStatusLabel1.Text = "更新软件配置失败";
            else
                toolStripStatusLabel1.Text = "更新软件配置成功";
        }

        private void ReadSWCfg()
        {
            List<string> swCfgList;
            List<DateTime> timeList;
            _accessor.GetSoftWareCfg(out swCfgList, out timeList);
            DataTable dt = new DataTable();
            dt.Columns.Add("content");
            foreach (var item in swCfgList)
            {
                dt.Rows.Add(new object[] { item });
            }
            SelectResultBind(dt);
        }
        private void UpdateUserCfg()
        {
            if (!_accessor.UpdateUserCfg("userconfig", SystemHelper.GetUtcTicksByDateTime(DateTime.Now)))
                toolStripStatusLabel1.Text = "更新用户配置失败";
            else
                toolStripStatusLabel1.Text = "更新用户配置成功";
        }

        private void ReadUserCfg()
        {
            //_accessor.GetUserCfg();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("content");
            //dt.Rows.Add(new object[] { item });
            //SelectResultBind(dt);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            UpdateSWCfg();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReadSWCfg();
        }

        private void UpdateLightList()
        {
            List<string> lightNameList = new List<string>();
            lightNameList.Add("light12");
            lightNameList.Add("light22");
            lightNameList.Add("light32");
            lightNameList.Add("light42");
            //if (!_accessor.UpdateLightProbeCfg("sn-21", lightNameList))
            //    toolStripStatusLabel1.Text = "更新光探头配置失败";
            //else
            //    toolStripStatusLabel1.Text = "更新光探头配置成功";
        }
        private void GetLightList()
        {
            //Dictionary<string, List<string>> = _accessor.GetLightProbeCfg("");
            //DataTable dt = new DataTable();
            //dt.Columns.Add("sn");
            //dt.Columns.Add("content");
            //string lightList = string.Empty;
            //foreach (var item in dic.Keys)
            //{
            //    for (int i = 0; i < dic[item].Count; i++)
            //    {
            //        lightList += dic[item][i] + ",";
            //    }
            //    dt.Rows.Add(new object[] { item, lightList });
            //}
            //SelectResultBind(dt);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            UpdateLightList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            GetLightList();
        }

        private void button_SetuserInfo_Click(object sender, EventArgs e)
        {
            UpdateUserCfg();
        }

        private void button_GetUserInfo_Click(object sender, EventArgs e)
        {
            ReadUserCfg();
        }
    }
}
