namespace Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
public interface IDatabaseNameFactory
{
        string Create(string? postfix = null);
}
