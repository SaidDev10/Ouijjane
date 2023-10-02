using Microsoft.Extensions.Options;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Ouijjane.Shared.Infrastructure.Settings;

namespace Ouijjane.Shared.Infrastructure.Persistence.Factories;
public class DefaultDatabaseNameFactory : IDatabaseNameFactory
{
    private readonly MicroserviceSettings _microServiceConfiguration;

    public DefaultDatabaseNameFactory(IOptions<MicroserviceSettings> microServiceConfiguration)
    {
        _microServiceConfiguration = microServiceConfiguration.Value;
    }

    public string Create(string? postfix = null)
    {
        return $"{_microServiceConfiguration.Product}-{_microServiceConfiguration.Namespace}-{_microServiceConfiguration.Module}{postfix}".ToLower();
    }
}
