const FizzBuzzer = require('../services/fizzbuzzer');

module.exports = class FizzBuzzController {
  constructor(){
    this._fizzbuzzer = new FizzBuzzer();
    this._redis = require('../libs/redis');
  }

  async get(_, res){
    const newCounterValue = await this._redis.incr(this.redisCounterKey);
    const fizzbuzzedString = this._fizzbuzzer.getFizzBuzzedString(newCounterValue).toString();
    return res.json({
      value: fizzbuzzedString,
      app_name: process.env.npm_package_name,
      app_version: process.env.npm_package_version
    });
  }

  async delete(_, res){
    await this._redis.del(this.redisCounterKey);
    return res.status(204).send();
  }

  get redisCounterKey(){
    return process.env.FIZZBUZZ_REDIS_COUNTER_KEY || 'fizzbuzz-counter';
  }
}