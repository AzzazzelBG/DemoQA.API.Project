using RestSharp;
using System;

namespace DemoQA.HttpClients
{
    public class UnAuthenticatedRestClient
    {
        private IRestClient _client;

        public UnAuthenticatedRestClient(string url)
        {
            _client = new RestClient(url)
            {
                UserAgent = "Demo QA Test Agent",
            };
        }

        public IRestClient Client => _client;
    }
}
