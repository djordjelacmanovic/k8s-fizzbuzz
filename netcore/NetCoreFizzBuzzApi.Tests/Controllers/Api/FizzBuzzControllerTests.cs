using NSubstitute;
using NUnit.Framework;
using NetCoreFizzBuzzApi.Controllers.Api;
using NetCoreFizzBuzzApi.Services;
using System.Threading.Tasks;

namespace NetCoreFizzBuzzApi.Tests.Controllers.Api {

    [TestFixture]
    public class FizzBuzzControllerTests {
        readonly IFizzBuzzer _fizzbuzzer;
        readonly IAppInfoService _appInfoService;
        readonly ICounter _counter;
        readonly FizzBuzzController _controller;

        public FizzBuzzControllerTests(){
            _fizzbuzzer = Substitute.For<IFizzBuzzer>();
            _appInfoService = Substitute.For<IAppInfoService>();
            _counter = Substitute.For<ICounter>();
            _controller = new FizzBuzzController(_fizzbuzzer, _appInfoService, _counter);
        }

        [Test]
        public void Delete_Resets_The_Counter(){
            _controller.Delete();
            _counter.Received(1).Reset();
        }

        [Test]
        public async Task Get_Returns_The_Correct_App_Name(){
            var testName = "Test App";
            _appInfoService.Name.Returns(testName);
            var result = await _controller.Get();
            Assert.AreEqual(result.AppName, testName);
        }

        [Test]
        public async Task Get_Returns_The_Correct_Version(){
            var testVersion = "0.1.1";
            _appInfoService.Version.Returns(testVersion);
            var result = await _controller.Get();
            Assert.AreEqual(result.AppVersion, testVersion);
        }

        [Test]
        public async Task Get_Returns_Correct_FizzBuzzed_Value_From_Counter(){
            var newValue = 11;
            var testString = "zzBBzz";
            _counter.Increment().Returns(newValue);
            _fizzbuzzer.GetFizzBuzzedString(newValue).Returns(testString);
            var result = await _controller.Get();
            Assert.AreEqual(result.Value, testString);
        }

        [TearDown]
        public void Reset(){
            _fizzbuzzer.ClearReceivedCalls();
            _appInfoService.ClearReceivedCalls();
            _counter.ClearReceivedCalls();
        }
    }
}