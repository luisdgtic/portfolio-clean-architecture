using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Profiles;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.Profiles;

public sealed class GetProfileTests
{
    private readonly Mock<IProfileRepository> _repositoryMock;
    private readonly GetProfileHandler _handler;

    public GetProfileTests()
    {
        _repositoryMock = new Mock<IProfileRepository>();
        _handler = new GetProfileHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnProfile_WhenProfileExists()
    {
        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            FullName = "Test User",
            Title = "Developer",
            Summary = "A developer",
            Email = "test@example.com",
            Location = "Remote"
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Profile> { profile });

        var result = await _handler.Handle(new GetProfileQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.FullName.Should().Be("Test User");
        result.Value.Title.Should().Be("Developer");
        result.Value.Email.Should().Be("test@example.com");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoProfileExists()
    {
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Profile>());

        var result = await _handler.Handle(new GetProfileQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase));
    }
}
