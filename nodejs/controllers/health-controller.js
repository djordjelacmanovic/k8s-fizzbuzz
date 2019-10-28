const { performance } = require('perf_hooks');

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
      return res.status(503).json({
        redis_up: false,
        error: error
      });
    }
  }
}