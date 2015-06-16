using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataSource
{
    public abstract class DataSourceBase : IDataSource, IDisposable
    {
        public delegate void DataSourceStateChangedEventHandler(object sender, DataSourceState state);
        public delegate void ExecuteCommandEventHandler(object sender, Command cmd);

        public event DataSourceStateChangedEventHandler StateChanged;
        public event ExecuteCommandEventHandler CommandExecuted;
        public event EventHandler<ExceptionOccurredEventArgs> ExceptionOccurred;

        private DataSourceState _state;
        private string _name;
        private Exception _exception;


        public DataSourceBase()
        {
            State = DataSourceState.New;
        }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return this.GetType().Name;
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// 数据源状态
        /// </summary>
        public DataSourceState State
        {
            get { return _state; }
            protected set
            {
                if (_state == DataSourceState.Error && value != DataSourceState.Initializing)
                    return;
                if (value == _state)
                    return;

                _state = value;
                if (StateChanged != null)
                {
                    StateChanged(this, _state);
                }
            }
        }


        protected virtual void OnInitialize() { }

        protected virtual void OnStart() { }

        protected virtual void OnStop() { }

        protected virtual void OnPause() { }

        protected virtual void OnResume() { }

        protected virtual void OnDispose() { }

        protected virtual void OnExecuteCommand(Command cmd) { }

        public void Initialize()
        {
            State = DataSourceState.Initializing;
            try
            {
                OnInitialize();
                State = DataSourceState.Initialized;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnInitialize() got a fatal error.", ex);
            }
        }

        public void Start()
        {
            State = DataSourceState.Starting;
            try
            {
                OnStart();
                State = DataSourceState.Running;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnStart() got a fatal error.", ex);

            }
        }

        public void Stop()
        {
            State = DataSourceState.Stopping;
            try
            {
                OnStop();
                State = DataSourceState.Stopped;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnStop() got a fatal error.", ex);
            }
        }

        public void Pause()
        {
            State = DataSourceState.Pausing;
            try
            {
                OnPause();
                State = DataSourceState.Paused;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnPause() got a fatal error.", ex);
            }
        }

        public void Resume()
        {
            State = DataSourceState.Resuming;
            try
            {
                OnResume();
                State = DataSourceState.Running;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnResume() got a fatal error.", ex);
            }
        }

        public void ExecuteCommand(Command cmd)
        {
            try
            {
                OnExecuteCommand(cmd);

                if (CommandExecuted != null)
                    CommandExecuted(this, cmd);
            }
            catch (Exception ex)
            {
                _exception = new Exception("OnExecuteCommand() got a error.", ex);
            }

        }

        public Exception MonitorException
        {
            get { return _exception; }
            set
            {
                _exception = value;
                if (ExceptionOccurred != null)
                {
                    ExceptionOccurred(this, new ExceptionOccurredEventArgs(this.Name, _exception));
                }
            }
        }



        #region SendData

        public Action<DataPoint> DataPointReceived;
        public Action<string, object> DataReceived;

        public void SendData(DataPoint data)
        {
            if (DataPointReceived != null)
                DataPointReceived(data);
        }

        public void SendData(string identity, object data)
        {
            if (DataReceived != null)
                DataReceived(identity, data);
        }

        #endregion


        #region IDispose

        public void Dispose()
        {
            State = DataSourceState.Disposing;
            try
            {
                OnDispose();
                State = DataSourceState.Disposed;
            }
            catch (Exception ex)
            {
                State = DataSourceState.Error;
                _exception = new Exception("OnDispose() got a fatal error.", ex);
            }
        }

        #endregion





    }


}
