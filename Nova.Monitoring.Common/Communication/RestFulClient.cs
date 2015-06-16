using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Monitoring.Common
{
    public class RestFulClient
    {
        private static readonly object locker = new object();
        private static RestFulClient _restFulClient;
        private RestClient _client;
        private RestFulClient()
        {

        }
        public static RestFulClient Instance
        {
            get
            {
                if (_restFulClient == null)
                {
                    lock (locker)
                    {
                        if (_restFulClient == null)
                        {
                            _restFulClient = new RestFulClient();
                        }
                    }
                }
                return _restFulClient;
            }
        }

        //  public bool IsInitialized  { get; set; }

        public void Initialize(string baseUrl)
        {
            _client = new RestClient(string.Format("http://{0}", baseUrl));
            _client.Timeout = 60 * 1000;
        }

        public void Post(string resource, object body, Action<IRestResponse> responseHandler)
        {
            if (_client == null)
                return;

            var request = new RestRequest(resource, Method.POST);
            //request.Timeout = 60;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("EnablingCompression", "1");
            request.AddBody(body);
            _client.ExecuteAsync(request, response =>
            {
                if (responseHandler != null && response != null)
                    responseHandler.BeginInvoke(response, null, null);
            });

        }

        public string Post(string resource, object body)
        {
            if (_client == null)
                throw new TypeInitializationException("RestClient", new Exception());

            var request = new RestRequest(resource, Method.POST);
           // request.Timeout = 60;
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(body);
            var response = _client.Execute(request);
            var content = response.Content;
            return content;
        }

    }
}
