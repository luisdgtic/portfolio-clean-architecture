using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Experiences;

namespace Portfolio.WebApi.Controllers;

public sealed class ExperiencesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExperienceDto>>> Get()
    {
        var result = await Mediator.Send(new GetExperiencesQuery());
        return HandleResult(result);
    }
}
