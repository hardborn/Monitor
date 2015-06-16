using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Nova.Monitoring.Common;
using Nova.Monitoring.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Nova.Monitoring.ClientDispatcher.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NovaCareServerAddress = "172.16.80.166";
        private const int DataServicePort = 8888;

        //private DataDispatcher.ClientDispatcher _dispatcher;
        private List<DataSourceInfo> _datasourceInfos = new List<DataSourceInfo>();
        private List<DataDispatcherInfo> _dataDispatcherInfos = new List<DataDispatcherInfo>();

        private ObservableCollection<LedInfo> _ledInfoList = new ObservableCollection<LedInfo>();
        public MainWindow()
        {
            InitializeComponent();
            
            AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory + "/DataSource");
            DispatcherHelper.Initialize();
            DataEngine.Dispatcher = DispatcherHelper.UIDispatcher;
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }

        public List<DataSourceInfo> DataSourceInfos
        {
            get { return _datasourceInfos; }
            set
            {
                if (_datasourceInfos == value)
                {
                    return;
                }
                _datasourceInfos = value;
            }
        }
        public List<DataDispatcherInfo> DataDispatcherInfos
        {
            get { return _dataDispatcherInfos; }
            set
            {
                if (_dataDispatcherInfos == value)
                {
                    return;
                }
                _dataDispatcherInfos = value;
            }
        }

        public ObservableCollection<LedInfo> LedInfoList
        {
            get { return _ledInfoList; }
            set
            { _ledInfoList = value; }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //StartCoreService();
            LoadDispatchers();
            //LoadDataSources();
        }

        private string StartCoreService()
        {
            try
            {
                DataEngine.StartService();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        private void LoadDispatchers()
        {
            DataDispatcher.ClientDispatcher.Instance.Initialize("localhost", DataServicePort);
            //DataDispatcher.ClientDispatcher.Instance.MonitoringDataReceived += MainWindow_MonitoringDataReceived;
            //DataDispatcher.ClientDispatcher.Instance.CommandReceived += MainWindow_CommandReceived;
            //DataDispatcher.ClientDispatcher.Instance.LedBaseInfoUpdated += Instance_LedBaseInfoUpdated;
        }

        void Instance_LedBaseInfoUpdated(List<LedBasicInfo> registationInfos)
        {
            //to do something
            DataDispatcher.ClientDispatcher.Instance.GetLedRegistationInfo("", "");

        }

        void MainWindow_CommandReceived(Command commad)
        {
            //to do something
        }

        void MainWindow_MonitoringDataReceived(string key,object monitoringData)
        {
            //to do something
        }

        private void LoadDataSources()
        {
            InitializeDataSourceInfo();
            foreach (var info in DataSourceInfos)
            {
                Type type = Type.GetType(info.Type, false, true);

                if (type != null)
                {
                    DataEngine.LoadDataSource(type, true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.Location))
                    {
                        string filePath = string.Empty;
                        if (Path.IsPathRooted(info.Location))
                        {
                            filePath = info.Location;

                            //filePath = new Uri(System.IO.Path.Combine(basePath, relativePath)).LocalPath;
                        }
                        else
                        {
                            filePath = Path.GetFullPath(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, info.Location));
                        }

                        FileInfo assemblypath = new FileInfo(filePath);
                        Assembly ass = Assembly.LoadFile(assemblypath.FullName);

                        foreach (Type t in ass.GetExportedTypes())
                        {
                            if (t.AssemblyQualifiedName == info.Type || t.FullName == info.Type)
                            {
                                type = t;
                                break;
                            }
                        }
                        if (type != null)
                        {
                            Action action = new Action(() =>
                            {
                                DataEngine.LoadDataSource(type, true);
                            });
                            action.BeginInvoke(new AsyncCallback(GetLedInfo), null);
                        }
                    }
                }
            }
        }

        private void InitializeDataSourceInfo()
        {
            DataSourceInfos.Add(new DataSourceInfo()
            {
                Type = "Nova.NovaCare.DataAcquisition",
                Location = "./DataSource/Nova.NovaCare.DataAcquisition.dll",
                Name = "M3数据源"
            });
        }

        private void InitializeDataDispatcherInfo()
        {
            DataDispatcherInfos.Add(new DataDispatcherInfo()
            {
                Type = "Nova.Monitoring.DataDispatcher.ClientDispatcher",
                Location = "./DataDispatcher/Nova.Monitoring.DataDispatcher.dll",
                Name = "ClientDispatcher"
            });
        }

        private void GetLedInfo(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                foreach (var source in DataEngine.DataSources)
                {
                    //foreach (var led in source.LedDeviceInfos)
                    //{
                    //    DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
                    //    {
                    //        LedInfoList.Add(led);
                    //    }));                        
                    //}
                }
            }
            
        }
    }



    [Serializable]
    public class DataSourceInfo
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string Type { get; set; }
        [XmlAttribute()]
        public string Location { get; set; }
    }

    [Serializable]
    public class DataDispatcherInfo
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public string Type { get; set; }
        [XmlAttribute()]
        public string Location { get; set; }
    }
}
