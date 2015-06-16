using Nova.Diagnostics;
using Nova.LCT.GigabitSystem.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Nova.Monitoring.UI.MonitorManager
{
    public partial class MonitorMain
    {
        public static Hashtable LangHashTable = null;
        public static Hashtable ProtocalHashTable = null;
        public static ProtocalLanguageParser ProtocalLangParser = new ProtocalLanguageParser("");

        private Form_ErrorNotice _errorNoticeDlg = new Form_ErrorNotice();

        #region 调试信息相关
        private static string NormalInfoBeginStr = ": >>>>";
        private static string SeriousErrorBeginStr = ":@@@@@@@@";
        private static string NormalClassNameSeperate = "--";
        private static string NormalInfoEnd = "=====";
        private static string SeriousErrorInfEnd = "XXXXXX";
        private readonly string CLASS_NAME = "MonitorPlatForm";
        #endregion
    }
}
