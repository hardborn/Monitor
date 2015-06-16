using Nova.Monitoring.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataSource
{
    public interface IDataSource
    {
        DataSourceState State { get; }
        Exception MonitorException { get; }
        void Initialize();
        void Start();
        void Stop();
        void Pause();
        void Resume();
        void ExecuteCommand(Command cmd);
    }

    public enum DataSourceState : byte
    {
        Disposed,
        Disposing,
        Error,
        Initialized,
        Initializing,
        New,
        Paused,
        Pausing,
        Resuming,
        Running,
        Starting,
        Stopped,
        Stopping,
        Unknown
    }
}
