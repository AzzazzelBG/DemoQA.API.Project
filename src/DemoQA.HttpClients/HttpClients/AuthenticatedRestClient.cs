using DemoQA.Core;
using DemoQA.Core.Models.Account.ResponseModels;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using System.Threading.Tasks;

namespace DemoQA.Http.HttpClients
{
    public class AuthenticatedRestClient
    {
        private readonly string _baseUrl;
        private readonly IRestClient _internalClient;

        public AuthenticatedRestClient(string url)
        {
            _baseUrl = url;
            _internalClient = new RestClient(_baseUrl);
        }

        public async Task<IRestClient> Create(string username, string password)
        {
            RestRequest tempRequest = new RestRequest("Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            tempRequest.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password,
            });

            GenerateTokenResponseModel tempResponse = await _internalClient.PostAsync<GenerateTokenResponseModel>(tempRequest);

            RestClient client = new RestClient(_baseUrl)
            {
                Authenticator = new JwtAuthenticator(tempResponse.Token),
                UserAgent = "Demo QA Test Agent",
            };

            return client;
        }
    }
}
