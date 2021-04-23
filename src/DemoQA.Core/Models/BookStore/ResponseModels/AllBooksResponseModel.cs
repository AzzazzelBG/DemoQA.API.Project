using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoQA.Core.Models.BookStore.ResponseModels
{
    public class AllBooksResponseModel
    {
        [JsonProperty("books")]
        public IList<BookResponseModel> Books { get; set; }
    }
}
