using Nova.LCT.GigabitSystem.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorManager
{
    public class CommonInfo
    {
        public readonly static string ERRORLOGPATH = ConstValue.MYDOCUMENT_PATH + @"Monitor\ErrorLog\";
        
        public readonly static string AppLangPath = Application.StartupPath + @"\Lang\";
        public static string LanFileName = AppLangPath + @"zh-CN\BrightAdjustTool.zh-CN.resources";
        public static string ProtocalLangFileName = AppLangPath + @"zh-CN\Protocal.zh-CN.resources";
        public readonly static string LangTypeFileName = ConstValue.CONFIG_FOLDER + @"\Lang.txt";
        public readonly static string SERVER_NAME = "MarsServerProvider";
        public readonly static string SERVER_PATH = Application.StartupPath + "..\\MarsServerProvider\\" + SERVER_NAME + ".exe";
    }
}