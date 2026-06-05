using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Education;
using Portfolio.Domain.Interfaces;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Application.Tests.Features.Education;

public sealed class GetEducationTests
{
    [Fact]
    public async Task Handle_ShouldReturnEducationOrderedBySortOrder()
    {
        var repositoryMock = new Mock<IEducationRepository>();
        var handler = new GetEducationHandler(repositoryMock.Object);

        var educationList = new List<Domain.Entities.Education>
        {
            new() { Id = Guid.NewGuid(), Institution = "University", Degree = "BS", Period = new DateRange(DateTime.UtcNow), SortOrder = 1 }
        };

        repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(educationList);

        var result = await handler.Handle(new GetEducationQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
        result.Value![0].Institution.Should().Be("University");
    }
}
