require "redis"

RSpec.describe Api::V1::FizzbuzzController, type: :controller do
  ENV["FIZZBUZZ_REDIS_COUNTER_KEY"] = "test-counter-key"

  let(:redis) {
    redis = double(Redis)
    allow(redis).to receive(:incr)
    redis
  }

  let(:fizzbuzzer) {
    fizzbuzzer = double(FizzBuzzer)
    allow(fizzbuzzer).to receive(:get_fizzbuzzed_string)
    fizzbuzzer
  }

  before(:each) do
    allow(Redis).to receive(:current).and_return(redis)
    allow(FizzBuzzer).to receive(:new).and_return(fizzbuzzer)
  end

  describe "GET #index" do
    it "increments the value of fizzbuzz counter" do
      expect(redis).to receive(:incr).with("test-counter-key")
      get :index
    end

    it "calls FizzBuzzer with new counter value" do
      allow(redis).to receive(:incr).and_return(15)
      expect(fizzbuzzer).to receive(:get_fizzbuzzed_string).with(15)
      get :index
    end

    it "returns correct response" do
      fizzbuzzed_string = "Test_Buzz"
      expect(fizzbuzzer).to receive(:get_fizzbuzzed_string).and_return(fizzbuzzed_string)
      get :index
      expect(JSON.parse(response.body)).to eql({
        "value" => fizzbuzzed_string,
        "app_name" => Rails.application.class.module_parent_name,
        "app_version" => File.read(Rails.root.join("version")).chomp,
      })
    end
  end

  describe "DELETE #index" do
    it "deletes the redis key" do
      expect(redis).to receive(:del).with("test-counter-key")
      delete :reset
    end
  end
end
