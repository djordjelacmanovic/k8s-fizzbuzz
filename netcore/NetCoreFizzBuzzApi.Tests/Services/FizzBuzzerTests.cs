using System;
using System.Collections.Generic;
using NetCoreFizzBuzzApi.Services;
using NUnit.Framework;

namespace NetCoreFizzBuzzApi.Tests.Services
{
    [TestFixture]
    public class FizzBuzzerTests
    {
        [Test]
        [TestCase(1, ExpectedResult = "1")]
        [TestCase(3, ExpectedResult = "Fizz")]
        [TestCase(5, ExpectedResult = "Buzz")]
        [TestCase(15, ExpectedResult = "FizzBuzz")]
        public string GetFizzBuzzedString_With_Default_Implementation_Returns_Correct_Values(int input)
            => new FizzBuzzer().GetFizzBuzzedString(input);

        [Test]
        [TestCase(7, ExpectedResult = "LessThan15")]
        [TestCase(17, ExpectedResult = "17")]
        [TestCase(10, ExpectedResult = "DivisibleBy10LessThan15")]
        [TestCase(20, ExpectedResult = "DivisibleBy10")]
        public string GetFizzBuzzedString_With_Custom_Rules_Returns_Correct_Values(int input)
            => new FizzBuzzer(CustomRules).GetFizzBuzzedString(input);


        [Test]
        public void DivisibleBy_Generates_Correct_Predicate()
        {
            var predicate = FizzBuzzer.DivisibleBy(5);
            Assert.IsTrue(predicate(5));
            Assert.IsFalse(predicate(3));
        }

        private IDictionary<string, Predicate<int>> CustomRules
            => new Dictionary<string, Predicate<int>>
        {
                { "DivisibleBy10", n => n % 10 == 0 },
                { "LessThan15", n => n < 15 },
        };
    }
}
