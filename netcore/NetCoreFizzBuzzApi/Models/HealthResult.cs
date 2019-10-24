using System.Text.Json.Serialization;

namespace NetCoreFizzBuzzApi.Models {
    public class HealthResult {

        [JsonPropertyName("redis_up")]
        public bool RedisUp { get; set; }

        [JsonPropertyName("redis_ping_latency")]
        public double? RedisPingLatency { get; set; }
        
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}