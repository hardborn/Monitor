using Log4NetLibrary;
using Newtonsoft.Json;
using Nova.Monitoring.Common;
using Nova.Monitoring.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Monitoring.DataDispatcher
{
    public class WebDispatcher : IDataDispatcher, IDisposable
    {
        private static object _syncObj = new object();
        private static WebDispatcher _instance;
        private RTClient _webDataClient;
        private string _host;
        private int _port;
        private Timer _heartbeatTimer;
        private Queue<Command> _commandQueue = new Queue<Command>();
        private object _obj = new object();
        private Command _currentCommand;
        private bool _commandReturn = false;
        private ILogService _logService;

        public static WebDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebDispatcher();
                        }
                    }
                }
                return _instance;
            }
        }

        public WebDispatcher()
        {
            //_dataClient.Connect();
            _logService = new FileLogService(typeof(WebDispatcher));
        }

        private string _macAddress;
        public string MacAddress
        {
            get
            { return _macAddress; }
            set
            {
                _macAddress = value;
            }
        }


        public void Initialize(string host, int port)
        {
            _host = host;
            _port = port;
            try
            {
                _webDataClient = new RTClient();
                _webDataClient.Connect(_host, _port);
                _webDataClient.Login("WebDispatcher");
                _webDataClient.DataReceived += DataClient_DataReceived;
               // _webDataClient.CommandReceived += DataClient_CommandReceived;
                _webDataClient.Register(new string[] { "*" });
                _macAddress = SystemHelper.GetMACAddress(string.Empty);
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "Initialize", ex.ToString()));
            }
        }

        //private void DataClient_CommandReceived(Command command)
        //{
        //}

        private void DataClient_DataReceived(string identity, object data)
        {
            Action action = new Action(() =>
            {
                ProcessMonitoringData(identity, data);
            });
            action.BeginInvoke(null,null);
        }

        private void ProcessMonitoringData(string identity, object data)
        {
            try
            {
                if (identity.Equals("ScreenBaseInfoList", StringComparison.OrdinalIgnoreCase))
                {
                }
                else if (identity.Equals("AllPhysicalDisplayInfo", StringComparison.OrdinalIgnoreCase))
                {
                }
                else if (identity.Equals("MonitoringData", StringComparison.OrdinalIgnoreCase))
                {

                    var ledData = data as DataPoint;

                    if (ledData == null)
                    {
                        System.Diagnostics.Debug.WriteLine("监控数据格式错误！！");
                        return;
                    }

                    var packetData = new WebPacketData();
                    packetData.Identifier = String.Format("{0}+{1}", _macAddress, ledData.Key);
                    packetData.IdentifierClass = PacketDataType.MonitoringData;
                    packetData.SequenceNumber = string.Empty;
                    packetData.Timestamp = DateTime.UtcNow.Ticks.ToString();

                    List<DataPoint> dataPointCollection = new List<DataPoint>();
                    if (typeof(List<DataPoint>) == ledData.Value.GetType())
                    {
                        dataPointCollection = ledData.Value as List<DataPoint>;
                    }
                    else if (typeof(DataPoint) == ledData.Value.GetType())
                    {
                        dataPointCollection = new List<DataPoint>() { ledData.Value as DataPoint };
                    }

                    string json = JsonConvert.SerializeObject(dataPointCollection);
                    packetData.DataPointCollection = Nova.Zip.GZipCompression.Compress(json);
                    //packetData.DataPointCollection = dataPointCollection;
                    RestFulClient.Instance.Post("api/index/monitorData", packetData, response =>
                    {
                        MonitorDataResponse(response);
                    });
                }
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "ProcessMonitoringData", ex.ToString()));
            }
            
        }

        private void MonitorDataResponse(IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                switch (response.Content)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        break;
                    default:
                        try
                        {
                            var commandList = JsonConvert.DeserializeObject<List<Command>>(response.Content);
                            if (commandList == null)
                                return;
                            lock (_commandQueue)
                            {
                                foreach (var command in commandList)
                                {
                                    if (!_commandQueue.Contains(command))
                                    {
                                        _commandQueue.Enqueue(command);
                                    }
                                }
                            }

                            Queue<Command> commandQueue = new Queue<Command>();

                            foreach (var item in _commandQueue)
                            {
                                commandQueue.Enqueue(item.Clone() as Command);
                            }


                            Thread processCommandThread = new Thread(new ParameterizedThreadStart(t => ProcessCommand(t)));
                            {
                                processCommandThread.Name = "CommandThread";
                                processCommandThread.IsBackground = true;
                                processCommandThread.Priority = ThreadPriority.BelowNormal;
                            }
                            processCommandThread.Start(commandQueue);


                        }
                        catch (JsonReaderException ex)
                        {
                            _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "MonitorDataResponse", ex.ToString()));
                        }
                        catch (Exception ex)
                        {
                            _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "MonitorDataResponse", ex.ToString()));
                        }
                        break;
                }
            }
            else
            {

            }
        }

        private void ProcessCommand(object commandList)
        {
            Queue<Command> commands = commandList as Queue<Command>;
            while (commands.Count > 0)
            {
                _webDataClient.SendCommand(commands.Dequeue());
            }
        }

        public void Dispose()
        {
            if (_webDataClient != null)
            {
                _webDataClient.Dispose();
            }
        }
    }
}
