const FizzBuzzer = require('../../services/fizzbuzzer');

describe(FizzBuzzer, () => {
  describe('when using default implemenation', () => {
    const fizzbuzzer = new FizzBuzzer();

    describe('#getFizzBuzzedString()', () => {
      it('returns Fizz when passed 3', () => {
        expect(fizzbuzzer.getFizzBuzzedString(3)).toBe('Fizz');
      });

      it('returns Buzz when passed 5', () => {
        expect(fizzbuzzer.getFizzBuzzedString(5)).toBe('Buzz');
      });

      it('returns FizzBuzz when passed 15', () => {
        expect(fizzbuzzer.getFizzBuzzedString(15)).toBe('FizzBuzz');
      });
    });
  });

  describe('when passed custom ruleset', () => {
    it('returns expected value', () => {
      const fizzbuzzer = new FizzBuzzer({
        one: (n) => n === 1,
        odd: (n) => n % 2 != 0,
      });
      expect(fizzbuzzer.getFizzBuzzedString(1)).toBe('oneodd');
      expect(fizzbuzzer.getFizzBuzzedString(2)).toBe('2');
      expect(fizzbuzzer.getFizzBuzzedString(3)).toBe('odd');
    });
  });

  describe('.divisibleBy()', () => {
    it('generates correct divisibility predicate', () => {
      const isEven = FizzBuzzer.divisibleBy(2);
      expect(isEven(4)).toBe(true);
      expect(isEven(5)).toBe(false);
    });
  });
});