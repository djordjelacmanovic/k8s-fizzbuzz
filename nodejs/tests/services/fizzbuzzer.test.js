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
  });
});