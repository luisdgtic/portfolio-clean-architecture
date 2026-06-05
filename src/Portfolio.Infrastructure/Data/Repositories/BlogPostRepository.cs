using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Data.Repositories;

public sealed class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
{
    public BlogPostRepository(PortfolioDbContext context) : base(context)
    {
    }

    public async Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await Context.Set<BlogPost>()
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished, cancellationToken);
    }

    public async Task<IReadOnlyList<BlogPost>> GetPublishedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Context.Set<BlogPost>()
            .AsNoTracking()
            .Where(b => b.IsPublished)
            .OrderByDescending(b => b.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
