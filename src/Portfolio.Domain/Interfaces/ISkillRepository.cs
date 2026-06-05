using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Interfaces;

public interface ISkillRepository : IGenericRepository<Skill>
{
    Task<IReadOnlyList<Skill>> GetByCategoryAsync(SkillCategory category, CancellationToken cancellationToken = default);
}
