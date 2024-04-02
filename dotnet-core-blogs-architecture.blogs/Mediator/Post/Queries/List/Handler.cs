using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Data;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List
{
    public class Handler : IRequestHandler<QueryModel, ValidationResult>
    {
        private readonly IReadRepository<Data.Models.Post> _postRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Handler> _logger;

        public Handler(IMapper mapper, IReadRepository<Data.Models.Post> postRepository, ILogger<Handler> logger): base()
        {
           
            _mapper = mapper;
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the post list");

            // Retrieve a paginated list of posts
            var result = await _postRepository.GetPagninated(new PaginatedPostSpecification(query), query);

            _logger.LogInformation("Successfully fetched the post list");

            // Map the retrieved posts to response models and return them
            return new ValidObjectResult(_mapper.Map<PaginatedResponseModel<PostResponseModel>>(result));
        }
    }
}
