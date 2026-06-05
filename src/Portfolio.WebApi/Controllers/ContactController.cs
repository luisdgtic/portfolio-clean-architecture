using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.ContactMessages;

namespace Portfolio.WebApi.Controllers;

public sealed class ContactController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SendContactMessageResponse>> Send([FromBody] SendContactMessageCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
