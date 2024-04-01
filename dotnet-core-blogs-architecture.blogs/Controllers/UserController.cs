using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DT.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #region GET

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var userDetails = await _mediator.Send(new dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.GetById.QueryModel { Id = userId });
            return new ObjectResult(userDetails);
        }

        [ProducesResponseType(typeof(PaginatedResponseModel<UserResponseModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] dotnet_core_blogs_architecture.blogs.Mediator.User.Queries.List.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }

        #endregion

        #region POST

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion

        #region PUT

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Update.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPut("{userId}/status")]
        public async Task<IActionResult> SetUserStatusAsync(int userId)
        {
            var userDetails = await _mediator.Send(new dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Status.QueryModel { Id = userId });
            return new ObjectResult(userDetails);
        }

        #endregion
    }
}
