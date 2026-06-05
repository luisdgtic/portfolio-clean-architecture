using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.BlogPosts;

namespace Portfolio.WebApi.Controllers;

public sealed class BlogController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BlogPostsResponse>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await Mediator.Send(new GetBlogPostsQuery(page, pageSize));
        return HandleResult(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<BlogPostDetailDto>> GetBySlug(string slug)
    {
        var result = await Mediator.Send(new GetBlogPostBySlugQuery(slug));
        return HandleResult(result);
    }
}
