using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Skills;

public sealed record GetSkillsQuery(SkillCategory? Category = null) : IRequest<Result<IReadOnlyList<SkillDto>>>;

public sealed record SkillDto(
    Guid Id,
    string Name,
    SkillCategory Category,
    int Proficiency,
    string? IconUrl,
    int SortOrder);

public sealed class GetSkillsHandler : IRequestHandler<GetSkillsQuery, Result<IReadOnlyList<SkillDto>>>
{
    private readonly ISkillRepository _skillRepository;

    public GetSkillsHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Result<IReadOnlyList<SkillDto>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = request.Category.HasValue
            ? await _skillRepository.GetByCategoryAsync(request.Category.Value, cancellationToken)
            : await _skillRepository.GetAllAsync(cancellationToken);

        var dtos = skills.Select(Map).OrderBy(s => s.SortOrder).ToList();
        return Result<IReadOnlyList<SkillDto>>.Success(dtos);
    }

    private static SkillDto Map(Domain.Entities.Skill s) =>
        new(s.Id, s.Name, s.Category, s.Proficiency, s.IconUrl, s.SortOrder);
}
