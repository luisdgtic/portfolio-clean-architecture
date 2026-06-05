using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Experiences;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Application.Tests.Features.Experiences;

public sealed class GetExperiencesTests
{
    [Fact]
    public async Task Handle_ShouldReturnExperiencesOrderedBySortOrder()
    {
        var repositoryMock = new Mock<IExperienceRepository>();
        var handler = new GetExperiencesHandler(repositoryMock.Object);

        var experiences = new List<Experience>
        {
            new()
            {
                Id = Guid.NewGuid(), Company = "Old Co", Position = "Dev",
                Period = new DateRange(new DateTime(2020, 1, 1)),
                SortOrder = 2, Technologies = new List<string>()
            },
            new()
            {
                Id = Guid.NewGuid(), Company = "Recent Co", Position = "Sr Dev",
                Period = new DateRange(new DateTime(2023, 1, 1)),
                SortOrder = 1, Technologies = new List<string>()
            }
        };

        repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(experiences);

        var result = await handler.Handle(new GetExperiencesQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value.Should().BeInAscendingOrder(e => e.SortOrder);
    }

    [Fact]
    public async Task Handle_ShouldMapIsCurrentCorrectly()
    {
        var repositoryMock = new Mock<IExperienceRepository>();
        var handler = new GetExperiencesHandler(repositoryMock.Object);

        var experiences = new List<Experience>
        {
            new()
            {
                Id = Guid.NewGuid(), Company = "Current Co", Position = "Dev",
                Period = new DateRange(new DateTime(2023, 1, 1)),
                SortOrder = 1, Technologies = new List<string>()
            }
        };

        repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(experiences);

        var result = await handler.Handle(new GetExperiencesQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value![0].IsCurrent.Should().BeTrue();
        result.Value[0].StartDate.Should().Be(new DateTime(2023, 1, 1));
    }
}
