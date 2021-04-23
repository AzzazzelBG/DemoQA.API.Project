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
                Username = "GeorgiGeorgiev",
                Password = "Pasword11!",
            });

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<GenerateTokenResponseModel>(request);
        }

        #region GenerateToken POST Endpoint Tests

        [TestCase("GeorgiGeorgiev", "Pasword11!"), Order(1)]
        public async Task User_Should_Be_Able_To_Generate_Token_Successfully(string username, string password)
        {
            var request = new RestRequest("Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password,
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<GenerateTokenResponseModel>(request);

            Assert.True(!string.IsNullOrEmpty(response.Token));
        }

        [Test, Order(2)]
        [TestCase("", "Pasword11!", "UserName and Password required.")]
        [TestCase("GeorgiGeorgiev", "", "UserName and Password required.")]
        public async Task User_Cannot_Generate_Token_With_Empty_Values(string username, string password, string errorMessage)
        {
            var request = new RestRequest("/Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password,
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<ErrorModel>(request);

            Assert.AreEqual(errorMessage, response.Message);
        }

        // This test fails intentionally!
        // When the user is not found it should not return success code and model!
        [Test, Order(3)]
        [TestCase("", "Pasword11!", HttpStatusCode.NotFound)]
        [TestCase("GeorgiGeorgiev", "", HttpStatusCode.NotFound)]
        public async Task User_Cannot_Generate_With_Incorrect_Username_Or_Password_Proper_Error_Code_Appears(string username, string password, HttpStatusCode httpStatusCode)
        {
            var request = new RestRequest("/Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password
            });

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync(request);

            Assert.True(response.StatusCode == httpStatusCode);
        }

        // This test fails intentionally!
        // When the user is not found it should not return success code and model!
        [Test, Order(4)]
        [TestCase("AlaBalaTest", "Pasword1!", "User not found!")]
        [TestCase("GeorgiGeorgiev", "AlaBalaTest", "User not found!")]
        public async Task User_Cannot_Generate_With_Incorrect_Username_Or_Password_Proper_Error_Message_Appears(string username, string password, string errorMessage)
        {
            var request = new RestRequest("/Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password
            });

            var response = await _unAuthenticatedRestClient.Client.PostAsync<ErrorModel>(request);

            Assert.AreEqual(errorMessage, response.Message);
        }

        [Test, Order(5)]
        [TestCase("", "Pasword1!")]
        [TestCase("GeorgiGeorgiev", "")]
        public async Task User_Cannot_Generate_With_Empty_Values_Proper_Status_Code_Appears(string username, string password)
        {
            var request = new RestRequest("/Account/v1/GenerateToken", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password,
            });

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region Authorized POST Endpoint tests

        [Test, Order(6)]
        [TestCase("GeorgiGeorgiev", "Pasword11!")]
        public async Task User_Should_Be_Able_To_Authorize_Successfully(string username, string password)
        {
            var request = new RestRequest("/Account/v1/Authorized", Method.POST, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddJsonBody(new GenerateTokenRequestModel()
            {
                Username = username,
                Password = password,
            });

            var
                response = await _unAuthenticatedRestClient.Client.ExecuteAsync(request);

            Assert.AreEqual("true", response.Content);
        }

        #endregion
    }
}