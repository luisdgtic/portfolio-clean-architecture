using MediatR;
using Portfolio.Application.Common.Models;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Projects;

public sealed record GetProjectByIdQuery(Guid Id) : IRequest<Result<ProjectDetailDto>>;

public sealed record ProjectDetailDto(
    Guid Id,
    string Title,
    string Description,
    string? ImageUrl,
    string? GitHubUrl,
    string? LiveUrl,
    IReadOnlyList<string> TechStack,
    bool IsFeatured,
    int SortOrder);

public sealed class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDetailDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectDetailDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return Result<ProjectDetailDto>.Failure($"Project with id '{request.Id}' not found.");

        var dto = new ProjectDetailDto(
            project.Id, project.Title, project.Description, project.ImageUrl,
            project.GitHubUrl, project.LiveUrl, project.TechStack,
            project.IsFeatured, project.SortOrder);

        return Result<ProjectDetailDto>.Success(dto);
    }
}
