using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.List
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Data.Models.Comment> _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public Handler(IReadRepository<Data.Models.Comment> commentRepository, IMapper mapper, ILogger<Handler> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the comment list");
            var result = await _commentRepository.GetPagninated(new PaginatedCommentSpecification(query), query);
            _logger.LogInformation("Successfully fetched the comment list");
            return new ValidObjectResult(_mapper.Map<PaginatedResponseModel<CommentResponseModel>>(result));
        }
    }
}
