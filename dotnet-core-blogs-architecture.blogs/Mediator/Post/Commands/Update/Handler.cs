using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Update
{
    public class Handler : IRequestHandler<CommandModel, ValidationResult>
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

        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the post with Id: {Id}", request.Id);
            // Retrieve the post to update
            var postDetail = await _postRepository.FirstOrDefaultAsync(new PostFetchSpecification(request.Id), cancellationToken);

            if (postDetail == null)
            {
                _logger.LogError("Post with Id: {Id} not found", request.Id);
                return new NotFoundValidationResult("PostId", "Invalid Post Id");
            }

            _logger.LogInformation("Found the post with Id: {Id}", postDetail.Id);

            // Update properties of the retrieved post entity
            postDetail.Title = request.Title;
            postDetail.Content = request.Content;

            // Save the updated post entity
            await _postRepository.UpdateAsync(postDetail, cancellationToken);
            var saved = await _postRepository.SaveChangesAsync(cancellationToken);

            if (saved <= 0)
            {
                _logger.LogError("Unable to save the post with Id: {Id}", postDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error updating 'Post'");
            }

            _logger.LogInformation("Successfully updated the post with Id: {Id}", postDetail.Id);

            // Return the updated post as a valid result
            return new ValidObjectResult(_mapper.Map<Post.Shared.PostResponseModel>(postDetail));
        }

    }
}
