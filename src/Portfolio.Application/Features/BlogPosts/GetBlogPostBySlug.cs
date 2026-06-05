using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.BlogPosts;

public sealed record GetBlogPostBySlugQuery(string Slug) : IRequest<Result<BlogPostDetailDto>>;

public sealed record BlogPostDetailDto(
    Guid Id,
    string Title,
    string Slug,
    string Summary,
    string Content,
    DateTime PublishedAt,
    IReadOnlyList<string> Tags,
    int ReadTimeMinutes);

public sealed class GetBlogPostBySlugHandler : IRequestHandler<GetBlogPostBySlugQuery, Result<BlogPostDetailDto>>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public GetBlogPostBySlugHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<Result<BlogPostDetailDto>> Handle(GetBlogPostBySlugQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Slug))
            return Result<BlogPostDetailDto>.Failure("Slug cannot be empty.");

        var post = await _blogPostRepository.GetBySlugAsync(request.Slug, cancellationToken);

        if (post is null)
            return Result<BlogPostDetailDto>.Failure($"Blog post with slug '{request.Slug}' not found.");

        var dto = new BlogPostDetailDto(
            post.Id, post.Title, post.Slug, post.Summary, post.Content,
            post.PublishedAt, post.Tags, post.ReadTimeMinutes);

        return Result<BlogPostDetailDto>.Success(dto);
    }
}
