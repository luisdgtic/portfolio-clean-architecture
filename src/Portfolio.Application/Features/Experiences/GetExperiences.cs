using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Experiences;

public sealed record GetExperiencesQuery : IRequest<Result<IReadOnlyList<ExperienceDto>>>;

public sealed record ExperienceDto(
    Guid Id,
    string Company,
    string Position,
    string? Description,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsCurrent,
    IReadOnlyList<string> Technologies,
    string? CompanyUrl,
    int SortOrder);

public sealed class GetExperiencesHandler : IRequestHandler<GetExperiencesQuery, Result<IReadOnlyList<ExperienceDto>>>
{
    private readonly IExperienceRepository _experienceRepository;

    public GetExperiencesHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public async Task<Result<IReadOnlyList<ExperienceDto>>> Handle(GetExperiencesQuery request, CancellationToken cancellationToken)
    {
        var experiences = await _experienceRepository.GetAllAsync(cancellationToken);
        var dtos = experiences.Select(Map).OrderBy(e => e.SortOrder).ToList();
        return Result<IReadOnlyList<ExperienceDto>>.Success(dtos);
    }

    private static ExperienceDto Map(Domain.Entities.Experience e) =>
        new(e.Id, e.Company, e.Position, e.Description,
            e.Period.StartDate, e.Period.EndDate, e.Period.IsCurrent,
            e.Technologies, e.CompanyUrl, e.SortOrder);
}
