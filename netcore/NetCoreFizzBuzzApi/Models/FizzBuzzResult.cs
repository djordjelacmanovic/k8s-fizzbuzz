using System.Text.Json.Serialization;

namespace NetCoreFizzBuzzApi.Models {
    public class FizzBuzzResult {

        [JsonPropertyName("value")]
        public string Value { get; set; }
        
        [JsonPropertyName("app_name")]
        public string AppName { get; set; }

        [JsonPropertyName("app_version")]
        public string AppVersion { get; set; }
    }
}