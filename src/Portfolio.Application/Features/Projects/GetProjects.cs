using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Projects;

public sealed record GetProjectsQuery(bool? Featured = null) : IRequest<Result<IReadOnlyList<ProjectDto>>>;

public sealed record ProjectDto(
    Guid Id,
    string Title,
    string Description,
    string? ImageUrl,
    string? GitHubUrl,
    string? LiveUrl,
    IReadOnlyList<string> TechStack,
    bool IsFeatured,
    int SortOrder);

public sealed class GetProjectsHandler : IRequestHandler<GetProjectsQuery, Result<IReadOnlyList<ProjectDto>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<IReadOnlyList<ProjectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = request.Featured == true
            ? await _projectRepository.GetFeaturedAsync(cancellationToken)
            : await _projectRepository.GetAllAsync(cancellationToken);

        var dtos = projects.Select(Map).OrderBy(p => p.SortOrder).ToList();
        return Result<IReadOnlyList<ProjectDto>>.Success(dtos);
    }

    private static ProjectDto Map(Domain.Entities.Project p) =>
        new(p.Id, p.Title, p.Description, p.ImageUrl, p.GitHubUrl, p.LiveUrl,
            p.TechStack, p.IsFeatured, p.SortOrder);
}
