using DemoQA.HttpClients;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Serializers.NewtonsoftJson;
using DemoQA.Core;
using DemoQA.Core.Models.Account.ResponseModels;
using DemoQA.Core.Models.Error;

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

        [OneTimeSetUp]
        public async Task Setup()
        {
            await Creation_Of_User();
        }

        public async Task Creation_Of_User()
        {
            var request = new RestRequest("/Account/v1/User", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov1234",
                Password = "Pasword1!",
            });

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<GenerateTokenResponseModel>(request);
        }

        #region GenerateToken POST Endpoint Tests

        [Test]
        public async Task User_Should_Be_Able_To_Generate_Token_Successfully()
        {
            var request = new RestRequest("Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov1234",
                Password = "Pasword1!",
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<GenerateTokenResponseModel>(request);

            Assert.True(!string.IsNullOrEmpty(response.Token));
        }

        [Test]
        public async Task User_Cannot_Generate_Token_With_Empty_Values()
        {
            var request = new RestRequest("/Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "",
                Password = "Pasword1!",
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<ErrorModel>(request);

            Assert.AreEqual("UserName and Password required.", response.Message);
        }

        #endregion

        #region Authorized POST Endpoint tests

        [Test]
        public async Task User_Should_Be_Able_To_Authorize_Successfully()
        {
            var request = new RestRequest("/Account/v1/Authorized", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = "PetarPetrov1234",
                Password = "Pasword1!",
            });

            var
                response = await _unAuthenticatedRestClient.Client.ExecuteAsync(request);

            Assert.AreEqual("true", response.Content);
        }

        #endregion
    }
}