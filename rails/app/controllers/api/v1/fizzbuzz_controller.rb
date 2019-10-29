class Api::V1::FizzbuzzController < ApplicationController
  def index
    new_counter_value = Redis.current.incr(redis_fizzbuzz_key)
    render json: {
      value: FizzBuzzer.new.get_fizzbuzzed_string(new_counter_value),
      app_name: Rails.application.class.module_parent_name,
      app_version: File.read(Rails.root.join("version")).chomp,
    }
  end

  def reset
    Redis.current.del(redis_fizzbuzz_key)
  end

  private

  def redis_fizzbuzz_key
    ENV["FIZZBUZZ_REDIS_COUNTER_KEY"] || "fizzbuzz-counter"
  end
end
