const Redis = require("ioredis");

const redisHosts = (process.env.FIZZBUZZ_REDIS_HOSTS || 'localhost').split(',');
console.log(redisHosts);
module.exports = redisHosts.length == 1 ? new Redis(6379, redisHosts[0]) : new Redis.Cluster(redisHosts.map(host => ({
  host,
  port: 6379
})));