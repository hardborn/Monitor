using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nova.Monitoring.Common
{
    public class DataManageQueue : IDisposable
    {
        private Thread _thread;
        private ConcurrentQueue<object> _commandQueue;
        private AutoResetEvent _resetEvent;
        public void AddCommand(Command cmd)
        {
            if (cmd != null)
            {
                _commandQueue.Enqueue(cmd);
                _resetEvent.Set();
            }
        }
        public void Initialize()
        {
            if (_thread != null) return;
            _commandQueue = new ConcurrentQueue<object>();
            _thread = new Thread(Function);
            _thread.IsBackground = true;
            _thread.Start();
        }
        private void Function()
        {
            _resetEvent = new AutoResetEvent(false);
            object cmd;
            while (true)
            {
                if (_commandQueue.Count != 0)
                {
                    if (_commandQueue.TryDequeue(out cmd))
                    {
                        if (cmd != null)
                        {
                            CommandExecute(cmd);
                            /*TODO:执行命令
                             * 命令执行完成后释放信号量 _isExecuting = 0
                             */
                        }
                    }
                }
                else _resetEvent.WaitOne();
            }
        }
        protected virtual void CommandExecute(object o)
        { }
        public void Dispose()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
            _thread = null;
        }
    }
}
