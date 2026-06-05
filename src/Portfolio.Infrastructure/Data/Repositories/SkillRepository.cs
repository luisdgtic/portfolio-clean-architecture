using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Data.Repositories;

public sealed class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(PortfolioDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Skill>> GetByCategoryAsync(SkillCategory category, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Skill>()
            .AsNoTracking()
            .Where(s => s.Category == category)
            .OrderBy(s => s.SortOrder)
            .ToListAsync(cancellationToken);
    }
}
