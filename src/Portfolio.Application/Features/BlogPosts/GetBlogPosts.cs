using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.BlogPosts;

public sealed record GetBlogPostsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<BlogPostsResponse>>;

public sealed record BlogPostsResponse(
    IReadOnlyList<BlogPostDto> Posts,
    int TotalCount,
    int Page,
    int PageSize);

public sealed record BlogPostDto(
    Guid Id,
    string Title,
    string Slug,
    string Summary,
    DateTime PublishedAt,
    IReadOnlyList<string> Tags,
    int ReadTimeMinutes);

public sealed class GetBlogPostsHandler : IRequestHandler<GetBlogPostsQuery, Result<BlogPostsResponse>>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public GetBlogPostsHandler(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<Result<BlogPostsResponse>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page < 1)
            return Result<BlogPostsResponse>.Failure("Page must be greater than 0.");

        if (request.PageSize is < 1 or > 50)
            return Result<BlogPostsResponse>.Failure("PageSize must be between 1 and 50.");

        var posts = await _blogPostRepository.GetPublishedAsync(request.Page, request.PageSize, cancellationToken);
        var allPosts = await _blogPostRepository.GetPublishedAsync(1, int.MaxValue, cancellationToken);
        var totalCount = allPosts.Count;

        var dtos = posts.Select(Map).ToList();
        var response = new BlogPostsResponse(dtos, totalCount, request.Page, request.PageSize);

        return Result<BlogPostsResponse>.Success(response);
    }

    private static BlogPostDto Map(Domain.Entities.BlogPost b) =>
        new(b.Id, b.Title, b.Slug, b.Summary, b.PublishedAt, b.Tags, b.ReadTimeMinutes);
}
