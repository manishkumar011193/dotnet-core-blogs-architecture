using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Status
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

        public async Task<ValidationResult> Handle(QueryModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the post with Id: {Id}", request.Id);

            // Fetch the post from the repository
            var postDetail = await _postRepository.FirstOrDefaultAsync(new PostFetchSpecification(request.Id), cancellationToken);

            if (postDetail == null)
            {
                _logger.LogError("Post not found with Id: {Id}", request.Id);
                return new NotFoundValidationResult("PostId", "Invalid Post Id");
            }


            // Update the post in the repository
            await _postRepository.UpdateAsync(postDetail, cancellationToken);
            var saved = await _postRepository.SaveChangesAsync(cancellationToken);

            if (saved <= 0)
            {
                _logger.LogError("Unable to update the status for post with Id: {Id}", request.Id);
                return new InvalidValidationResult("Summary", "There was a server error updating post status.");
            }

            // Return the updated post as a valid result
            return new ValidObjectResult(_mapper.Map<Post.Shared.PostResponseModel>(postDetail));
        }
    }
}
