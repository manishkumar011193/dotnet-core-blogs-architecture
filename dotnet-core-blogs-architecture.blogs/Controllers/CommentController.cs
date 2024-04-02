using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_blogs_architecture.blogs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}
