﻿using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Create;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Update;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.List;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_blogs_architecture.blogs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET

        [ProducesResponseType(typeof(PostResponseModel), 200)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(long Id)
        {
            var postDetails = await _mediator.Send(new Mediator.Post.Queries.GetById.QueryModel { Id = Id });
            return new ObjectResult(postDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] Mediator.Post.Queries.List.QueryModel query)
        {
            return new ObjectResult(await _mediator.Send(query));
        }

        #endregion

        #region POST

        [ProducesResponseType(typeof(PostResponseModel), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Mediator.Post.Commands.Create.CommandModel command)
        {
            return new ObjectResult(await _mediator.Send(command));
        }

        #endregion
    }
}
