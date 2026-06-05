using FluentAssertions;
using Moq;
using Portfolio.Application.Features.BlogPosts;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.BlogPosts;

public sealed class GetBlogPostsTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedPosts()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostsHandler(repositoryMock.Object);

        var posts = new List<BlogPost>
        {
            new()
            {
                Id = Guid.NewGuid(), Title = "Post 1", Slug = "post-1", Summary = "Summary 1",
                Content = "Content", PublishedAt = DateTime.UtcNow, IsPublished = true,
                Tags = new List<string> { "dotnet" }, ReadTimeMinutes = 5
            },
            new()
            {
                Id = Guid.NewGuid(), Title = "Post 2", Slug = "post-2", Summary = "Summary 2",
                Content = "Content", PublishedAt = DateTime.UtcNow.AddDays(-1), IsPublished = true,
                Tags = new List<string> { "react" }, ReadTimeMinutes = 3
            }
        };

        repositoryMock.Setup(r => r.GetPublishedAsync(
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(posts);

        var result = await handler.Handle(new GetBlogPostsQuery(1, 10), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Posts.Should().HaveCount(2);
        result.Value.TotalCount.Should().Be(2);
        result.Value.Page.Should().Be(1);
        result.Value.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPageIsLessThanOne()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostsHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetBlogPostsQuery(0, 10), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("Page"));
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPageSizeIsInvalid()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostsHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetBlogPostsQuery(1, 100), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("PageSize"));
    }
}
