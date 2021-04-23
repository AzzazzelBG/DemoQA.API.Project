using DemoQA.Core.Models.BookStore.ResponseModels;
using DemoQA.HttpClients;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
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
    }
}
