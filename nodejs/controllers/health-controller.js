const { performance } = require('perf_hooks');
const { RedisError } = require('redis-errors');

module.exports = class HealthController {
  constructor(){
    this._redis = require('../libs/redis');
  }

  async get(_, res){
    try {
      const tStart = performance.now();
      await this._redis.ping();
      const tEnd = performance.now();
      return res.json({
        redis_up: true,
        redis_ping_latency: Math.round(tEnd - tStart)
      });
    } catch (error) {
      console.error(error);
      if(error instanceof RedisError) {
        return res.status(503).json({
          redis_up: false,
          error: error.message
        });
      } else throw error;
    }
  }
}