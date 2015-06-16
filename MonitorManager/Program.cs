using Log4NetLibrary;
using Nova.Diagnostics;
using Nova.LCT.GigabitSystem.Common;
using Nova.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace Nova.Monitoring.UI.MonitorManager
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        //private static Thread _startupThread = null;
        private static ILogService _logService = new FileLogService(typeof(UnhandledExceptionEventArgs));
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool can_execute = true;
            try
            {
                mutex = new System.Threading.Mutex(false, "MONITORMANAGER", out can_execute);
            }
            catch (Exception ex)
            {
                _logService.Error("ExistCatch：获取Mutex时出现异常：" + ex.ToString());
            }
            if (can_execute)
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args == null || args.Length == 0 || args[0].ToLower() == "true")
                {
                    Application.Run(new MonitorMain(true));
                }
                else
                {
                    Application.Run(new MonitorMain(false));
                }
            }
            else
            {
                _logService.Info("MONITORMANAGER already running!");
                Debug.WriteLine("MONITORMANAGER already running!");
            }
        }
        private static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;

            if (exception != null)
            {
                ILogService logService = new FileLogService(typeof(UnhandledExceptionEventArgs));
                logService.Error(string.Format("错误：{0} \r\n错误详情：{1}",exception.Message,exception.StackTrace));
            }
           
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ILogService logService = new FileLogService(typeof(System.Threading.ThreadExceptionEventArgs));
            logService.Error(string.Format("错误：{0} \r\n错误详情：{1}", e.Exception.Message, e.Exception.StackTrace));
        }
        private static Process RunningInstance(Process current)
        {
            _logService.Debug("Monitor---当前线程ID:" + current.Id);
            Process[] processes = Process.GetProcessesByName("NovaMonitorManager");
            foreach (Process process in processes)
            {
                _logService.Debug("Monitor---找到进程名相同的ID:" + process.Id);
                if (process.Id != current.Id)
                {
                    _logService.Debug("Monitor---找到进程名相同但不是本进程的ID:" + process.Id);
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        _logService.Debug("Monitor---找到同一文件启动进程的ID:" + process.Id);
                        return process;
                    }
                    else
                    {
                        _logService.Debug("Monitor---找到不同文件启动进程的ID:" + process.Id);
                        return process;
                    }
                }
            }
            _logService.Debug("Monitor---找不到同进程名且同一文件启动的ID");
            return null;
        }
    }
}