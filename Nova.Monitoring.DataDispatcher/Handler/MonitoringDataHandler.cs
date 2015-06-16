using Log4NetLibrary;
using Newtonsoft.Json;
using Nova.Monitoring.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Monitoring.DataDispatcher
{
    public class MonitoringDataHandler : Handler
    {
        private ClientDispatcher _client;
        private Queue<Command> _commandQueue = new Queue<Command>();
        private ILogService _logService;
        public MonitoringDataHandler(ClientDispatcher client)
            : base(client)
        {
            _client = client;
            _logService = new FileLogService(typeof(MonitoringDataHandler));
        }

        public override void Execute(string key, object data)
        {
            var ledData = data as DataPoint;

            if (ledData == null)
            {
                System.Diagnostics.Debug.WriteLine("监控数据格式错误！！");
                return;
            }

            var packetData = new WebPacketData();
            packetData.Identifier = _client.GetScreenId(AppDataConfig.CurrentMAC, ledData.Key);
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

            var brightnessDataPoint = dataPointCollection.FirstOrDefault(p => p.Key.Split(new char[] { '|' })[0] == "1" && p.Key.Split(new char[] { '|' })[1] == "3");
            if (brightnessDataPoint != null)
            {
                var action = new Action(() =>
                {
                    _client.NotifyBrightnessValueRefreshed(ledData.Key, (string)brightnessDataPoint.Value);
                });
                action.BeginInvoke(null, null);
            }

            string json = JsonConvert.SerializeObject(dataPointCollection);

            packetData.DataPointCollection = Nova.Zip.GZipCompression.Compress(json);
            //packetData.DataPointCollection = dataPointCollection;
            RestFulClient.Instance.Post("api/index/monitorData", packetData, response =>
            {
                MonitorDataResponse(response);
            });
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
                            _logService.Error(string.Format("ExistCatch:<-{0}->:{1}", "MonitorDataResponse.Json", ex.ToString()));
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
                _client.SendCommand(commands.Dequeue());
            }
        }
    }
}
