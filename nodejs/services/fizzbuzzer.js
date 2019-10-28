module.exports = class FizzBuzzer {
    constructor(rules = {
        Fizz: n => !(n % 3),
        Buzz: n => !(n % 5)
    }){
        this._rules = rules;
    }

    getFizzBuzzedString(number){
        return Object.entries(this._rules)
            .filter(([_, predicate]) => predicate(number))
            .map(([text]) => text).join('') || number;
    }
}