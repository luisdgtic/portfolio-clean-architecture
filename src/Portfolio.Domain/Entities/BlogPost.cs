using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public sealed class BlogPost : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public IReadOnlyList<string> Tags { get; set; } = new List<string>();
    public int ReadTimeMinutes { get; set; }
    public bool IsPublished { get; set; }
}
