using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Skills;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.Skills;

public sealed class GetSkillsTests
{
    private readonly Mock<ISkillRepository> _repositoryMock;
    private readonly GetSkillsHandler _handler;

    public GetSkillsTests()
    {
        _repositoryMock = new Mock<ISkillRepository>();
        _handler = new GetSkillsHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllSkills_WhenNoCategoryFilter()
    {
        var skills = new List<Skill>
        {
            new() { Id = Guid.NewGuid(), Name = "C#", Category = SkillCategory.Backend, Proficiency = 90, SortOrder = 1 },
            new() { Id = Guid.NewGuid(), Name = "React", Category = SkillCategory.Frontend, Proficiency = 85, SortOrder = 2 }
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(skills);

        var result = await _handler.Handle(new GetSkillsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ShouldReturnFilteredSkills_WhenCategoryProvided()
    {
        var skills = new List<Skill>
        {
            new() { Id = Guid.NewGuid(), Name = "C#", Category = SkillCategory.Backend, Proficiency = 90, SortOrder = 1 }
        };

        _repositoryMock.Setup(r => r.GetByCategoryAsync(SkillCategory.Backend, It.IsAny<CancellationToken>()))
            .ReturnsAsync(skills);

        var result = await _handler.Handle(new GetSkillsQuery(SkillCategory.Backend), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
        result.Value![0].Category.Should().Be(SkillCategory.Backend);
    }

    [Fact]
    public async Task Handle_ShouldReturnOrderedBySortOrder()
    {
        var skills = new List<Skill>
        {
            new() { Id = Guid.NewGuid(), Name = "Z", Category = SkillCategory.Backend, SortOrder = 3 },
            new() { Id = Guid.NewGuid(), Name = "A", Category = SkillCategory.Backend, SortOrder = 1 }
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(skills);

        var result = await _handler.Handle(new GetSkillsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeInAscendingOrder(s => s.SortOrder);
    }
}
