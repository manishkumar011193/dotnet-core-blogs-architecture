using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IRepository<Data.Models.Post> _postRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Handler> _logger;

        public Handler(IRepository<Data.Models.Post> postRepository, IMapper mapper, ILogger<Handler> logger)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the post list");

            // Retrieve a paginated list of posts
            var result = await _postRepository.ListAsync();

            _logger.LogInformation("Successfully fetched the post list");

            // Map the retrieved posts to response models and return them
            return new ValidObjectResult(_mapper.Map<PaginatedResponseModel<PostResponseModel>>(result));
        }
    }
}
