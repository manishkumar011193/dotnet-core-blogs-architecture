using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Commands.Update
{
    public class Handler : CommentBaseHandler, IRequestHandler<CommandModel, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Data.Models.Comment> _commentRepository;
        private readonly ILogger _logger;

        public Handler(IMapper mapper, IRepository<Data.Models.Comment> commentRepository, ILogger<Handler> logger) : base()
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(CommandModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the comment with Id: {Id}", request.Id);

            // Retrieve the comment from the repository
            var commentDetail = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(request.Id), cancellationToken);

            if (commentDetail == null)
            {
                _logger.LogError("Comment with Id: {Id} not found", request.Id);
                return new NotFoundValidationResult("CommentId", "Invalid Comment Id");
            }

            _logger.LogInformation("Found the comment with Id: {Id}", commentDetail.Id);

            // Update the retrieved comment entity
            AssignValues(request, commentDetail, cancellationToken);
            await _commentRepository.UpdateAsync(commentDetail, cancellationToken);

            // Save the updated comment entity
            var saved = await _commentRepository.SaveChangesAsync(cancellationToken);

            if (saved <= 0)
            {
                _logger.LogError("Unable to save the comment with Id: {Id}", commentDetail.Id);
                return new InvalidValidationResult("Summary", "There was a server error updating 'comment'");
            }

            // Retrieve the updated comment entity from the repository
            commentDetail = await _commentRepository.FirstOrDefaultAsync(new CommentFetchSpecification(commentDetail.Id), cancellationToken);

            _logger.LogInformation("Successfully updated the comment with Id: {Id}", commentDetail.Id);

            // Return the updated comment as a valid result
            return new ValidObjectResult(_mapper.Map<CommentResponseModel>(commentDetail));
        }
    }
}
