using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Certifications;

public sealed record GetCertificationsQuery : IRequest<Result<IReadOnlyList<CertificationDto>>>;

public sealed record CertificationDto(
    Guid Id,
    string Name,
    string Issuer,
    DateTime IssueDate,
    DateTime? ExpiryDate,
    string? Url,
    string? CredentialId,
    int SortOrder);

public sealed class GetCertificationsHandler : IRequestHandler<GetCertificationsQuery, Result<IReadOnlyList<CertificationDto>>>
{
    private readonly ICertificationRepository _certificationRepository;

    public GetCertificationsHandler(ICertificationRepository certificationRepository)
    {
        _certificationRepository = certificationRepository;
    }

    public async Task<Result<IReadOnlyList<CertificationDto>>> Handle(GetCertificationsQuery request, CancellationToken cancellationToken)
    {
        var certifications = await _certificationRepository.GetAllAsync(cancellationToken);
        var dtos = certifications.Select(Map).OrderBy(c => c.SortOrder).ToList();
        return Result<IReadOnlyList<CertificationDto>>.Success(dtos);
    }

    private static CertificationDto Map(Domain.Entities.Certification c) =>
        new(c.Id, c.Name, c.Issuer, c.IssueDate, c.ExpiryDate,
            c.Url, c.CredentialId, c.SortOrder);
}
