using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoQA.Core.Models.Account.ResponseModels
{
    public class GenerateTokenResponseModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires")]
        public DateTime Expires { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
