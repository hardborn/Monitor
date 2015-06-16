
using GalaSoft.MvvmLight.Threading;
using Log4NetLibrary;
using Nova.Monitoring.Common;
using Nova.Monitoring.DAL;
using Nova.Monitoring.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Nova.Monitoring.Engine
{
    public static class DataEngine
    {
        private static CommandManageQueue _cmdManager;
        private static ObservableCollection<DataSourceBase> _dataSources;
        private static ObservableCollection<DataService> _serviceCollection = new ObservableCollection<DataService>();
        private static List<DataService> innerServiceCollection = new List<DataService>();
        // private static RuleEngine _ruleEngine;

        private static Dictionary<string, DataPoint> _datamap;
        private static Dispatcher _dispatcher;


        private static bool _recordEnable = true;

        private static object _sync = new object();

        public delegate void EngineExceptionThrown(Exception exception);
        public static EngineExceptionThrown OnEngineExceptionThrown;
        public static event EventHandler<ExceptionOccurredEventArgs> DataSourceExceptionOccurred;

        private static ILogService _logService = new FileLogService(typeof(DataEngine));
        public static Dispatcher Dispatcher
        {
            get { return DataEngine._dispatcher; }
            set { DataEngine._dispatcher = value; }
        }
        public static ObservableCollection<DataService> Clients
        {
            get { return DataEngine._serviceCollection; }
        }
        public static ObservableCollection<DataSourceBase> DataSources
        {
            get
            {
                if (_dataSources == null)
                    _dataSources = new ObservableCollection<DataSourceBase>();
                return _dataSources;
            }
        }

        public static void LoadDataSource(Type dataSourceType)
        {
            LoadDataSource(dataSourceType, false);
        }

        public static void LoadDataSource(Type dataSourceType, bool autoStart)
        {
            try
            {
                DataSourceBase ds = Activator.CreateInstance(dataSourceType) as DataSourceBase;

                if (ds != null)
                {
                    DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
                    {
                        DataSources.Add(ds);
                    }));
                    ds.DataReceived = new Action<string, object>(SendData);
                    ds.DataPointReceived = new Action<DataPoint>(SendData);
                    ds.ExceptionOccurred += DataSource_ExceptionOccurred;

                    ds.Initialize();

                    if (autoStart)
                        ds.Start();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n Error detail：{2}", "LoadDataSource", ex.Message, ex.ToString()));
            }

        }

        public static void LoadDataSource(DataSourceBase ds, bool autoStart)
        {
            if (ds != null)
            {
                DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
                {
                    DataSources.Add(ds);
                }));
                ds.DataReceived = new Action<string, object>(SendData);
                ds.DataPointReceived = new Action<DataPoint>(SendData);
                ds.ExceptionOccurred += DataSource_ExceptionOccurred;

                ds.Initialize();

                if (autoStart)
                    ds.Start();
            }
        }

        private static void DataSource_ExceptionOccurred(object sender, ExceptionOccurredEventArgs e)
        {
            if (DataSourceExceptionOccurred != null)
            {
                DataSourceExceptionOccurred(sender, e);
            }
        }


        internal static void AddService(DataService service)
        {
            try
            {
                Action action = new Action(() =>
                {
                    foreach (DataService ds in _serviceCollection.Where(i => i.IsInterrupted).ToArray())
                        _serviceCollection.Remove(ds);

                    _serviceCollection.Add(service);
                    innerServiceCollection = _serviceCollection.ToList();
                });

                action.Invoke();

                //if (_dispatcher == null)
                //    action.Invoke();
                //else
                //    _dispatcher.Invoke(action);
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\nError detail：{2}", "AddService", ex.Message, ex.ToString()));
            }

        }

        internal static void RemoveService(DataService dataService)
        {
            try
            {
                Action action = new Action(() =>
                        {
                            _serviceCollection.Remove(dataService);
                            innerServiceCollection = _serviceCollection.ToList();
                        });

                //if (_dispatcher == null)
                action.Invoke();
                //else
                //    _dispatcher.Invoke(action);
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n error detail：{2}", "RemoveService", ex.Message, ex.ToString()));
            }

        }

        internal static void SendData(string key, object data)
        {
            try
            {
                if (_recordEnable)
                {
                    DataPoint dp = new DataPoint();
                    dp.Key = key;
                    //dp.TimeStamp = DateTime.Now.Ticks;
                    dp.Value = data;
                    string mapKey = key;
                    lock (_sync)
                    {
                        if (_datamap != null)
                        {
                            if (key == "MonitoringData")
                            {
                                var tempVar = data as DataPoint;
                                if (tempVar != null)
                                {
                                    mapKey = "(" + key + ")" + tempVar.Key;
                                }
                            }
                            if (!_datamap.ContainsKey(mapKey))
                            {
                                _datamap.Add(mapKey, dp);
                            }
                            else
                            {
                                _datamap[mapKey] = dp;
                            }
                        }
                    }
                }


                foreach (DataService service in innerServiceCollection)
                {
                    if (service.IsInterrupted)
                        continue;
                    if (!service.Need(key))
                        continue;
                    try
                    {
                        service.Client.ReceiveData(key, data);
                    }
                    catch (Exception ex)
                    {
                        service.IsInterrupted = true;
                        service.Exception = ex;
                    }
                }
                //策略引擎获取实时数据          
                Task task = Task.Factory.StartNew(() =>
                {
                    RuleEngine.ReceiveData(key, data);
                }); 
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n Error detail：{2}", "SendData", ex.Message, ex.ToString()));
            }

        }


        internal static void SendData(DataPoint data)
        {
            try
            {
                if (recordData)
                {
                    lock (_sync)
                    {
                        if (!_datamap.ContainsKey(data.Key))
                        {
                            _datamap.Add(data.Key, data);
                        }
                        else
                        {
                            _datamap[data.Key] = data;
                        }
                    }
                }

                foreach (DataService service in innerServiceCollection)
                {
                    if (service.IsInterrupted)
                        continue;
                    if (!service.Need(data.Key))
                        continue;
                    try
                    {
                        service.Client.ReceiveCompositeData(data);
                    }
                    catch (Exception ex)
                    {
                        service.IsInterrupted = true;
                        service.Exception = ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:error：<-{0}->:{1} \r\n Error detail:{2}", "SendData", ex.Message, ex.ToString()));
            }
        }

        public static void InitializeCommandManagerQueue()
        {
            if (_cmdManager != null) return;
            _cmdManager = new CommandManageQueue();
            _cmdManager.Initialize();
        }
        internal static void AddCommand(Command command)
        {
            _logService.Info(string.Format("@CommandLog@SendCommand####Code={0},id={1},source={2},Target={3},CommandText={4},Description={5}", command.Code.ToString(), command.Id, command.Source, command.Target.ToString(), command.CommandText, command.Description));
            if (_cmdManager == null) return;
            _cmdManager.AddCommand(command);
        }
        internal static void ExecuteCommand(Command command)
        {
            try
            {
                if (command.Target == TargetType.ToRuleEngine && command.Code == CommandCode.UpdateStrategy)
                {
                    RuleEngine.UpdateStrategy(command);
                }

                foreach (var ds in _dataSources)
                {
                    if (command.Target == TargetType.ToDataSource || command.Target == TargetType.ToAll)//&& command.Target == ds.Name
                        ds.ExecuteCommand(command);
                }

                foreach (DataService service in innerServiceCollection.Where(i => !i.IsInterrupted))
                {
                    try
                    {
                        if (command.Target == TargetType.ToClient || command.Target == TargetType.ToAll)//&& command.Target == service.Name
                            service.Client.ReceiveCommand(command);
                    }
                    catch (Exception ex)
                    {
                        service.IsInterrupted = true;
                        service.Exception = ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error：<-{0}->:{1} \r\n Error detail：{2}", "ExecuteCommand", ex.Message, ex.ToString()));
            }
        }

        #region ServiceManager

        private static ServiceHost _host;
        private static int maxConnections = 100;
        private static TimeSpan timeout = TimeSpan.FromHours(24);
        /// <summary>
        /// 启动实时数据服务，默认端口为8888
        /// </summary>
        public static void StartService()
        {
            StartService(8888);
        }



        public static TimeSpan Timeout
        {
            get { return DataEngine.timeout; }
            set { DataEngine.timeout = value; }
        }

        public static int MaxConnections
        {
            get { return maxConnections; }
            set { maxConnections = value; }
        }

        public static void StartService(int port)
        {
            try
            {
                ILogService logService = new FileLogService(typeof(DataEngine));
                logService.Info("启动数据服务引擎...");

                // UriBuilder builder = new UriBuilder(Uri.UriSchemeNetTcp, "localhost", port, "/Data");
                UriBuilder builder = new UriBuilder(Uri.UriSchemeNetPipe, "localhost");

                _host = new ServiceHost(typeof(DataService), builder.Uri);

                ServiceThrottlingBehavior throttling = _host.Description.Behaviors.Find<ServiceThrottlingBehavior>();

                if (throttling != null)
                {
                    throttling.MaxConcurrentSessions = maxConnections;
                }
                else
                {
                    throttling = new ServiceThrottlingBehavior();
                    throttling.MaxConcurrentSessions = maxConnections;

                    _host.Description.Behaviors.Add(throttling);
                }

                //GlobalErrorBehaviorAttribute errorBehavior = _host.Description.Behaviors.Find<GlobalErrorBehaviorAttribute>();
                //if (errorBehavior == null)
                //{
                //    _host.Description.Behaviors.Add(errorBehavior);
                //}

                _host.AddServiceEndpoint(typeof(IDataService), CommunicationSettingsFactory.CreateDataServicePipeBinding(Timeout), "IDataService");
                _host.AddServiceEndpoint(typeof(IPingService), CommunicationSettingsFactory.CreateDataServicePipeBinding(Timeout), "IPingService");
                _host.Open();
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error：<-{0}->:{1} \r\n Error detail:{2}", "StartService", ex.Message, ex.ToString()));

            }

        }


        public static string GetState()
        {
            return _host.State.ToString();
        }


        public static void StopService()
        {
            try
            {
                if (_host.State != CommunicationState.Closed)
                    _host.Close();
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error：<-{0}->:{1} \r\n Error detail：{2}", "StopService", ex.Message, ex.ToString()));
            }

        }
        #endregion

        #region WebService

        private static WebServiceHost _webhost;
        //  private static Dictionary<string, DataPoint> _datamap;
        private static bool recordData = false;

        public static void StartWebService()
        {
            StartWebService(8899);
        }

        public static void StartWebService(int port)
        {
            try
            {
                recordData = true;
                _datamap = new Dictionary<string, DataPoint>();

                UriBuilder builder = new UriBuilder(Uri.UriSchemeHttp, "localhost", port, "/Web");
                _webhost = new WebServiceHost(typeof(WebDataService), builder.Uri);

                _webhost.Open();
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error:<-{0}->:{1} \r\n Error detail:{2}", "StartWebService", ex.Message, ex.ToString()));
            }

        }

        public static void StopWebService()
        {
            try
            {
                if (_webhost.State != CommunicationState.Closed)
                    _webhost.Close();
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error：<-{0}->:{1} \r\n Error detail:{2}", "StartWebService", ex.Message, ex.ToString()));
            }

        }

        public static DataPoint GetData(string identity)
        {
            if (_datamap != null && _datamap.ContainsKey(identity))
                return _datamap[identity];
            else
                return new DataPoint();
        }

        public static List<DataPoint> GetDataList(string[] identities)
        {
            if (_datamap != null)
            {
                List<DataPoint> pts = new List<DataPoint>();

                foreach (string id in identities)
                {
                    if (_datamap.ContainsKey(id))
                        pts.Add(_datamap[id]);
                }

                return pts;
            }
            else
                return null;
        }

        public static DataPoint[] GetAllData()
        {
            if (_datamap != null)
                return _datamap.Values.ToArray();
            else
                return null;
        }

        #endregion

        public static void InitializeRuleEngine()
        {
            try
            {
                string strategyTableStr;
                DateTime time;
                if (MonitorDataAccessor.Instance().IsOpenDbResult)
                {
                    MonitorDataAccessor.Instance().GetStrategy(out strategyTableStr, out time);
                    RuleEngine.UpdateStrategy(strategyTableStr);
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:Error:<-{0}->:{1} \r\n Error detail:{2}", "InitializeRuleEngine", ex.Message, ex.ToString()));

            }

        }

    }
}
