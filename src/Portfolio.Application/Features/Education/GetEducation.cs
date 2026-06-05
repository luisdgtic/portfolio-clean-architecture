using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Education;

public sealed record GetEducationQuery : IRequest<Result<IReadOnlyList<EducationDto>>>;

public sealed record EducationDto(
    Guid Id,
    string Institution,
    string Degree,
    string? FieldOfStudy,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsCurrent,
    decimal? Gpa,
    string? Description,
    int SortOrder);

public sealed class GetEducationHandler : IRequestHandler<GetEducationQuery, Result<IReadOnlyList<EducationDto>>>
{
    private readonly IEducationRepository _educationRepository;

    public GetEducationHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<Result<IReadOnlyList<EducationDto>>> Handle(GetEducationQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetAllAsync(cancellationToken);
        var dtos = education.Select(Map).OrderBy(e => e.SortOrder).ToList();
        return Result<IReadOnlyList<EducationDto>>.Success(dtos);
    }

    private static EducationDto Map(Domain.Entities.Education e) =>
        new(e.Id, e.Institution, e.Degree, e.FieldOfStudy,
            e.Period.StartDate, e.Period.EndDate, e.Period.IsCurrent,
            e.Gpa, e.Description, e.SortOrder);
}
