

using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Nova.Monitoring.Engine
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, 
        ConcurrencyMode = ConcurrencyMode.Reentrant,
        UseSynchronizationContext = false,
        MaxItemsInObjectGraph = 1310720000, 
        IncludeExceptionDetailInFaults = true)]
    //[GlobalErrorBehaviorAttribute(typeof(GlobalErrorHandler))]
    public class DataService : IDataService, IPingService
    {
        private Dictionary<string, string> _currentKeys;
        private IDataClient _client;

        private bool isInterrupted = false;
        private bool registerAll = true;
        private bool registered = false;
        private string _name ;
        private Exception _exception;
        private Log4NetLibrary.ILogService _logServer = new Log4NetLibrary.FileLogService(typeof(DataService));

        private object sync = new object();
        public DataService()
        {
            _client = OperationContext.Current.GetCallbackChannel<IDataClient>();

            MessageProperties properties = OperationContext.Current.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口   
            //RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //ip = string.Format("{0}:{1}", endpoint.Address, endpoint.Port);

            OperationContext.Current.Channel.Closed += new EventHandler(Channel_Closed);
            // OperationContext.Current.Channel.Faulted += Channel_Faulted;
            DataEngine.AddService(this);
        }

        void Channel_Faulted(object sender, EventArgs e)
        {
            _logServer.Debug("Channel_Faulted.");
            var state = OperationContext.Current.Channel.State;
        }

        public bool IsInterrupted
        {
            get { return isInterrupted; }
            set
            {
                isInterrupted = value;
            }
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return string.Empty;
                return _name;
            }
        }

        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
            }
        }

        private string ip;

        public string IP
        {
            get { return ip; }
        }


        public IDataClient Client
        {
            get { return _client; }
        }
        public bool Need(string readkey)
        {
            if (!registered)
                return false;

            if (registerAll)
                return true;

            lock (sync)
            {
                if (_currentKeys.ContainsKey(readkey))
                    return true;
            }
            return false;
        }

        private void Channel_Closed(object sender, EventArgs e)
        {
            _logServer.Debug("Channel_Closed." + this.Name);
            IsInterrupted = true;
        }

        #region IDataService

        public void Register(string[] keys)
        {
            lock (sync)
            {
                if (keys != null && keys.Length > 0)
                    registered = true;

                registerAll = false;

                _currentKeys = new Dictionary<string, string>();

                foreach (string key in keys)
                {
                    if (key == "*")
                    {
                        registerAll = true;
                        break;
                    }
                    else
                    {
                        if (!_currentKeys.ContainsKey(key))
                            _currentKeys.Add(key, key);
                    }
                }
            }
        }

        public void Login(string name)
        {
            // throw new NotImplementedException();
            _name = name;
        }

        public void Logout()
        {
            //DataEngine.RemoveService(this);
        }

        public void SendCommand(Command command)
        {
            DataEngine.AddCommand(command);
        }

        public void SendData(string key, object data)
        {
            DataEngine.SendData(key, data);
        }

        public void SendCompositeData(DataPoint dp)
        {
            //if (dp.TimeStamp == 0)
            //    dp.TimeStamp = DateTime.Now.Ticks;
            DataEngine.SendData(dp);
        }

        public bool Hello()
        {
            return true;
        }

        #endregion


        public bool Ping()
        {
            return true;
        }


    }


}
