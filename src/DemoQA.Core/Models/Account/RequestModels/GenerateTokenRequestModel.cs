using Newtonsoft.Json;

namespace DemoQA.Core
{
    public class GenerateTokenRequestModel
    {
        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
