using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.DataDispatcher
{
    public abstract class Handler
    {
        protected ClientDispatcher _client;

        public Handler(ClientDispatcher client)
        {
            this._client = client;
        }

        public virtual void Execute(string key,object data)
        {

        }
    }
}
