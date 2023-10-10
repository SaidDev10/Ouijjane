using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class MicroserviceOptions : IOptionsRoot
{
    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Product)} is not configured")]//TODO: localisation
    public string Product { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Module)} is not configured")]
    public string Module { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Component)} is not configured")]
    public string Component { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Version)} is not configured")]
    public string Version { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Namespace)} is not configured")]
    public string Namespace { get; set; } = string.Empty;
}
