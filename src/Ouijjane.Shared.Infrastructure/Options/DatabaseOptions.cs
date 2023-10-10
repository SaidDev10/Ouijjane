using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class DatabaseOptions : IOptionsRoot
{
    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(DBProvider)} is not configured")]//TODO: localisation
    public string DBProvider { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(ConnectionString)} is not configured")]
    public string ConnectionString { get; set; } = string.Empty;
}