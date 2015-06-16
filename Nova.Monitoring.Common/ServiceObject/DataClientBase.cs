using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;

namespace Nova.Monitoring.Common
{
    public abstract class DataClientBase : IDataClient, IDataService, INotifyPropertyChanged, IDisposable
    {
        private IDataService _dataService;

        private string _name;
        private string[] _keysRegistered;
        private Exception _exception;

        public string[] KeysRegistered
        {
            get
            {
                return _keysRegistered;
            }
            set
            {
                _keysRegistered = value;
                OnPropertyChanged("KeysRegistered");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public DataClientState State
        {
            get
            {
                return GetState();
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
        }
        private bool _isLogin = false;

        public bool IsRegister
        {
            get
            {
                return _isRegister;
            }
        }
        private bool _isRegister = false;

        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                OnPropertyChanged("Exception");
            }
        }

        public DataClientBase()
        {
            if (AppEnvionment.Current.ServiceProxy == null)
            {
                AppEnvionment.Current.ServiceProxy = this;
            }
        }
        /// <summary>
        /// 连接本地指定的服务。
        /// </summary>
        /// <param name="serviceType">指定服务的类型。</param>
        public void Connect()
        {
            Connect("localhost");
        }

        private void Connect(string host)
        {
            Connect(host, 8888);
        }
        public void Connect(string host, int port)
        {
            try
            {
                _dataService = DataClientFactory.CreateDataServicePipeProxy(this, host);

                //bool result = false;
                //result = _dataService.Hello();

                //if (!result)
                //{
                //    System.Diagnostics.Debug.WriteLine("++++++++++++++++ Hello Failure +++++++++++++++++");
                //}

                if (!string.IsNullOrEmpty(Name))
                {
                    _dataService.Login(Name);
                }
                if (_keysRegistered != null && KeysRegistered.Length > 0)
                {
                    _dataService.Register(_keysRegistered);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Connect Error:" + host + ":" + port.ToString()+ex.ToString());
            }
        }

        /// <summary>
        /// 断开所有通讯通道。
        /// </summary>
        public void Disconnect()
        {
            var channel = _dataService as IClientChannel;
            try
            {
                if (channel != null)
                {
                    channel.Close();
                }
            }
            catch (Exception)
            {
                channel.Abort();
            }
        }

        public DataClientState GetState()
        {
            //return DataClientState.Opened;

            var channel = _dataService as IClientChannel;
            if (channel == null)
            {
                return DataClientState.Initialize;
            }
            switch (channel.State)
            {
                case CommunicationState.Created:
                    return DataClientState.Created;
                case CommunicationState.Opening:
                    return DataClientState.Opening;
                case CommunicationState.Opened:
                    return DataClientState.Opened;
                case CommunicationState.Closing:
                    return DataClientState.Closing;
                case CommunicationState.Closed:
                    return DataClientState.Closed;
                case CommunicationState.Faulted:
                    return DataClientState.Faulted;
            }

            return DataClientState.Unknown;
        }

        public void Register(string[] keys)
        {
            _isRegister = false;
            bool changed = false;
            if (keys == null || keys.Length == 0)
            {
                throw new ArgumentNullException("keys");
            }
            if (_keysRegistered == null || _keysRegistered.Length == 0)
            {
                _keysRegistered = keys;
                changed = true;
            }
            else
            {
                var temp = _keysRegistered.Union(keys).ToArray();

                if (temp.Length > _keysRegistered.Length)
                {
                    _keysRegistered = temp;
                    changed = true;
                }
            }

            if (changed)
            {
                try
                {
                    _dataService.Register(_keysRegistered);
                    _isRegister=true;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Register Error:"+ex.ToString());
                }
            }
        }

        public void Login(string name)
        {
            try
            {
                _dataService.Login(name);
                _name = name;
                _isLogin = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login Error:" + ex.ToString());
                _isLogin = false;
            }
        }

        public void Logout()
        {
            try
            {
                _dataService.Logout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Logout Error:" + ex.ToString());
            }
        }

        public void SendCommand(Command command)
        {
            //ReceiveCommand(command);
            if (this.State == DataClientState.Closed || this.State == DataClientState.Closing || this.State == DataClientState.Unknown)
            {
                System.Diagnostics.Debug.WriteLine("SendCommand Error State:" + this.State);
                return;
            }
            _dataService.SendCommand(command);
        }

        public void SendData(string key, object data)
        {
            _dataService.SendData(key, data);
        }

        public void SendCompositeData(DataPoint dp)
        {
            _dataService.SendCompositeData(dp);
        }

        public bool Hello()
        {
            bool result;
            try
            {
                result = _dataService.Hello();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hello Error："+ex.ToString());
               result =false;
            }
            return result;
        }

        public virtual void ReceiveData(string identity, object data)
        {
        }
        public virtual void ReceiveCompositeData(DataPoint data)
        {
        }
        public virtual void ReceiveCommand(Command command)
        {
        }

        public virtual void ReceiveDataBuffer(byte[] buffer)
        {
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public virtual void Dispose()
        {
            if (_dataService != null)
            {
                ((IClientChannel)_dataService).Dispose();
                _dataService = null;
            }
        }
    }
}
