using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class Certification : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Url { get; set; }
    public string? CredentialId { get; set; }
    public int SortOrder { get; set; }
}
