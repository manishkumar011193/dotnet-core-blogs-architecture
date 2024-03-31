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

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.List.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetByIdAsync(int commentId)
        {
            var commentDetails = await _mediator.Send(new dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.GetById.QueryModel { Id = commentId });
            return new ObjectResult(commentDetails);
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Update.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }        

        #endregion
    }
}
