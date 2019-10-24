using System;
using System.Threading.Tasks;

namespace NetCoreFizzBuzzApi.Services {
    public interface ICounter {
        Task<int> Increment();

        Task Reset();
    }
}