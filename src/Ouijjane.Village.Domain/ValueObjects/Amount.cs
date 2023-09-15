using Ouijjane.Village.Domain.Enums;

namespace Ouijjane.Village.Domain.ValueObjects
{
    public record Amount(Currency Currency, decimal Value);
}
