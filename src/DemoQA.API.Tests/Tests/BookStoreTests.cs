using DemoQA.Core.Models.BookStore.ResponseModels;
using DemoQA.Core.Models.Error;
using DemoQA.HttpClients;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.API.Tests
{
    public class BookStoreTests
    {
        private UnAuthenticatedRestClient _unAuthenticatedRestClient;

        [OneTimeSetUp]
        public void Init()
        {
            string baseUrl = "https://www.demoqa.com/";
            _unAuthenticatedRestClient = new UnAuthenticatedRestClient(baseUrl);
        }

        #region Books GET Endpoint Tests
        [Test]
        public async Task User_Can_See_All_Books_In_The_Book_Store()
        {
            var request = new RestRequest("/BookStore/v1/Books", Method.GET, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<AllBooksResponseModel>(request);

            Assert.True(response.Data.Books.Count == 8);
        }

        [Test]
        public async Task User_Should_Receive_Proper_Status_Code_When_Access_All_Books()
        {
            var request = new RestRequest("/BookStore/v1/Books", Method.GET, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<AllBooksResponseModel>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Book GET Endpoint Tests

        [Test]
        public async Task User_Can_Access_Specific_Book_By_Isbn()
        {
            var request = new RestRequest("/BookStore/v1/Book", Method.GET, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddQueryParameter("ISBN", "9781449325862");

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<BookResponseModel>(request);

            Assert.AreEqual("9781449325862", response.Data.Isbn);
        }

        [Test]
        public async Task User_Receives_Proper_Error_Message_If_Isbn_Is_Incorrect()
        {
            var request = new RestRequest("/BookStore/v1/Book", Method.GET, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddQueryParameter("ISBN", "9781449325");

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<ErrorModel>(request);

            Assert.AreEqual("ISBN supplied is not available in Books Collection!", response.Data.Message);
        }

        [TestCase("9781449325862", HttpStatusCode.OK)]
        [TestCase("97814493262", HttpStatusCode.BadRequest)]
        public async Task User_Receives_Proper_Status_Code_After_Request(string isbn, HttpStatusCode httpStatusCode)
        {
            var request = new RestRequest("/BookStore/v1/Book", Method.GET, DataFormat.Json)
            {
                JsonSerializer = new JsonNetSerializer()
            };

            request.AddQueryParameter("ISBN", isbn);

            var response = await _unAuthenticatedRestClient.Client.ExecuteAsync<ErrorModel>(request);

            Assert.AreEqual(httpStatusCode, response.StatusCode);
        }

        #endregion

    }
}
