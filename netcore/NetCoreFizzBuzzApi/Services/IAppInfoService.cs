namespace NetCoreFizzBuzzApi.Services
{
    public interface IAppInfoService
    {
        string Name { get; }
        
        string Version { get; }
    }
}