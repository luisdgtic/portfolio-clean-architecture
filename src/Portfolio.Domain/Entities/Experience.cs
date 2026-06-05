using Portfolio.Domain.Common;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Entities;

public sealed class Experience : BaseEntity
{
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateRange Period { get; set; } = new(DateTime.UtcNow);
    public IReadOnlyList<string> Technologies { get; set; } = new List<string>();
    public string? CompanyUrl { get; set; }
    public int SortOrder { get; set; }
}
