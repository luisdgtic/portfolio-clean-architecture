using Portfolio.Domain.Common;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public sealed class Skill : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public SkillCategory Category { get; set; }
    public int Proficiency { get; set; }
    public string? IconUrl { get; set; }
    public int SortOrder { get; set; }
}
