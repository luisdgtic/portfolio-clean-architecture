using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class Project : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public IReadOnlyList<string> TechStack { get; set; } = new List<string>();
    public bool IsFeatured { get; set; }
    public int SortOrder { get; set; }
}
