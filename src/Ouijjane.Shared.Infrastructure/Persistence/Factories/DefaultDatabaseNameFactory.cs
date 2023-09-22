using Microsoft.Extensions.Options;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Ouijjane.Shared.Infrastructure.Settings;

namespace Ouijjane.Shared.Infrastructure.Persistence.Factories;
public class DefaultDatabaseNameFactory : IDatabaseNameFactory
{
    private readonly MicroServiceSettings _microServiceConfiguration;

    public DefaultDatabaseNameFactory(IOptions<MicroServiceSettings> microServiceConfiguration)
    {
        _microServiceConfiguration = microServiceConfiguration.Value;
    }

    public string Create(string? postfix = null)
    {
        return $"{_microServiceConfiguration.Product}-{_microServiceConfiguration.Namespace}-{_microServiceConfiguration.Module}{postfix}".ToLower();
    }
}
