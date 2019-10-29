require "benchmark"

class HealthzController < ApplicationController
  def index
    begin
      render json: {
        redis_up: true,
        redis_ping_latency: Benchmark.measure {
          Redis.current.ping
        }.real.round,
      }
    rescue Redis::CannotConnectError => exception
      render json: {
        redis_up: false,
        error: exception.message,
      }
    end
  end
end
