﻿using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.GetById
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IRepository<Data.Models.Comment> _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Handler> _logger;

        public Handler(IRepository<Data.Models.Comment> commentRepository, IMapper mapper, ILogger<Handler> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the comment with Id: {Id}", query.Id);

            // Retrieve the comment by its Id
            var commentDetail = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(query.Id), cancellationToken);

            if (commentDetail == null)
            {
                _logger.LogError("Comment with Id: {Id} not found", query.Id);
                return new NotFoundValidationResult("CommentId", "Invalid Comment Id");
            }

            _logger.LogInformation("Successfully fetched the comment with Id: {Id}", commentDetail.Id);

            // Map the retrieved comment to a response model and return it
            return new ValidObjectResult(_mapper.Map<CommentResponseModel>(commentDetail));
        }
    }
}