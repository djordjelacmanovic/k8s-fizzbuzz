class FizzBuzzer
  attr_reader :ruleset

  def initialize(**rules)
    init_ruleset(rules)
  end

  def get_fizzbuzzed_string(number)
    ruleset
      .select { |t, rule| rule.call(number) }
      .map(&:first)
      .join("").presence || number.to_s
  end

  def default_ruleset
    {
      Fizz: ->(n) { n % 3 == 0 },
      Buzz: ->(n) { n % 5 == 0 },
    }
  end

  def self.divisible_by(divisor)
    ->(number) { number % divisor == 0 }
  end

  private

  def init_ruleset(ruleset)
    @ruleset = ruleset.presence || default_ruleset
  end
end
