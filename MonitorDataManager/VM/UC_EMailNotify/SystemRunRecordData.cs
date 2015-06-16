using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace Nova.Monitoring.MonitorDataManager
{
    public class SystemRunRecordData
    {
        public string ScreenName
        {
            get { return _screenName; }
            set { _screenName = value; }
        }
        private string _screenName = "";

        public uint SystemRefeshIndex
        {
            get { return _systemRefeshIndex; }
            set { _systemRefeshIndex = value; }
        }
        private uint _systemRefeshIndex = 0;

        public uint SenderErrCount
        {
            get { return _senderErrCount; }
            set { _senderErrCount = value; }
        }
        private uint _senderErrCount = 0;

        public uint ReceiveCardErroeIndex
        {
            get { return _receiveCardErrorIndex; }
            set { _receiveCardErrorIndex = value; }
        }
        private uint _receiveCardErrorIndex = 0;

        public uint MCStatusErrCount
        {
            get { return _mCStatusErrCount; }
            set { _mCStatusErrCount = value; }
        }
        private uint _mCStatusErrCount = 0;

        public uint TemperatureMonitorErrorIndex
        {
            get { return _temperatureMonitorErrorIndex; }
            set { _temperatureMonitorErrorIndex = value; }
        }
        private uint _temperatureMonitorErrorIndex = 0;

        public uint HumidityAlarmCount
        {
            get { return _humidityAlarmCount; }
            set { _humidityAlarmCount = value; }
        }
        private uint _humidityAlarmCount = 0;

        public uint SmokeMonitorErrorIndex
        {
            get { return _smokeMonitorErrorIndex; }
            set { _smokeMonitorErrorIndex = value; }
        }
        private uint _smokeMonitorErrorIndex = 0;

        public uint FanAlarmSwitchCount
        {
            get { return _fanAlarmSwitchCount; }
            set { _fanAlarmSwitchCount = value; }
        }
        private uint _fanAlarmSwitchCount = 0;

        public uint PowerAlarmSwitchCount
        {
            get { return _powerAlarmSwitchCount; }
            set { _powerAlarmSwitchCount = value; }
        }
        private uint _powerAlarmSwitchCount = 0;

        public uint CabinetMonitorIndex
        {
            get { return _cabinetMonitorIndex; }
            set { _cabinetMonitorIndex = value; }
        }
        private uint _cabinetMonitorIndex = 0;

        public uint CabinetDoorMonitorIndex
        {
            get { return _cabinetDoorMonitorIndex; }
            set { _cabinetDoorMonitorIndex = value; }
        }
        private uint _cabinetDoorMonitorIndex = 0;

        public uint RedundantStateCount
        {
            get { return _redundantStateCount; }
            set { _redundantStateCount = value; }
        }
        private uint _redundantStateCount = 0;
    }

    public class SystemRunStatuslFileAccessor
    {
        /// <summary>
        /// Xml文件读取
        /// </summary>
        public static bool ReadConfigurationFile(string readPath, out SystemRunRecordData systemRunRecordData)
        {
            FileStream fileStream = null;
            XmlSerializer xmlSerializer = null;
            systemRunRecordData = null;
            try
            {
                fileStream = new FileStream(readPath, FileMode.Open);
                xmlSerializer = new XmlSerializer(typeof(SystemRunRecordData));
                systemRunRecordData = (SystemRunRecordData)xmlSerializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch：读取Xml文件出现异常" + ex.ToString(), true);
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

            }
            return true;
        }
        /// <summary>
        /// xml文件保存
        /// </summary>
        public static bool SaveAsConfigurationFile(SystemRunRecordData systemRunRecordData, string savePath)
        {
            XmlSerializer xmlSerializer = null;
            StreamWriter streamWriter = null;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(SystemRunRecordData));
                streamWriter = new StreamWriter(savePath);
                XmlWriter xmlWriter = XmlWriter.Create(streamWriter);
                xmlSerializer.Serialize(streamWriter, systemRunRecordData);
                streamWriter.Close();
            }
            catch (System.Exception ex)
            {
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch：写入Xml文件出现异常" + ex.ToString(), true);
                return false;
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
            return true;
        }
        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="dateTable"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CreateStatisticsExcel(DataTable dateTable, string fileName)
        {
            System.Threading.Thread thisThread = System.Threading.Thread.CurrentThread;
            System.Globalization.CultureInfo originalCulture = thisThread.CurrentCulture;
            object missing = System.Reflection.Missing.Value;
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            try
            {
                thisThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                app = new Excel.ApplicationClass();

                app.DisplayAlerts = false;   //设置不显示弹出框
                app.AlertBeforeOverwriting = false;  //设置默认为覆盖原有文件
                app.Application.Workbooks.Add(true);
                book = (Excel.Workbook)app.ActiveWorkbook;

                sheet = (Excel.Worksheet)book.ActiveSheet;

                for (int i = 0; i < dateTable.Columns.Count; i++)
                {
                    sheet.Cells[1, i + 1] = dateTable.Columns[i].ToString();
                }
                for (int i = 0; i < dateTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dateTable.Columns.Count; j++)
                    {
                        sheet.Cells[i + 2, j + 1] = dateTable.Rows[i][j];
                    }
                }

                book.SaveCopyAs(fileName);//保存文件

                //book.SaveAs(fileName, Excel.XlFileFormat.xlExcel8, Missing.Value, Missing.Value, Missing.Value,
                //                    Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value,
                //                    Missing.Value, Missing.Value);//保存文件

            }
            catch (System.Exception ex)
            {
                MonitorAllConfig.Instance().WriteLogToFile("ExistCatch：写入Excel报告文件出现异常" + ex.ToString(), true);
                return false;
            }
            finally
            {
                book.Close(false, missing, missing);
                app.Quit();
                thisThread.CurrentCulture = originalCulture;
            }
            return true;

        }
    }
}
