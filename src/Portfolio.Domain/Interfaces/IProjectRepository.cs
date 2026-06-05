using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<IReadOnlyList<Project>> GetFeaturedAsync(CancellationToken cancellationToken = default);
}
