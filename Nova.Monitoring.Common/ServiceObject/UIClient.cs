using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class UIClient : DataClientBase, IDisposable
    {
        public delegate void DataReceivedHandler(string identity, object data);
        public delegate void CommandReceivedHandler(Command command);
        public event DataReceivedHandler DataReceived;
        public event CommandReceivedHandler CommandReceived;
        public UIClient()
            : base()
        {
            AppEnvionment.Current.ServiceProxy = this;
        }

        public UIClient(string name)
            : this()
        {
            Name = name;
        }

        public override void ReceiveData(string identity, object data)
        {
            base.ReceiveData(identity, data);
            if (DataReceived != null)
            {
                DataReceived(identity, data);
            }
        }

        public override void ReceiveCommand(Command command)
        {
            base.ReceiveCommand(command);
            if (CommandReceived != null)
            {
                CommandReceived(command);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
