using System.Reflection;

namespace NetCoreFizzBuzzApi.Services
{
    class AppInfoService : IAppInfoService
    {
        public string Version => Assembly
            .GetEntryAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;

        public string Name => Assembly.GetEntryAssembly().GetName().Name;
    }
}