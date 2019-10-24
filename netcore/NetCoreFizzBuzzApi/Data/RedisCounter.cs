using System.Threading.Tasks;
using NetCoreFizzBuzzApi.Services;
using StackExchange.Redis;

namespace NetCoreFizzBuzzApi.Data {
    class RedisCounter : ICounter
    {
        private readonly IRedisClientFactory _redisClientFactory;
        private readonly string _persistedCounterKey;

        public IDatabaseAsync RedisDb => _redisClientFactory.GetDatabase();

        public RedisCounter(IRedisClientFactory redisClientFactory, string persistedCounterKey = "fizzbuzz-counter")
        {
            _redisClientFactory = redisClientFactory;
            _persistedCounterKey = persistedCounterKey;
        }

        public async Task<int> Increment() => (int) await RedisDb.StringIncrementAsync(_persistedCounterKey);

        public async Task Reset() => await RedisDb.KeyDeleteAsync(_persistedCounterKey);
    }
}