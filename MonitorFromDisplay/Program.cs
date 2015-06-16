using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorFromDisplay
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        #region 处理未捕获异常的挂钩函数
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                MessageBox.Show(string.Format("出现应用程序未处理的异常\n异常类型：{0}\n异常消息：{1}\n异常位置：{2}\n",
                error.GetType().Name, error.Message, error.StackTrace), "main");
            }
            else
            {
                MessageBox.Show(string.Format("Application ThreadError:{0}", e));
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = e.ExceptionObject as Exception;
            if (error != null)
            {
                MessageBox.Show(string.Format("Application UnhandledException:{0};\n堆栈信息:{1}", error.Message, error.StackTrace));
            }
            else
            {
                MessageBox.Show(string.Format("Application UnhandledError:{0}", e));
            }
        }

        #endregion
    }
}