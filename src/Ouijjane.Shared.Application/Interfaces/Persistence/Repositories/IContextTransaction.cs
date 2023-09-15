namespace Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
public interface IContextTransaction : IAsyncDisposable, IDisposable
{
    Task Commit(CancellationToken cancellationToken = default);

    Task Rollback(CancellationToken cancellationToken = default);
}
