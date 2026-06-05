using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Education;

namespace Portfolio.WebApi.Controllers;

public sealed class EducationController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EducationDto>>> Get()
    {
        var result = await Mediator.Send(new GetEducationQuery());
        return HandleResult(result);
    }
}
