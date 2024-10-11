using biorand.desktop.Attributes.DependencyInjectionAttributes;
using biorand.desktop.Services;

namespace biorand.desktop.Factories;

[RegisterSingleton]
public interface IServiceFactory
{
    IConfigurationService ConfigurationService { get; }
    //IPlayerService PlayerService { get; }
}

public class ServiceFactory : IServiceFactory
{
    private readonly IConfigurationService _configurationService;
    //private readonly IPlayerService _playerService;

    public ServiceFactory(IConfigurationService configurationService/*, IPlayerService playerService*/)
    {
        _configurationService = configurationService;
        //_playerService = playerService;
    }

    public IConfigurationService ConfigurationService => _configurationService;
    //public IPlayerService PlayerService => _playerService;
}
