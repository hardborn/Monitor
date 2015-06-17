using Nova.Algorithms.CheckCodes;
using Nova.Database.DataBaseManager;
using Nova.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Care.Updater
{
    class Program
    {
        private static readonly int RETRY_TIMES = 3;
        private static readonly string UPATE_DETAILS_FILE_NAME = "CheckUpdateList.db";
        private static readonly string BACKUP_FOLDER_NAME = "Backup";
        private static readonly string CURRENT_PATH = Environment.CurrentDirectory;
        private static readonly string UPDATE_PACKAGE_PATH = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Update_Package");
        private static readonly string UNZIP_UPDATE_PACKAGE_PATH = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), Guid.NewGuid().ToString());
        private static readonly string UPATE_DETAILS_FILE_PATH = Path.Combine(UNZIP_UPDATE_PACKAGE_PATH, UPATE_DETAILS_FILE_NAME);
        private static readonly string BACKUP_SOFTWARE_PATH = Path.Combine(UPDATE_PACKAGE_PATH, BACKUP_FOLDER_NAME);
        

        private static readonly string SQL_GET_UPDATE_DETAILS = "select dictype,sourcedic,targerdic from checklist";

        private static string _downloadLink = string.Empty;
        private static string _checkDigit = string.Empty;
        private static string _applicationPath = string.Empty;

        private static List<UpdatePackageDetails> _updateDetailsList = new List<UpdatePackageDetails>();

        static void Main(string[] args)
        {
           // args = new string[3];
           // args[0] = "http://nova-update.b0.upaiyun.com/update/97654d980db10e5df731e608b7bdba6c.zip";
           // args[1] = "807643A9052A1F450B7969A8EBB1C0C8";
           //args[2] = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, "Test.exe");
            //args[2] = Path.Combine(par, "Nova.Monitoring.UI.MonitorMangager.exe");

            if (args == null && args.Length != 3)
            {
                return;
            }

            _downloadLink = args[0];
            _checkDigit = args[1];
            _applicationPath = args[2];



            FetchUpdatePackage(_downloadLink, UPDATE_PACKAGE_PATH);
            List<Process> relatedProcesses;
            System.Console.WriteLine("Staring check is software running");
            do
            {
                relatedProcesses = GetRelatedProcessesBy(_applicationPath);
                foreach (var item in relatedProcesses)
                {
                    if (!item.HasExited)
                    {
                        System.Console.WriteLine("Software is running,kill it");
                        item.Kill();
                    }
                }
                foreach (var item in relatedProcesses)
                {
                    if (!item.HasExited)
                    {
                        Thread.Sleep(5000);
                    }
                }
            } while (IsSoftWareRunning(_applicationPath));

            if (!IsSoftWareRunning(_applicationPath))
            {
                BackupSoftwarePackage();
                if (ApplyUpdatePackage())
                {
                    RollbackSoftwarePackage();
                }
            } 
            
        }

        private static bool RollbackSoftwarePackage()
        {
            foreach (var item in _updateDetailsList)
            {
                if (item.Type == TargetType.File)
                {
                    IOHelper.CopyFile(Path.Combine(BACKUP_SOFTWARE_PATH, item.UpdateTarget), Path.Combine(CURRENT_PATH, item.UpdateTarget));
                }
                else if (item.Type == TargetType.Folder)
                {
                    IOHelper.CopyFolder(Path.Combine(BACKUP_SOFTWARE_PATH, item.UpdateTarget), Path.Combine(CURRENT_PATH, item.UpdateTarget));
                }
            }

            try
            {
                Process.Start(_applicationPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("StartOldSoftWare:Move directory error," + ex.ToString());
                return false;
            }
            return true;


        }

        private static bool ApplyUpdatePackage()
        {
            foreach (var item in _updateDetailsList)
            {
                if (item.Type == TargetType.File)
                {
                    IOHelper.CopyFile(Path.Combine(CURRENT_PATH, item.UpdateSource), Path.Combine(CURRENT_PATH, item.UpdateTarget));
                }
                else if (item.Type == TargetType.Folder)
                {
                    IOHelper.CopyFolder(Path.Combine(CURRENT_PATH, item.UpdateSource), Path.Combine(CURRENT_PATH, item.UpdateTarget));
                }
            }

            try
            {
                Process.Start(_applicationPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("StartOldSoftWare:Move directory error," + ex.ToString());
                return false;
            }
            return true;
        }

        private static void BackupSoftwarePackage()
        {
            foreach (var item in _updateDetailsList)
            {
                if (item.Type == TargetType.File)
                {
                    IOHelper.CopyFile(Path.Combine(CURRENT_PATH, item.UpdateTarget), Path.Combine(BACKUP_SOFTWARE_PATH, item.UpdateTarget));
                }
                else if (item.Type == TargetType.Folder)
                {
                    IOHelper.CopyFolder(Path.Combine(CURRENT_PATH, item.UpdateTarget), Path.Combine(BACKUP_SOFTWARE_PATH, item.UpdateTarget));
                }
            }
        }

        private static void FetchUpdatePackage(string downloadLink, string localFilePath)
        {
            bool downloadResult = false;
            int downloadTimes = 0;
            do
            {
                downloadResult = DownloadUpdatePackage(downloadLink, localFilePath);
                if (downloadResult)
                {
                    break;
                }
                downloadTimes++;
            } while (downloadTimes < RETRY_TIMES);

            bool isMatch;
            MD5Helper.FileMD5ErrorMode errCode;
            isMatch = CheckUpdatePackageMD5(localFilePath, _checkDigit, out errCode);
            if (errCode != MD5Helper.FileMD5ErrorMode.OK || !isMatch)
            {
                return;
            }

            if (!UnZipUpdatePackage(localFilePath, UNZIP_UPDATE_PACKAGE_PATH))
            {
                return;
            }

            GetUpdateDetailsList(UPATE_DETAILS_FILE_PATH);
        }

        private static void GetUpdateDetailsList(string detailsFilePath)
        {
            var dbReader = new DbSqLiteHelper(detailsFilePath);
            dbReader.ConnectionInit();
            var dt = dbReader.ExecuteDataTable(SQL_GET_UPDATE_DETAILS);
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            for (int index = 0; index < dt.Rows.Count; index++)
            {
                var details = new UpdatePackageDetails();
                details.Type = (TargetType)dt.Rows[index][0];
                details.UpdateSource = (string)dt.Rows[index][1];
                details.UpdateTarget = (string)dt.Rows[index][2];
                _updateDetailsList.Add(details);
            }
        }


        private static bool UnZipUpdatePackage(string sourceFilePath, string destFileDir)
        {
            ZipFile zip = new ZipFile();
            if (!zip.UnZipFiles(sourceFilePath, destFileDir, OperateType.Direct, true))
            {
                System.Console.WriteLine("Unzip the files failed");
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
            return true;
        }

        private static bool CheckUpdatePackageMD5(string filePath, string md5Code, out MD5Helper.FileMD5ErrorMode errCode)
        {
            string currentMD5Code;
            errCode = MD5Helper.CreateMD5(filePath, out currentMD5Code);
            if (errCode != MD5Helper.FileMD5ErrorMode.OK)
            {
                System.Console.WriteLine("Calculation file md5 error,Err:" + errCode.ToString());
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
                try
                {
                    File.Delete(filePath);
                }
                catch { }
                return false;
            }
            else return true;
        }

        private static bool DownloadUpdatePackage(string downloadLink, string localFilePath)
        {
            System.Net.WebClient webClient = new System.Net.WebClient();
            try
            {
                webClient.DownloadFile(downloadLink, localFilePath);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("下载链接为NULL");
                return false;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("下载数据时发生错误");
                return false;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("该方法已在多个线程上同时使用");
                return false;
            }
            return true;
        }

        private static bool CheckUpdatePackage(string checkDigit)
        {
            bool isMatch;
            MD5Helper.FileMD5ErrorMode errCode;
            isMatch = CheckUpdatePackageMD5(UPDATE_PACKAGE_PATH, checkDigit, out errCode);
            if (errCode != MD5Helper.FileMD5ErrorMode.OK)
            {
                return false;
            }
            return isMatch;
        }
        
        private static bool IsSoftWareRunning(string appPath)
        {
            Process[] pList = Process.GetProcesses();
            return pList.FirstOrDefault(p => p.ProcessName == Path.GetFileNameWithoutExtension(appPath)) == null ? false : true;
        }

        private static List<Process> GetRelatedProcessesBy(string appPath)
        {
            List<Process> proList;
            Process[] pList = Process.GetProcesses();
            proList = new List<Process>();
            foreach (var item in pList)
            {
                if (Path.GetFileNameWithoutExtension(appPath) == item.ProcessName)
                {
                    proList.Add(item);
                }
            }
            return proList;
        }
    }
}
