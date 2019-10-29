RSpec.describe FizzBuzzer do
  context "with default ruleset" do
    subject(:fizzbuzzer) { FizzBuzzer.new }

    it "returns 'Fizz' when given 3" do
      expect(fizzbuzzer.get_fizzbuzzed_string(3)).to eq("Fizz")
    end

    it "returns 'Buzz' when given 5" do
      expect(fizzbuzzer.get_fizzbuzzed_string(5)).to eq("Buzz")
    end

    it "returns 'FizzBuzz' when given 15" do
      expect(fizzbuzzer.get_fizzbuzzed_string(15)).to eq("FizzBuzz")
    end
  end

  context "with custom ruleset" do
    subject(:fizzbuzzer) { FizzBuzzer.new(Boh: ->(n) { n % 10 == 0 }, Meh: ->(n) { n < 10 }, Jah: ->(n) { n > 11 }) }

    it "returns expected values" do
      expect(fizzbuzzer.get_fizzbuzzed_string(11)).to eq("11")
      expect(fizzbuzzer.get_fizzbuzzed_string(5)).to eq("Meh")
      expect(fizzbuzzer.get_fizzbuzzed_string(12)).to eq("Jah")
      expect(fizzbuzzer.get_fizzbuzzed_string(20)).to eq("BohJah")
    end
  end

  describe ".divisible_by()" do
    it "generates correct divisibility predicate" do
      isEven = FizzBuzzer.divisible_by(2)
      expect(isEven.call(4)).to be true
      expect(isEven.call(7)).to be false
    end
  end
end
