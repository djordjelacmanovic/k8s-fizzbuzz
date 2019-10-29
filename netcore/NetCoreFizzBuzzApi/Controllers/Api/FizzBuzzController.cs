using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreFizzBuzzApi.Services;
using NetCoreFizzBuzzApi.Models;

namespace NetCoreFizzBuzzApi.Controllers.Api
{
    public class FizzBuzzController : ApiController
    {
        private IFizzBuzzer FizzBuzzer { get; }
        private IAppInfoService AppInfoService { get; }
        private ICounter Counter { get; }

        public FizzBuzzController(
            IFizzBuzzer fizzBuzzer, 
            IAppInfoService appInfoService,
            ICounter counter){
            FizzBuzzer = fizzBuzzer;
            AppInfoService = appInfoService;
            Counter = counter;
        }

        [HttpGet]
        public async Task<FizzBuzzResult> Get()
            => CreateFizzBuzzResult(await Counter.Increment());

        [HttpDelete]
        public async Task<IActionResult> Delete(){
            await Counter.Reset();
            return NoContent();
        }

        private FizzBuzzResult CreateFizzBuzzResult(int number)
            => new FizzBuzzResult { 
                Value = FizzBuzzer.GetFizzBuzzedString(number), 
                AppName = AppInfoService.Name,
                AppVersion = AppInfoService.Version
            };
    }
}
