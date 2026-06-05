using FluentAssertions;
using Moq;
using Portfolio.Application.Features.Certifications;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Tests.Features.Certifications;

public sealed class GetCertificationsTests
{
    [Fact]
    public async Task Handle_ShouldReturnCertificationsOrderedBySortOrder()
    {
        var repositoryMock = new Mock<ICertificationRepository>();
        var handler = new GetCertificationsHandler(repositoryMock.Object);

        var certifications = new List<Certification>
        {
            new() { Id = Guid.NewGuid(), Name = "Azure", Issuer = "Microsoft", IssueDate = DateTime.UtcNow, SortOrder = 1 }
        };

        repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(certifications);

        var result = await handler.Handle(new GetCertificationsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
        result.Value![0].CredentialId.Should().BeNull();
    }
}
