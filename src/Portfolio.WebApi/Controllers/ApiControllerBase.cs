using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common.Models;

namespace Portfolio.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ActionResult<T> HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Errors.Count == 1 && result.Errors[0].Contains("not found", StringComparison.OrdinalIgnoreCase)
            ? NotFound(CreateProblemDetails(StatusCodes.Status404NotFound, "Not Found", result.Errors[0]))
            : BadRequest(CreateProblemDetails(StatusCodes.Status400BadRequest, "Bad Request", string.Join("; ", result.Errors)));
    }

    protected ProblemDetails CreateProblemDetails(int statusCode, string title, string detail)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = HttpContext.Request.Path
        };
    }
}
