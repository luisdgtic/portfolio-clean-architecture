using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Projects;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.Projects;

public sealed class GetProjectsTests
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly GetProjectsHandler _handler;

    public GetProjectsTests()
    {
        _repositoryMock = new Mock<IProjectRepository>();
        _handler = new GetProjectsHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProjects_WhenNoFilterProvided()
    {
        var projects = CreateTestProjects(3);
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);

        var result = await _handler.Handle(new GetProjectsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnFeaturedProjects_WhenFeaturedIsTrue()
    {
        var projects = CreateTestProjects(2);
        _repositoryMock.Setup(r => r.GetFeaturedAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);

        var result = await _handler.Handle(new GetProjectsQuery(true), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value!.Should().AllSatisfy(p => p.IsFeatured.Should().BeTrue());
    }

    [Fact]
    public async Task Handle_ShouldReturnOrderedBySortOrder()
    {
        var projects = new List<Project>
        {
            new() { Id = Guid.NewGuid(), Title = "C", SortOrder = 3, TechStack = new List<string>() },
            new() { Id = Guid.NewGuid(), Title = "A", SortOrder = 1, TechStack = new List<string>() },
            new() { Id = Guid.NewGuid(), Title = "B", SortOrder = 2, TechStack = new List<string>() }
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);

        var result = await _handler.Handle(new GetProjectsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeInAscendingOrder(p => p.SortOrder);
    }

    private static List<Project> CreateTestProjects(int count)
    {
        return Enumerable.Range(1, count).Select(i => new Project
        {
            Id = Guid.NewGuid(),
            Title = $"Project {i}",
            Description = $"Description {i}",
            IsFeatured = true,
            SortOrder = i,
            TechStack = new List<string> { "C#", "React" }
        }).ToList();
    }
}
