using DemoQA.HttpClients;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Serializers.NewtonsoftJson;
using DemoQA.Core;
using DemoQA.Core.Models.Account.ResponseModels;

namespace DemoQA.API.Tests
{
    [TestFixture]
    public class AccountTests
    {
        private UnAuthenticatedRestClient _unAuthenticatedRestClient;

        [OneTimeSetUp]
        public void Init()
        {
            string baseUrl = "https://www.demoqa.com/";
            _unAuthenticatedRestClient = new UnAuthenticatedRestClient(baseUrl);
        }

        [SetUp]
        public async Task Setup()
        {
            await Creation_Of_Users_Should_Be_Successfull_With_Right_Status_Code();
        }

        public async Task Creation_Of_Users_Should_Be_Successfull_With_Right_Status_Code()
        {
            var request = new RestRequest("/Account/v1/User", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov123",
                Password = "Pasword1!",
            });

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<GenerateTokenResponseModel>(request);
        }

        [Test]
        public async Task User_Should_Be_Able_To_Generate_Token_Successfully()
        {
            var request = new RestRequest("Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov123",
                Password = "Pasword1!",
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<GenerateTokenResponseModel>(request);

            Assert.True(!string.IsNullOrEmpty(response.Token));
        }

        [Test]
        public async Task User_Should_Be_Able_To_Authorize_Successfully()
        {
            var request = new RestRequest("/Account/v1/Authorized", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov123",
                Password = "Pasword1!",
            });

            var
                response = await _unAuthenticatedRestClient.Client.ExecuteAsync(request);

            Assert.AreEqual("true", response.Content);
        }
    }
}