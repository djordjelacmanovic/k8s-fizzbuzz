using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreFizzBuzzApi.Data;
using NetCoreFizzBuzzApi.Models;
using NetCoreFizzBuzzApi.Services;

[ApiController]
[Route("healthz")]
public class HealthController : ControllerBase {
    private readonly IRedisClientFactory _redisClientFactory;

    public HealthController(IRedisClientFactory redisClientFactory)
    {
        _redisClientFactory = redisClientFactory;
    }


    [HttpGet]
    public async Task<IActionResult> Get(){
        try {
            return Ok(new HealthResult {
                RedisUp = true,
                RedisPingLatency = (await _redisClientFactory.GetDatabase().PingAsync()).Milliseconds
            });
        } catch (Exception e) {
            return StatusCode(503, new HealthResult {
                RedisUp = false,
                RedisPingLatency = null,
                Error = e.Message
            });
        }
    }
}