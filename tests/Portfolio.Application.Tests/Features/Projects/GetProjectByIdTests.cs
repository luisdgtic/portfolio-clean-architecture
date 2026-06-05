using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Projects;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.Projects;

public sealed class GetProjectByIdTests
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly GetProjectByIdHandler _handler;

    public GetProjectByIdTests()
    {
        _repositoryMock = new Mock<IProjectRepository>();
        _handler = new GetProjectByIdHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnProject_WhenFound()
    {
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            Id = projectId,
            Title = "Test Project",
            Description = "Description",
            TechStack = new List<string> { "C#" }
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        var result = await _handler.Handle(new GetProjectByIdQuery(projectId), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Id.Should().Be(projectId);
        result.Value.Title.Should().Be("Test Project");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNotFound()
    {
        var projectId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project?)null);

        var result = await _handler.Handle(new GetProjectByIdQuery(projectId), CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase));
    }
}
