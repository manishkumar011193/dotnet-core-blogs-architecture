using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.GetById
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
            _logger.LogInformation("Fetching the post with Id: {Id}", query.Id);

            // Retrieve the post by its Id
            var postDetail = await _postRepository.FirstOrDefaultAsync(new PostFetchSpecification(query.Id), cancellationToken);

            if (postDetail == null)
            {
                _logger.LogError("Post with Id: {Id} not found", query.Id);
                return new NotFoundValidationResult("PostId", "Invalid Post Id");
            }

            _logger.LogInformation("Successfully fetched the post with Id: {Id}", postDetail.Id);

            // Map the retrieved post to a response model and return it
            return new ValidObjectResult(_mapper.Map<Shared.PostResponseModel>(postDetail));
        }
    }
}
