using Nova.LCT.GigabitSystem.Common;
using Nova.Monitoring.Common;
using Nova.Monitoring.MonitorDataManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    public class CommonUI
    {
        public static Hashtable HashTable { get; set; }
        public static string LanguageName { get; set; }
        public static System.Drawing.Font SoftwareFont { get; set; }
        public static string ControlConfigLangPath { get; set; }
        public static string BrightnessConfigLangPath { get; set; }
        public static string OpticalProbeConfigLangPath { get; set; }
        public static string ScreenRegistrationLangPath { get; set; }
        public static void SetLanguage(string langType)
        {
            StaticValue.LangType = langType;
            CommonUI.LanguageName = string.Format(StaticValue.LangFileName, langType);
            CommonUI.BrightnessConfigLangPath = string.Format(StaticValue.BrightnessConfigLangName, langType);
            CommonUI.ControlConfigLangPath = string.Format(StaticValue.WHControlConfigLangName, langType);
            CommonUI.OpticalProbeConfigLangPath = string.Format(StaticValue.OpticalProbeConfigLangPath, langType);
            CommonUI.ScreenRegistrationLangPath = string.Format(StaticValue.ScreenRegistrationLangPath, langType);
            Nova.LCT.GigabitSystem.UI.Form_CommunicationMessage.LangFileName = CommonUI.LanguageName;
        }

        public static string GetCustomMessage(string messageKey, string messageDefault)
        {
            string msg = "";
            if (!CustomTransform.GetLanguageString(messageKey, HashTable, out msg))
            {
                msg = messageDefault;
            }
            return msg;
        }
        public static string GetCustomMessage(Hashtable langHashTable, string messageKey, string messageDefault)
        {
            string msg = "";
            if (!CustomTransform.GetLanguageString(messageKey, langHashTable, out msg))
            {
                msg = messageDefault;
            }
            return msg;
        }
    }

    public class StaticValue
    {
        public static string[] DisplayTypeStr = new string[] { "屏ID", "发送卡", "接收卡状态", "温度", "监控卡状态", "湿度", "烟雾", "风扇", "电源", "箱体状态", "箱门状态", "Care状态" };
        public static string[] ColumnHeaderStr = new string[] { "屏名称", "数量" };
        public static string[] FaultAlarmInfoStr = new string[] { "故障信息(无效)", "告警信息", "冗余信息" };
        public static string ColConTextTip = "单击查看详情";

        public static string LangFileName = Application.StartupPath + "\\Lang\\{0}\\Frm_MonitorDisplay.{0}.resources";
        public static string WHControlConfigLangName = Application.StartupPath + "\\Lang\\{0}\\WHControlConfig.{0}.resources";
        public static string BrightnessConfigLangName = Application.StartupPath + "\\Lang\\{0}\\BrightnessConfig.{0}.resources";
        public static string OpticalProbeConfigLangPath = Application.StartupPath + "\\Lang\\{0}\\Frm_OpticalProbeConfig.{0}.resources";
        public static string ScreenRegistrationLangPath = Application.StartupPath + "\\Lang\\{0}\\Frm_RegistrationManager.{0}.resources";
         public static string LangType = string.Empty;
    }
    public delegate void ReceiverChangedEventHandler(object sender, ReceiverChangedEventArgs e);
    public class ReceiverChangedEventArgs : EventArgs
    {
        public DisplayType DisplayType
        {
            get
            {
                return _displayType;
            }
        }
        private DisplayType _displayType = DisplayType.Add;

        public string Name
        {
            get
            {
                return _name;
            }
        }
        private string _name;

        public string EmailAddr
        {
            get { return _emailAddr; }
        }
        private string _emailAddr;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayType"></param>
        /// <param name="nameChanged"></param>
        /// <param name="emailAddr"></param>
        /// <returns></returns>
        public ReceiverChangedEventArgs(DisplayType displayType,
            string name, string emailAddr)
        {
            _displayType = displayType;
            _name = name;
            _emailAddr = emailAddr;
        }
    }
}
