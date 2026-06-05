using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Projects;

namespace Portfolio.WebApi.Controllers;

public sealed class ProjectsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectDto>>> Get([FromQuery] bool? featured = null)
    {
        var result = await Mediator.Send(new GetProjectsQuery(featured));
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDetailDto>> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetProjectByIdQuery(id));
        return HandleResult(result);
    }
}
