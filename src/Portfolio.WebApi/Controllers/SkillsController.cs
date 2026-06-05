using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Skills;
using Portfolio.Domain.Enums;

namespace Portfolio.WebApi.Controllers;

public sealed class SkillsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SkillDto>>> Get([FromQuery] SkillCategory? category = null)
    {
        var result = await Mediator.Send(new GetSkillsQuery(category));
        return HandleResult(result);
    }
}
