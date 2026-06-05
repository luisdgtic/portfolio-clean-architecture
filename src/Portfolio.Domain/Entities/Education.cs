using Portfolio.Domain.Common;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Entities;

public sealed class Education : BaseEntity
{
    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string? FieldOfStudy { get; set; }
    public DateRange Period { get; set; } = new(DateTime.UtcNow);
    public decimal? Gpa { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
}
