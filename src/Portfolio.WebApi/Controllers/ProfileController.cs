using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Profiles;

namespace Portfolio.WebApi.Controllers;

public sealed class ProfileController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> Get()
    {
        var result = await Mediator.Send(new GetProfileQuery());
        return HandleResult(result);
    }
}
