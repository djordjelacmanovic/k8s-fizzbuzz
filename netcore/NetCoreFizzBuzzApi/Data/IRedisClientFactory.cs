using StackExchange.Redis;

namespace NetCoreFizzBuzzApi.Data {
    public interface IRedisClientFactory {
        IDatabaseAsync GetDatabase();
    }
}