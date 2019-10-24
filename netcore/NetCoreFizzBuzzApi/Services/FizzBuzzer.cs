using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFizzBuzzApi.Services
{
    class FizzBuzzer : IFizzBuzzer
    {
        readonly IDictionary<string, Predicate<int>> _defaultRules =
            new Dictionary<string, Predicate<int>>{
            { "Fizz", DivisibleBy(3) },
            { "Buzz", DivisibleBy(5) }
            };

        IDictionary<string, Predicate<int>> Rules { get; }

        public FizzBuzzer(IDictionary<string, Predicate<int>> rules = null)
        {
            Rules = rules ?? _defaultRules;
        }

        public static Predicate<int> DivisibleBy(int divisor) => (i) => i % divisor == 0;

        public string GetFizzBuzzedString(int number) => string.Join(null,
                Rules
                    .Where(kv => kv.Value.Invoke(number))
                    .Select(kv => kv.Key)
                    .DefaultIfEmpty(number.ToString()));
    }
}