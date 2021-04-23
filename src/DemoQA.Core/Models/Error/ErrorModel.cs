using Newtonsoft.Json;

namespace DemoQA.Core.Models.Error
{
    public class ErrorModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
