using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nova.Monitoring.Utilities
{
    public class ThreadInvoker
    {
        private static readonly object padlock = new object();
        private static readonly object locker = new object();
        private static ThreadInvoker _threadInvoker;

        private ThreadInvoker()
        {

        }
        public static ThreadInvoker Instance
        {
            get
            {
                if (_threadInvoker == null)
                {
                    lock (locker)
                    {
                        if (_threadInvoker == null)
                        {
                            _threadInvoker = new ThreadInvoker();
                        }
                    }
                }
                return _threadInvoker;
            }
        }

        public void RunByNewThread(Action action)
        {
            lock (padlock)
            {
                action.BeginInvoke(ar => ActionCompleted(ar, res => action.EndInvoke(res)), null);
            }
        }

        public void RunByNewThread<TResult>(Func<TResult> func, Action<TResult> callbackAction)
        {
            lock (padlock)
            {
                func.BeginInvoke(ar => FuncCompleted<TResult>(ar, res => func.EndInvoke(res), callbackAction), null);
            }
        }

        private static void ActionCompleted(IAsyncResult asyncResult, Action<IAsyncResult> endInvoke)
        {
            if (asyncResult.IsCompleted)
            {
                endInvoke(asyncResult);
            }
        }

        private static void FuncCompleted<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endInvoke, Action<TResult> callbackAction)
        {
            if (asyncResult.IsCompleted)
            {
                TResult response = endInvoke(asyncResult);
                if (callbackAction != null)
                {
                    callbackAction(response);
                }
            }
        }

    }
}
