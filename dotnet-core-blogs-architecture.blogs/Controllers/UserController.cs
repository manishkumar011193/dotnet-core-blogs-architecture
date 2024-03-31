using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.Data;
using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.Data.Results;
using DT.Identity.Core.User.Commands;
using DT.Identity.Core.User.Queries;
using DT.Identity.Core.User.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var userDetails = await _mediator.Send(new GetById.QueryModel { Id = userId });
            return new ObjectResult(userDetails);
        }

        [ProducesResponseType(typeof(PaginatedResponseModel<UserResponseModel>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] List.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }

        #endregion

        #region POST

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion

        #region PUT

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Update.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        [ProducesResponseType(typeof(UserResponseModel), 200)]
        [HttpPut("{userId}/Status")]
        public async Task<IActionResult> SetUserStatusAsync(int userId)
        {
            var userDetails = await _mediator.Send(new Status.QueryModel { Id = userId });
            return new ObjectResult(userDetails);
        }

        #endregion
    }
}
