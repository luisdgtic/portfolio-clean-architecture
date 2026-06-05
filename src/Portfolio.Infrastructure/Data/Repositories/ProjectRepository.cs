using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Data.Repositories;

public sealed class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(PortfolioDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Project>> GetFeaturedAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Project>()
            .AsNoTracking()
            .Where(p => p.IsFeatured)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(cancellationToken);
    }
}
