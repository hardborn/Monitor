using Nova.Database.DataBaseManager;
using Nova.Monitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DAL
{
    public class MonitorDataAccessor : IDisposable
    {
        private IDataBaseManager _idbWrite;
        private IDataBaseManager _idbRead;
        private static MonitorDataAccessor _instance = null;
        private static object _lock = new object();
        public static MonitorDataAccessor Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MonitorDataAccessor();
                    }
                }
            }
            return _instance;
        }
        private bool _isOpenDbResult = false;
        public bool IsOpenDbResult
        {
            get
            {
                return _isOpenDbResult;
            }
        }

        private MonitorDataAccessor()
        {
            string monitorBasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"NovaLCT 2012\Config\Monitoring\");
            string dataBaseFilePath = System.IO.Path.Combine(monitorBasePath, "MonitorDb.db");

            _idbWrite = new DbSqLiteHelper(dataBaseFilePath);
            _idbRead = new DbSqLiteHelper(dataBaseFilePath);
            _idbWrite.ConnectionInit();
            _idbRead.ConnectionInit();
            _isOpenDbResult = CreateTables();
        }

        #region 公共方法
        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="opLog"></param>
        /// <returns></returns>

        public bool InsertOprLog(OprLog opLog)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Format("insert into oprlog values('{0}',{1},{2},datetime('now'),'{3}','{4}');", opLog.Sn, opLog.Type, opLog.Source, opLog.Content, opLog.Condition);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <param name="type">操作类型，<0代表不限制</param>
        /// <param name="source">操作源，<0代表不限制</param>
        /// <param name="minDT">时段起始时间</param>
        /// <param name="maxDT">时段结束时间</param>
        /// <returns></returns>
        public List<OprLog> GetOprLog(string ledSN, int type, int source, long minDT, long maxDT)
        {
            List<OprLog> oprList = new List<OprLog>();
            string strTmp = "select sn,type,source,updatetime,content,condition from oprlog where";
            string strSql = "select sn,type,source,updatetime,content,condition from oprlog where";
            if (ledSN != string.Empty) strSql += " sn=" + "'" + ledSN + "'";
            if (type >= 0)
            {
                if (strTmp == strSql) strSql += " type=" + type;
                else strSql += " and type=" + type;
            }
            if (source >= 0)
            {
                if (strTmp == strSql) strSql += " source=" + source;
                else strSql += " and source=" + source;
            }
            if (strTmp == strSql) strSql += " updatetime between " + minDT + " and " + maxDT;
            else strSql += " and updatetime between " + minDT + " and " + maxDT;
            strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return oprList;
            OprLog opLog;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                opLog = new OprLog();
                opLog.Sn = (string)dt.Rows[i][0];
                opLog.Type = (int)dt.Rows[i][1];
                opLog.Source = (int)dt.Rows[i][2];
                opLog.Updatetime = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][3]);
                opLog.Content = (string)dt.Rows[i][4];
                opLog.Condition = (string)dt.Rows[i][5];
                oprList.Add(opLog);
            }
            return oprList;
        }
        /// <summary>
        /// 更新显示屏策略
        /// </summary>
        /// <param name="ledSN">显示屏SN号</param>
        /// <param name="content">策略内容</param>
        /// <returns></returns>
        public bool UpdateStrategy(string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            string strategyInfo;
            DateTime strategyTime;
            GetStrategy(out strategyInfo, out strategyTime);
            if (strategyInfo != string.Empty)
                strSql = string.Format("update strategycfg set content='{0}',updatetime='{1}';", content, time);
            else strSql = string.Format("insert into strategycfg values('{0}','{1}');", content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询策略信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <returns></returns>
        public void GetStrategy(out string strategyInfo, out DateTime time)
        {
            time = new DateTime();
            strategyInfo = string.Empty;
            string strSql = "select content,updatetime from strategycfg;";
            //if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            //else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return;
            strategyInfo = (string)dt.Rows[0][0];
            time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[0][1]);
        }
        /// <summary>
        /// 更新硬件配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号</param>
        /// <param name="content">硬件配置信息</param>
        /// <returns></returns>
        public bool UpdateHardWareCfg(string ledSN, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            if (GetHardWareCfg(ledSN).Count != 0)
                strSql = string.Format("update hardwarecfg set content='{0}',updatetime='{1}' where sn='{2}';", content, time, ledSN);
            else
            {
                strSql = string.Format("insert into hardwarecfg values('{0}','{1}','{2}');", ledSN, content, time);
            }
            //string temp = strSql + string.Format("('{0}','{1}',{2});", ledSN, content, DateTime.UtcNow.ToFileTimeUtc());
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);//"('{0}',{1});"
        }
        /// <summary>
        /// 查询硬件配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <returns></returns>
        public List<ConfigInfo> GetHardWareCfg(string ledSN)
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            ConfigInfo cfg;
            string strSql = "select sn,content,updatetime from hardwarecfg";
            if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return configInfoList;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfg = new ConfigInfo();
                cfg.SN = (string)dt.Rows[i][0];
                cfg.Content = (string)dt.Rows[i][1];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][2]);
                configInfoList.Add(cfg);
            }
            return configInfoList;
        }
        /// <summary>
        /// 更新告警配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号</param>
        /// <param name="content">告警配置信息</param>
        /// <returns></returns>
        public bool UpdateAlarmCfg(string ledSN, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            if (GetAlarmCfg(ledSN).Count != 0)
                strSql = string.Format("update alarmcfg set content='{0}',updatetime='{1}' where sn='{2}'", content, time, ledSN);
            else strSql = string.Format("insert into alarmcfg values('{0}','{1}','{2}')", ledSN, content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 更新点检周期配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号</param>
        /// <param name="content">告警配置信息</param>
        /// <returns></returns>
        public bool UpdatePeriodicInspectionCfg(string ledSN, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            if (GetPeriodicInspectionCfg(ledSN).Count != 0)
                strSql = string.Format("update periodicInspectionConfig set content='{0}',updatetime='{1}' where sn='{2}'", content, time, ledSN);
            else strSql = string.Format("insert into periodicInspectionConfig values('{0}','{1}','{2}')", ledSN, content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }

        /// <summary>
        /// 获取告警配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <returns></returns>
        public List<ConfigInfo> GetAlarmCfg(string ledSN)
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            ConfigInfo cfg;
            string strSql = "select sn,content,updatetime from alarmcfg";
            if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return configInfoList;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfg = new ConfigInfo();
                cfg.SN = (string)dt.Rows[i][0];
                cfg.Content = (string)dt.Rows[i][1];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][2]);
                configInfoList.Add(cfg);
            }
            return configInfoList;
        }

        /// <summary>
        /// 获取点检周期配置信息
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <returns></returns>
        public List<ConfigInfo> GetPeriodicInspectionCfg(string ledSN)
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            ConfigInfo cfg;
            string strSql = "select sn,content,updatetime from periodicInspectionConfig";
            if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return configInfoList;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfg = new ConfigInfo();
                cfg.SN = (string)dt.Rows[i][0];
                cfg.Content = (string)dt.Rows[i][1];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][2]);
                configInfoList.Add(cfg);
            }
            return configInfoList;
        }

        /// <summary>
        /// 更新外设配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool UpdatePeripheralCfg(string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            string peripheralCfgInfo;
            DateTime storeTime;
            GetPeripheralCfg(out peripheralCfgInfo, out storeTime);
            if (!string.IsNullOrEmpty(peripheralCfgInfo))
                strSql = string.Format("update peripheralcfg set content='{0}',updatetime='{1}'", content, time);
            else strSql = string.Format("insert into peripheralcfg values('{0}','{1}')", content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询外设信息
        /// </summary>
        /// <param name="key">空串代表不限制</param>
        /// <returns></returns>
        public void GetPeripheralCfg(out string peripheralCfgInfo, out DateTime time)
        {
            peripheralCfgInfo = string.Empty;
            time = new DateTime();
            string strSql = "select content,updatetime from peripheralcfg;";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return;
            peripheralCfgInfo = (string)dt.Rows[0][0];
            time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[0][1]);
        }
        /// <summary>
        /// 光探头配置信息更新
        /// </summary>
        /// <param name="ledSN">显示屏SN号</param>
        /// <param name="content">光探头列表</param>
        /// <returns></returns>
        public bool UpdateLightProbeCfg(string ledSN, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            if (GetLightProbeCfg(ledSN).Count != 0)
                strSql = string.Format("update lightprobecfg set content='{0}',updatetime='{1}' where sn='{2}'", content, time, ledSN);
            else strSql = string.Format("insert into lightprobecfg values('{0}','{1}','{2}')", ledSN, content, time);
            strSql += ";";
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询光探头
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <returns></returns>
        public List<ConfigInfo> GetLightProbeCfg(string ledSN)
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            ConfigInfo cfg;
            string strSql = "select sn,content,updatetime from lightprobecfg";
            if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return configInfoList;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfg = new ConfigInfo();
                cfg.SN = (string)dt.Rows[i][0];
                cfg.Content = (string)dt.Rows[i][1];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][2]);
                configInfoList.Add(cfg);
            }
            return configInfoList;
        }
        /// <summary>
        /// 更新软件配置信息
        /// </summary>
        /// <param name="content">配置参数</param>
        /// <returns></returns>
        public bool UpdateSoftWareCfg(string content, long time)
        {
            if (!DeleteDataFromDataTable(string.Empty, "softwareconfig")) return false;
            List<string> strSqlList = new List<string>();
            string strSql = "insert into softwareconfig values" + string.Format("('{0}','{1}');", content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询软件配置信息
        /// </summary>
        /// <returns></returns>
        public void GetSoftWareCfg(out List<string> swConfigList, out List<DateTime> swConfigStoreTimeList)
        {
            swConfigList = new List<string>();
            swConfigStoreTimeList = new List<DateTime>();
            string strSql = "select content,updatetime from softwareconfig";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                swConfigList.Add((string)dt.Rows[i][0]);
                swConfigStoreTimeList.Add(SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][1]));
            }
        }
        /// <summary>
        /// 更新用户配置信息
        /// </summary>
        /// <param name="content">用户配置信息</param>
        /// <returns></returns>
        public bool UpdateUserCfg(string content, long time)
        {
            if (!DeleteDataFromDataTable(string.Empty, "userconfig")) return false;
            List<string> strSqlList = new List<string>();
            string strSql = "insert into userconfig values";
            strSqlList.Add(strSql + string.Format("('{0}','{1}');", content, time));
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询用户配置信息
        /// </summary>
        /// <returns></returns>
        public ConfigInfo GetUserCfg()
        {
            string strSql = "select content,updatetime from userconfig";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            ConfigInfo cfg = new ConfigInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                cfg.SN = string.Empty;
                cfg.Content = (string)dt.Rows[0][0];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[0][1]);
            }
            return cfg;
        }
        /// <summary>
        /// 更新点检配置信息
        /// </summary>
        /// <param name="content">点检配置信息</param>
        /// <returns></returns>
        public bool UpdatePointDetectCfg(string ledSN, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Empty;
            if (GetPointDetectCfg(ledSN).Count != 0)
                strSql = string.Format("update pointdetectconfig set content='{0}',updatetime='{1}' where sn='{2}'", content, time, ledSN);
            else strSql = string.Format("insert into pointdetectconfig values('{0}','{1}','{2}')", ledSN, content, time);
            strSql += ";";
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询点检配置信息
        /// </summary>
        /// <returns></returns>
        public List<ConfigInfo> GetPointDetectCfg(string ledSN)
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            ConfigInfo cfg;
            string strSql = "select sn,content,updatetime from pointdetectconfig";
            if (ledSN != string.Empty) strSql += " where sn=" + "'" + ledSN + "'" + ";";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return configInfoList;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfg = new ConfigInfo();
                cfg.SN = (string)dt.Rows[i][0];
                cfg.Content = (string)dt.Rows[i][1];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[i][2]);
                configInfoList.Add(cfg);
            }
            return configInfoList;
        }
        public string GetAlarmConfigSyncMarkBy(string sn)
        {
            DateTime timestamp = new DateTime();
            string strSql = "select updatetime from alarmcfg";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            timestamp = (DateTime)dt.Rows[0][0];
            long time = timestamp.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            time = time / 10000000;
            return time.ToString();
        }
        /// <summary>
        /// 更新Email配置
        /// </summary>
        /// <param name="content">用户配置信息</param>
        /// <returns></returns>
        public bool UpdateEmailCfg(string content, long time)
        {
            if (!DeleteDataFromDataTable(string.Empty, "EmailConfig")) return false;
            List<string> strSqlList = new List<string>();
            string strSql = "insert into EmailConfig values";
            strSqlList.Add(strSql + string.Format("('{0}','{1}');", content, time));
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 读取Email配置
        /// </summary>
        /// <returns></returns>
        public ConfigInfo GetEmailCfg()
        {
            string strSql = "select content,updatetime from EmailConfig";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            ConfigInfo cfg = new ConfigInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                cfg.SN = string.Empty;
                cfg.Content = (string)dt.Rows[0][0];
                cfg.Time = SystemHelper.GetTimeByUtcTicks((long)dt.Rows[0][1]);
            }
            return cfg;
        }

        /// <summary>
        /// 插入一条邮件发送操作日志
        /// </summary>
        /// <param name="opLog"></param>
        /// <returns></returns>
        public bool InsertSendEmailOprLog(string times, string content, long time)
        {
            List<string> strSqlList = new List<string>();
            string strSql = string.Format("insert into SendEmailLog values('{0}','{1}','{2}');", times, content, time);
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        /// <summary>
        /// 查询一天邮件操作操作日志
        /// </summary>
        /// <param name="ledSN">显示屏SN号，空串代表不限制</param>
        /// <param name="type">操作类型，<0代表不限制</param>
        /// <param name="source">操作源，<0代表不限制</param>
        /// <param name="minDT">时段起始时间</param>
        /// <param name="maxDT">时段结束时间</param>
        /// <returns></returns>
        public List<string> GetSendEmailOprLog(string times)
        {
            List<string> oprList = new List<string>();
            string strSql = "select times,content,updatetime from SendEmailLog ";
            if (times != string.Empty) strSql += "where times=" + "'" + times + "'";
            else strSql += ";";
            DataTable dt = _idbRead.ExecuteDataTable(strSql);
            if (dt == null || dt.Rows.Count == 0) return oprList;
            //ConfigInfo opLog;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //opLog = new ConfigInfo();
                //opLog.Content = (string)dt.Rows[i][1];
                oprList.Add((string)dt.Rows[i][1]);
            }
            return oprList;
        }
        /// <summary>
        /// 删除一天的邮件发送日志
        /// </summary>
        /// <param name="times"></param>
        public bool DeleteOneSendEmailOprLog(string times)
        {
            string strSql = "delete from SendEmailLog where";
            if (times != string.Empty) strSql += " times=" + "'" + times + "'";
            int index = _idbRead.ExecuteNonQuery(strSql);
            if (index == -5)
            {
                return false;
            }
            return true;
        }
        public void Dispose()
        {
            if (_idbWrite != null)
            {
                _idbWrite.Dispose();
            }
            if (_idbRead != null)
            {
                _idbRead.Dispose();
            }
            if (_instance != null)
            {
                _instance = null;
            }
        }
        public bool CreateTables()
        {
            List<string> strSqlList = new List<string>();
            string strSql = "create database novacare if not exists";
            //DataTable Name：oprlog
            //col:sn,type,source,updatetime,content,condition
            strSql = "create table if not exists oprlog(sn varchar(50),type int,source int,updatetime long DEFAULT CURRENT_TIMESTAMP,content text,condition varchar(150));";
            strSqlList.Add(strSql);
            //strategycfg
            //content,updatetime
            strSql = "create table if not exists strategycfg(content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //hardwarecfg
            //sn,content,updatetime
            strSql = "create table if not exists hardwarecfg(sn varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //alarmcfg
            //sn,content,updatetime
            strSql = "create table if not exists alarmcfg(sn varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //peripheralcfg
            //key,content,updatetime
            strSql = "create table if not exists peripheralcfg(content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //lightprobecfg
            //sn,content,updatetime
            strSql = "create table if not exists lightprobecfg(sn varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //softwareconfig
            //content,updatetime
            strSql = "create table if not exists softwareconfig(content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);
            //userconfig
            //content,updatetime
            strSql = "create table if not exists userconfig(content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);

            //periodicInspectionConfig
            strSql = "create table if not exists periodicInspectionConfig(sn varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP);";
            strSqlList.Add(strSql);

            //pointdetectconfig
            //sn,content,updatetime
            strSql = "create table if not exists pointdetectconfig(sn varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP)";
            strSqlList.Add(strSql);
            strSql = "create table if not exists EmailConfig(content text,updatetime long DEFAULT CURRENT_TIMESTAMP)";
            strSqlList.Add(strSql);
            strSql = "create table if not exists SendEmailLog(times varchar(50),content text,updatetime long DEFAULT CURRENT_TIMESTAMP)";
            strSqlList.Add(strSql);
            strSql = "create index if not exists idx_sendemaillog on SendEmailLog(times)";
            strSqlList.Add(strSql);
            return _idbWrite.ExecuteNonQueryTrans(strSqlList);
        }
        #endregion
        #region 私有方法
        private bool DeleteDataFromDataTable(string sn, string tableName)
        {
            string strSql;
            if (sn != string.Empty) strSql = "delete from " + tableName + " where sn=" + "'" + sn + "'";
            else strSql = "delete from " + tableName;
            strSql += ";";
            List<string> strSqlList = new List<string>();
            strSqlList.Add(strSql);
            return _idbRead.ExecuteNonQueryTrans(strSqlList);
        }
        #endregion
    }
    public struct OprLog
    {
        public string Sn;
        public int Type;
        public int Source;
        public DateTime Updatetime;
        public string Content;
        public string Condition;
    }
    public struct ConfigInfo
    {
        public string SN;
        public string Content;
        public DateTime Time;
    }
}
