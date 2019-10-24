using System;
using StackExchange.Redis;

namespace NetCoreFizzBuzzApi.Data {
    class RedisClientFactory : IRedisClientFactory, IDisposable {

        public static RedisClientFactory Instance => _lazyFactoryInstance.Value;

        private static readonly Lazy<RedisClientFactory> _lazyFactoryInstance = new Lazy<RedisClientFactory>(
            () => new RedisClientFactory(), 
            isThreadSafe: true);
        private readonly Lazy<ConnectionMultiplexer> _lazyRedis;
        private ConnectionMultiplexer Redis => _lazyRedis.Value;

        private RedisClientFactory() { 
            string hostnames = Environment.GetEnvironmentVariable("FIZZBUZZ_REDIS_HOSTS") ?? "localhost";
            _lazyRedis = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(hostnames)
            , isThreadSafe: true);
        }

        public IDatabaseAsync GetDatabase()
            => Redis.GetDatabase();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Redis?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}