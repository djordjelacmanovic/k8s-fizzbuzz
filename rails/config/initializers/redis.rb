require "redis"

redis_hosts = (ENV["FIZZBUZZ_REDIS_HOSTS"] || "localhost").split(",")

Redis.current = redis_hosts.count == 1 ? Redis.new(host: redis_hosts.first) : Redis.new(cluster: redis_hosts.map { |h| { host: h, port: 6379 } })
