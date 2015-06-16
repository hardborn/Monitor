using Nova.Algorithms.CheckCodes;
using Nova.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace Nova.NovaUpdate
{
    class Program
    {
        private static string _uri;
        private static string _fileMD5;
        private static string _appPath;
        private static string _updatePackectPath;
        private static string _newSoftWareDir;
        private const int RetryTimes = 3;
        private static string _oldSoftWareDirSuffix = "_old";
        private static string _updatePackageFileName = "update.rar";
        private static string _tmpNewSoftWareDir = "updatepacked";
        private static readonly string LogFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @"NovaLCT 2012\Config\Monitoring\SoftWareUpdate.log");
        static private StreamWriter _sw = null;
        static void Main(string[] args)
        {
#if test

            args = new string[3];
            args[0] = "http://nova-update.b0.upaiyun.com/update/97654d980db10e5df731e608b7bdba6c.zip";
            args[1] = "807643A9052A1F450B7969A8EBB1C0C8";
            //string par = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, "MonitorMangager");
            string par = @"C:\Program Files (x86)\Nova Star\NovaLCT-Mars\Bin\MonitorMangager";
            args[2] = Path.Combine(par, "MonitorMangager.exe");
            args[0] = "http://nova-update.b0.upaiyun.com/update/97654d980db10e5df731e608b7bdba6c.zip";
            args[1] = "807643A9052A1F450B7969A8EBB1C0C8";
            string par = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, "MonitorMangager");
            args[2] = Path.Combine(par, "Nova.Monitoring.UI.MonitorMangager.exe");
#endif
            try
            {
                if (!File.Exists(LogFilePath)) File.Create(LogFilePath);
                _sw = new StreamWriter(LogFilePath, true, Encoding.UTF8);
            }
            catch
            {
                System.Console.WriteLine("Open log file error");
                _sw = null;
            }
            if (_sw != null)
            {
                _sw.WriteLine("");
                _sw.WriteLine("");
                _sw.WriteLine("<<<<<<<<<<<<<<<<<<<<Begin update software(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + ")<<<<<<<<<<<<<<<<<<<<");
            }
            if (args == null || args.Length < 3)
            {
                System.Console.WriteLine("args error");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): args error");
                Thread.Sleep(3000);
                CloseFile();
                Thread.Sleep(3000);
                return;
            }
            System.Console.WriteLine("AppPath--->" + args[2]);
            if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): AppPath--->" + args[2]);

            System.Console.WriteLine(string.Format("AppPath--->:", args[2]));

            _uri = args[0];
            _fileMD5 = args[1];
            _appPath = args[2];
            _newSoftWareDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;
            _newSoftWareDir = Path.Combine(_newSoftWareDir, _tmpNewSoftWareDir);
            _updatePackectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _updatePackageFileName);
            int curTimes = 0;
            while (curTimes <= RetryTimes)
            {
                Thread.Sleep(15000);
                System.Console.WriteLine("Begin download update package.");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Begin download update package.");
                if (DownloadFile(_uri, _updatePackectPath))
                {
                    System.Console.WriteLine("Update package download success!");
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Update package download success!");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Update failed download for the " + (curTimes + 1) + " time, try again after 15 seconds!");
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Update failed download for the " + (curTimes + 1) + " time, try again after 15 seconds!");
                    if (curTimes == RetryTimes)
                    {
                        System.Console.WriteLine("Update failed download error");
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Update package download error!");
                        CloseFile();
                        return;
                    }
                    else
                    {
                        System.Console.WriteLine("Update failed download finally(For the " + (curTimes + 1) + " time)!");
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Update failed download finally(For the " + (curTimes + 1) + " time)!");
                    }
                }
                curTimes++;
            }
            bool isMatch;
            MD5Helper.FileMD5ErrorMode errCode;
            isMatch = FileMD5Check(_updatePackectPath, _fileMD5, out errCode);
            if (errCode != MD5Helper.FileMD5ErrorMode.OK || !isMatch)
            {
                Thread.Sleep(3000);
                CloseFile();
                return;
            }
            try
            {
                if (Directory.Exists(_newSoftWareDir)) Directory.Delete(_newSoftWareDir, true);
                System.Console.WriteLine("Delete old dir success(Unzip files dir)");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Delete old dir success(Unzip files dir)");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Delete old dir error(Unzip files dir):" + ex.ToString());
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Delete old dir error(Unzip files dir):" + ex.ToString());
                CloseFile();
                Thread.Sleep(3000);
                return;
            }
            if (!UnZipfile(_updatePackectPath, _newSoftWareDir))
            {
                Thread.Sleep(3000);
                CloseFile();
                return;
            }
            if (!StartNewSoftWare())
            {
                StartOldSoftWare();
            }
            else
                if (!CheckSoftWareStart()) StartOldSoftWare();
            Thread.Sleep(3000);
            CloseFile();
        }
        private static void CloseFile()
        {
            if (_sw != null)
            {
                _sw.WriteLine(">>>>>>>>>>>>>>>>>>>>Update software finish(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + ")>>>>>>>>>>>>>>>>>>>>");
                _sw.Close();
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="downLoadUrl">文件的url路径</param>
        /// <param name="saveFullName">需要保存在本地的路径(包含文件名)</param>
        /// <returns></returns>
        private static bool DownloadFile(string downLoadUrl, string saveFullName)
        {
            bool flagDown = false;
            System.Net.HttpWebRequest httpWebRequest = null;
            try
            {
                //根据url获取远程文件流
                httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(downLoadUrl);

                System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                System.IO.Stream sr = httpWebResponse.GetResponseStream();

                //创建本地文件写入流
                System.IO.Stream sw = new System.IO.FileStream(saveFullName, System.IO.FileMode.Create);

                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = sr.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    sw.Write(by, 0, osize);
                    osize = sr.Read(by, 0, (int)by.Length);
                }
                System.Threading.Thread.Sleep(100);
                flagDown = true;
                sw.Close();
                sr.Close();
            }
            catch (System.Exception ex)
            {
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): " + ex.ToString());
                if (httpWebRequest != null)
                    httpWebRequest.Abort();
            }
            return flagDown;
        }
        /// <summary>
        /// 校验更新包MD5码
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="md5Code"></param>
        /// <param name="errCode"></param>
        /// <returns>是否校验成功</returns>
        private static bool FileMD5Check(string filePath, string md5Code, out MD5Helper.FileMD5ErrorMode errCode)
        {
            string currentMD5Code;
            errCode = MD5Helper.CreateMD5(filePath, out currentMD5Code);
            if (errCode != MD5Helper.FileMD5ErrorMode.OK)
            {
                System.Console.WriteLine("Calculation file md5 error,Err:" + errCode.ToString());
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Calculation file md5 error,Err:" + errCode.ToString());
                try
                {
                    File.Delete(filePath);
                }
                catch { }
                return false;
            }
            if (md5Code.ToLower() != currentMD5Code.ToLower())
            {
                System.Console.WriteLine("File md5 is not match!");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): File md5 is not match!");
                try
                {
                    File.Delete(filePath);
                }
                catch { }
                return false;
            }
            else return true;
        }
        /// <summary>
        /// 解压更新包
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destFileDir"></param>
        /// <returns></returns>
        private static bool UnZipfile(string sourceFilePath, string destFileDir)
        {
            ZipFile zip = new ZipFile();
            if (!zip.UnZipFiles(sourceFilePath, destFileDir, OperateType.Direct, true))
            {
                System.Console.WriteLine("Unzip the files failed");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Unzip the files failed");
                try
                {
                    if (Directory.Exists(destFileDir))
                    {
                        Directory.Delete(destFileDir, true);
                    }
                }
                catch { }
                return false;
            }

            System.Console.WriteLine("Unzip the files success");
            if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Unzip the files success");
            return true;
        }
        #region SoftWare start
        /// <summary>
        /// 判断软件是否在运行
        /// </summary>
        /// <param name="proList">正在运行软件的进程</param>
        /// <returns></returns>
        private static bool IsSoftWareRunning(out List<Process> proList)
        {
            Process[] pList = Process.GetProcesses();
            proList = new List<Process>();
            foreach (var item in pList)
            {
                if (Path.GetFileNameWithoutExtension(_appPath) == item.ProcessName)
                    proList.Add(item);
            }
            if (proList.Count != 0) return true;
            else return false;
        }
        /// <summary>
        /// 启动新版本软件
        /// </summary>
        /// <returns></returns>
        private static bool StartNewSoftWare()
        {
            List<Process> curProList;
            if (IsSoftWareRunning(out curProList))
            {
                foreach (var item in curProList)
                {
                    if (!item.HasExited)
                        item.Kill();
                }
                foreach (var item in curProList)
                {
                    if (!item.HasExited) Thread.Sleep(5000);
                }
            }
            List<Process> tmpPL;
            int count = 20;
            int index = 0;
            while (IsSoftWareRunning(out tmpPL))
            {
                Thread.Sleep(3000);
                index++;
                if (index > count)
                { return false; }
            }

            try
            {
                if (Directory.Exists(Path.GetDirectoryName(_appPath)))
                {
                    if (Directory.Exists(Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix))
                    {
                        System.Console.WriteLine("Begin delete old app directory");
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Begin delete old app directory");
                        Directory.Delete(Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix, true);
                        System.Console.WriteLine("Delete old app directory success");
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Delete old app directory success");
                    }
                    System.Console.WriteLine("App directory = " + Path.GetDirectoryName(_appPath));
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): App directory = " + Path.GetDirectoryName(_appPath));
                    System.Console.WriteLine("Move to directory:" + Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix);
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move to directory:" + Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix);
                    //Directory.Move(Path.GetDirectoryName(_appPath), Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix);
                    if (!DirMove(Path.GetDirectoryName(_appPath), Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix))
                    {
                        System.Console.WriteLine("Move app to old directory error");
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move app to old directory error");
                        return false;
                    }
                    System.Console.WriteLine("Move app to old directory success!");
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move app to old directory success!");
                }
                if (!Directory.Exists(_newSoftWareDir)) return false;
                System.Console.WriteLine("Begin move new software to app directory:");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Begin move new software to app directory:");
                //Directory.Move(_newSoftWareDir, Path.GetDirectoryName(_appPath));
                if (!DirMove(_newSoftWareDir, Path.GetDirectoryName(_appPath)))
                {
                    System.Console.WriteLine("Move new software to app directory error");
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move new software to app directory error");
                    return false;
                }
                System.Console.WriteLine("Move new software to app directory success!");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move new software to app directory success!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Change softWare directory error," + e.ToString());
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Change softWare directory error," + e.ToString());
                return false;
            }
            System.Console.WriteLine("Starting new softWare...");
            if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Starting new softWare...");
            Process.Start(_appPath);
            return true;
        }
        private static bool DirMove(string sourceDir, string destDir)
        {
            try
            {
                string tmpFile, finalFile;
                string[] fileList = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
                foreach (var item in fileList)
                {
                    tmpFile = item.Replace(sourceDir, "");
                    finalFile = destDir + tmpFile;
                    if (!Directory.Exists(Path.GetDirectoryName(finalFile)))
                        Directory.CreateDirectory(Path.GetDirectoryName(finalFile));
                    File.Move(item, finalFile);
                }
                System.Console.WriteLine("Move softWare files success");
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move softWare files success");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Move file error," + ex.ToString());
                if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): Move file error," + ex.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检测软件是否启动。机制：3秒检测一次，总共检测20次。
        /// </summary>
        /// <returns></returns>
        private static bool CheckSoftWareStart()
        {
            List<Process> proList;
            int index = 0;
            bool isRun = IsSoftWareRunning(out proList);
            while (!isRun)
            {
                index++;
                if (index > 20) break;
                Thread.Sleep(3000);
            }
            return isRun;
        }
        /// <summary>
        /// 启动旧版本软件
        /// </summary>
        private static void StartOldSoftWare()
        {
            string appDir = Path.GetDirectoryName(_appPath);
            if (Directory.Exists(Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix))
            {
                if (Directory.Exists(appDir))
                {
                    try
                    {
                        Directory.Delete(appDir, true);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("StartOldSoftWare:Delete directory error," + e.ToString());
                        if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): StartOldSoftWare:Delete directory error," + e.ToString());
                        return;
                    }
                }
                try
                {
                    Directory.Move(Path.GetDirectoryName(_appPath) + _oldSoftWareDirSuffix, Path.GetDirectoryName(_appPath));
                    Process.Start(_appPath);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("StartOldSoftWare:Move directory error," + ex.ToString());
                    if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): StartOldSoftWare:Move directory error," + ex.ToString());
                    return;
                }
            }
            else
            {
                if (Directory.Exists(appDir))
                    Process.Start(_appPath);
            }
            System.Console.WriteLine("StartOldSoftWare:Delete new software,start old software!");
            if (_sw != null) _sw.WriteLine("(UTC:" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "): StartOldSoftWare:Delete new software,start old software!");
        }
        #endregion
    }
}
