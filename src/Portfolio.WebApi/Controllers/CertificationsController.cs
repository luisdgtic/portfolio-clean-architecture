using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Certifications;

namespace Portfolio.WebApi.Controllers;

public sealed class CertificationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CertificationDto>>> Get()
    {
        var result = await Mediator.Send(new GetCertificationsQuery());
        return HandleResult(result);
    }
}
