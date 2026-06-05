using FluentAssertions;
using Moq;
using Portfolio.Application.Features.BlogPosts;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.BlogPosts;

public sealed class GetBlogPostBySlugTests
{
    [Fact]
    public async Task Handle_ShouldReturnPost_WhenFound()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostBySlugHandler(repositoryMock.Object);

        var post = new BlogPost
        {
            Id = Guid.NewGuid(), Title = "Test Post", Slug = "test-post",
            Summary = "Summary", Content = "Full content", PublishedAt = DateTime.UtcNow,
            Tags = new List<string> { "dotnet" }, ReadTimeMinutes = 5
        };

        repositoryMock.Setup(r => r.GetBySlugAsync("test-post", It.IsAny<CancellationToken>()))
            .ReturnsAsync(post);

        var result = await handler.Handle(new GetBlogPostBySlugQuery("test-post"), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Slug.Should().Be("test-post");
        result.Value.Content.Should().Be("Full content");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNotFound()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostBySlugHandler(repositoryMock.Object);

        repositoryMock.Setup(r => r.GetBySlugAsync("missing", It.IsAny<CancellationToken>()))
            .ReturnsAsync((BlogPost?)null);

        var result = await handler.Handle(new GetBlogPostBySlugQuery("missing"), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSlugIsEmpty()
    {
        var repositoryMock = new Mock<IBlogPostRepository>();
        var handler = new GetBlogPostBySlugHandler(repositoryMock.Object);

        var result = await handler.Handle(new GetBlogPostBySlugQuery(""), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("Slug"));
    }
}
