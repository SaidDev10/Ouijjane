using Microsoft.Extensions.Options;
using Ouijjane.Shared.Application.Configurations;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;

namespace Ouijjane.Shared.Infrastructure.Persistence.Factories;
public class DefaultDatabaseNameFactory : IDatabaseNameFactory
{
    private readonly MicroServiceConfiguration _microServiceConfiguration;

    public DefaultDatabaseNameFactory(IOptions<MicroServiceConfiguration> microServiceConfiguration)
    {
        _microServiceConfiguration = microServiceConfiguration.Value;
    }

    public string Create(string? postfix = null)
    {
        return $"{_microServiceConfiguration.Product}-{_microServiceConfiguration.Namespace}-{_microServiceConfiguration.Module}{postfix}".ToLower();
    }
}
